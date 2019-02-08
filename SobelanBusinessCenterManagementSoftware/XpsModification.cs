using System;
using System.Windows;
using System.Windows.Xps.Packaging;
using System.Collections.Generic;
using System.IO;
using System.Xml;


namespace SBCManagementSoftware
{
    public class XpsModification {
        List<XpsDetails> xpsImages = new List<XpsDetails>();
        List<XpsDetails> xpsFonts = new List<XpsDetails>();

        public XpsDocument CreateNewXPSFromSource(XpsDocument docToRead, string destinationXpsPath, Dictionary<string, string> inputs)
        {
            try{
                if (File.Exists(destinationXpsPath)) File.Delete(destinationXpsPath);
                XpsDocument document = new XpsDocument(destinationXpsPath,FileAccess.ReadWrite);
                IXpsFixedDocumentSequenceWriter docSeqWriter = document.AddFixedDocumentSequence();
                IXpsFixedDocumentSequenceReader docSequenceToRead = docToRead.FixedDocumentSequenceReader;
                foreach (IXpsFixedDocumentReader fixedDocumentReader in docSequenceToRead.FixedDocuments){
                    IXpsFixedDocumentWriter fixedDocumentWriter = docSeqWriter.AddFixedDocument();
                    AddStructure(fixedDocumentReader, fixedDocumentWriter);
                    foreach (IXpsFixedPageReader fixedPageReader in fixedDocumentReader.FixedPages){
                        IXpsFixedPageWriter pageWriter = fixedDocumentWriter.AddFixedPage();
                        AddImages(fixedPageReader, pageWriter);
                        AddFonts(fixedPageReader, pageWriter);
                        AddContent(fixedPageReader, pageWriter,inputs);
                        pageWriter.Commit();
                    }
                    fixedDocumentWriter.Commit();
                }
                docToRead.Close();
                docSeqWriter.Commit();
                return document;
            }
            catch (Exception ex){
                MessageBox.Show(ex.Message.ToString());
                MessageBox.Show("Failed To Create Document");
                return null;
            }
        }

        private void AddContent(IXpsFixedPageReader fixedPageReader,IXpsFixedPageWriter pageWriter, Dictionary<string,string> inputs){
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(fixedPageReader.XmlReader);
            EditXPSContent(xmlDoc.ChildNodes,inputs);
            xmlDoc.Save(pageWriter.XmlWriter);
        }

        private void AddFonts( IXpsFixedPageReader fixedPageReader, IXpsFixedPageWriter pageWriter) {
            foreach (XpsFont font in fixedPageReader.Fonts){
                if (!CheckIfFontAddedAlready(font.Uri.ToString())){
                    XpsFont newFont = pageWriter.AddFont(false);
                    using (Stream dstFontStream = newFont.GetStream()){
                        using (Stream srcFontStream = font.GetStream()){
                            if (font.IsObfuscated) WriteObfuscatedStream(font.Uri.ToString(),dstFontStream, srcFontStream);
                            else WriteToStream(dstFontStream, srcFontStream);
                            newFont.Commit();
                            XpsDetails xpsFont = new XpsDetails();
                            xpsFont.resource = newFont;
                            xpsFont.sourceURI = font.Uri;
                            xpsFont.destURI = newFont.Uri;
                            xpsFonts.Add(xpsFont);
                        }
                    }
                }
            }
        }
 
        private void AddImages( IXpsFixedPageReader fixedPageReader, IXpsFixedPageWriter pageWriter) {
            foreach (XpsImage image in fixedPageReader.Images) {
                XpsImage newImage = null; 
                string sourceExt = image.Uri.ToString().ToLower(); 
                if (sourceExt.Contains(ImageType.PNG)) {
                    newImage = pageWriter.AddImage(XpsImageType.PngImageType);
                }
                else if (sourceExt.Contains(ImageType.JPG) || sourceExt.Contains(ImageType.JPEG)) {
                    newImage = pageWriter.AddImage(XpsImageType.JpegImageType);
                }
                else if (sourceExt.Contains(ImageType.TIF) || sourceExt.Contains(ImageType.TIFF)) {
                    newImage = pageWriter.AddImage(XpsImageType.TiffImageType);
                }
                else if (sourceExt.Contains(ImageType.WDP)) {
                    newImage = pageWriter.AddImage(XpsImageType.WdpImageType);
                }
                else {
                    newImage = null;
                }
                if (null != newImage) {
                    using (Stream dstImageStream = newImage.GetStream()) {
                        using (Stream srcImageStream = image.GetStream()) {
                            CopyStream(srcImageStream, dstImageStream);
                            newImage.Commit();
                            XpsDetails xpsImage = new XpsDetails();
                            xpsImage.resource = newImage;
                            xpsImage.sourceURI = image.Uri;
                            xpsImage.destURI = newImage.Uri;
                            xpsImages.Add(xpsImage);
                        }
                    }
                }
            }
        } 
        private static void AddStructure( IXpsFixedDocumentReader fixedDocumentReader, IXpsFixedDocumentWriter fixedDocumentWriter) {
            XpsStructure Structure = fixedDocumentReader.DocumentStructure;
            if (Structure != null) {
                XpsStructure myStructure = fixedDocumentWriter.AddDocumentStructure(); 
                using (Stream destStream = myStructure.GetStream()) {
                    using (Stream sourceStream = Structure.GetStream()) {
                        CopyStream(sourceStream, destStream);
                        myStructure.Commit();
                    }
                }
            }
        } 
		 
        private void WriteToStream( Stream destStream, Stream sourceStream) {
            const int bufSize = 0x1000;
            byte[] buf = new byte[bufSize];
            int bytesRead = 0; 
            while ((bytesRead = sourceStream.Read(buf, 0, bufSize)) > 0) destStream.Write(buf, 0, bytesRead); 

        }
 
        private void WriteObfuscatedStream( string resourceName, Stream destStream, Stream sourceStream) {
            int bufSize = 0x1000;
            int guidByteSize = 16;
            int obfuscatedByte = 32; 
            int startPos = resourceName.LastIndexOf('/') + 1;
            int length = resourceName.LastIndexOf('.') - startPos;
            resourceName = resourceName.Substring(startPos, length); 
            Guid guid = new Guid(resourceName); 
            string guidString = guid.ToString("N"); 
            byte[] guidBytes = new byte[guidByteSize];
            for (int i = 0; i < guidBytes.Length; i++) guidBytes[i] = Convert.ToByte(guidString.Substring(i * 2, 2), 16); 
            byte[] buf = new byte[obfuscatedByte];
            sourceStream.Read(buf, 0, obfuscatedByte); 
            for (int i = 0; i < obfuscatedByte; i++) {
                int guidBytesPos = guidBytes.Length - (i % guidBytes.Length) - 1;
                buf[i] ^= guidBytes[guidBytesPos];
            }
            destStream.Write(buf, 0, obfuscatedByte); 
            buf = new byte[bufSize]; 
            int bytesRead = 0;
            while ((bytesRead = sourceStream.Read(buf, 0, bufSize)) > 0) destStream.Write(buf, 0, bytesRead); 
        }

        private void EditXPSContent(XmlNodeList childNodes,Dictionary<string,string> inputs){
            int maxvalue = childNodes.Count;
            for (int i = 0; i < maxvalue; i++) {
                XmlNode node = childNodes[i]; 
                if (node.HasChildNodes) {
                    EditXPSContent(node.ChildNodes, inputs);
                } 
                if (node.NodeType == XmlNodeType.Element) {
                    if (node.Name == "Glyphs") {
                        foreach (XmlAttribute attribute in node.Attributes) {
                            if (attribute.Name == "UnicodeString") {
                                foreach (string key in inputs.Keys){
                                    if (attribute.Value.Contains(key)){
										node.Attributes["UnicodeString"].Value = node.Attributes["UnicodeString"].Value.Replace( $"{key}" , $"{inputs[key]}");
                                        node.Attributes["Indices"].Value = "";
                                    }
                                }
                            }
                            if (attribute.Name == "FontUri") {
                                node.Attributes["FontUri"].Value = GetNewFontUri(attribute.Value);
                            }
                        }
                    }
                    else if (node.Name == "ImageBrush")
                    {
                        foreach (XmlAttribute attribute in node.Attributes)
                        {
                            if (attribute.Name == "ImageSource")
                            {
                                attribute.Value = GetNewImageUri(attribute.Value);
                            }
                        }
                    }
                }
            }
        }

        private string GetNewFontUri(string oldUri){
            string result = string.Empty;
            foreach (XpsDetails data in xpsFonts){
                if (data.sourceURI.ToString() == oldUri.ToString()){
                    result = data.destURI.ToString();
                }
            }
            return result;
        }

        private string GetNewImageUri(string oldUri){
            string result = string.Empty;
            foreach (XpsDetails data in xpsImages){
                if (data.sourceURI.ToString() == oldUri.ToString()){
                    result = data.destURI.ToString();
                }
            }
            return result;
        }

        private string GetNumFontUri(){
            string uri = string.Empty;
            foreach (XpsDetails dat in xpsFonts){
                if (dat.sourceURI.ToString() == dat.destURI.ToString()){
                    uri = dat.destURI.ToString();
                    break;
                }
            }
            return uri;
        } 

        private bool CheckIfFontAddedAlready(string uri){
            bool IsFontAdded = false;
            foreach (XpsDetails data in xpsFonts){
                if (data.sourceURI.ToString() == uri){
                    IsFontAdded = true;
                }
            }
            return IsFontAdded;
        }

        private static Int32 CopyStream( Stream sourceStream, Stream destStream)  {
            const int size = 64 * 1024;
            byte[] localBuffer = new byte[size];
            int bytesRead;
            Int32 bytesMoved = 0;
            sourceStream.Seek(0, SeekOrigin.Begin);
            destStream.Seek(0, SeekOrigin.Begin);
            while ((bytesRead = sourceStream.Read(localBuffer, 0, size)) > 0){
                destStream.Write(localBuffer, 0, bytesRead);
                bytesMoved += bytesRead;
            }
            return bytesMoved;
        }

    }

    public static class ImageType {
        public static string PNG = ".png";
        public static string JPG = ".jpg";
        public static string JPEG = ".jpeg";
        public static string TIF = ".tif";
        public static string TIFF = ".tiff";
        public static string WDP = ".wdp";

    }

}
