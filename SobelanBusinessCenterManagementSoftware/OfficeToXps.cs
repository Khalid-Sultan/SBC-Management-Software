using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Word = Microsoft.Office.Interop.Word;
namespace SBCManagementSoftware
{
    public class OfficeToXps
    {
        #region Properties & Constants
        private static List<string> wordExtensions = new List<string>
        {
            ".doc",
            ".docx"
        };

        #endregion

        #region Public Methods
        public static OfficeToXpsConversionResult ConvertToXps(string sourceFilePath, ref string resultFilePath)
        {
            var result = new OfficeToXpsConversionResult(ConversionResult.UnexpectedError);

            // Check to see if it's a valid file
            if (!IsValidFilePath(sourceFilePath))
            {
                result.Result = ConversionResult.InvalidFilePath;
                result.ResultText = sourceFilePath;
                return result;
            }



            var ext = Path.GetExtension(sourceFilePath).ToLower();

            // Check to see if it's in our list of convertable extensions
            if (!IsConvertableFilePath(sourceFilePath))
            {
                result.Result = ConversionResult.InvalidFileExtension;
                result.ResultText = ext;
                return result;
            }

            // Convert if Word
            if (wordExtensions.Contains(ext))
            {
                return ConvertFromWord(sourceFilePath, ref resultFilePath);
            }

            return result;
        }
        #endregion

        #region Private Methods
        public static bool IsValidFilePath(string sourceFilePath)
        {
            if (string.IsNullOrEmpty(sourceFilePath))
                return false;

            try
            {
                return File.Exists(sourceFilePath);
            }
            catch (Exception)
            {
            }

            return false;
        }

        public static bool IsConvertableFilePath(string sourceFilePath)
        {
            var ext = Path.GetExtension(sourceFilePath).ToLower();

            return IsConvertableExtension(ext);
        }
        public static bool IsConvertableExtension(string extension)
        {
            return wordExtensions.Contains(extension);
        }

        private static string GetTempXpsFilePath()
        {
            return Path.ChangeExtension(Path.GetTempFileName(), ".xps");
        }


        private static OfficeToXpsConversionResult ConvertFromWord(string sourceFilePath, ref string resultFilePath)
        {
            object pSourceDocPath = sourceFilePath;
            string pExportFilePath = string.IsNullOrWhiteSpace(resultFilePath) ? GetTempXpsFilePath() : resultFilePath;

            try
            {
                var pExportFormat = Word.WdExportFormat.wdExportFormatXPS;
                bool pOpenAfterExport = false;
                var pExportOptimizeFor = Word.WdExportOptimizeFor.wdExportOptimizeForOnScreen;
                var pExportRange = Word.WdExportRange.wdExportAllDocument;
                int pStartPage = 0;
                int pEndPage = 0;
                var pExportItem = Word.WdExportItem.wdExportDocumentContent;
                var pIncludeDocProps = true;
                var pKeepIRM = true;
                var pCreateBookmarks = Word.WdExportCreateBookmarks.wdExportCreateWordBookmarks;
                var pDocStructureTags = true;
                var pBitmapMissingFonts = true;
                var pUseISO19005_1 = false;


                Word.Application wordApplication = null;
                Word.Document wordDocument = null;

                try
                {
                    wordApplication = new Word.Application();
                }
                catch (Exception exc)
                {
                    return new OfficeToXpsConversionResult(ConversionResult.ErrorUnableToInitializeOfficeApp, "Word", exc);
                }

                try
                {
                    try
                    {
                        wordDocument = wordApplication.Documents.Open(ref pSourceDocPath);
                    }
                    catch (Exception exc)
                    {
                        return new OfficeToXpsConversionResult(ConversionResult.ErrorUnableToOpenOfficeFile, exc.Message, exc);
                    }

                    if (wordDocument != null)
                    {
                        try
                        {
                            wordDocument.ExportAsFixedFormat(
                                                pExportFilePath,
                                                pExportFormat,
                                                pOpenAfterExport,
                                                pExportOptimizeFor,
                                                pExportRange,
                                                pStartPage,
                                                pEndPage,
                                                pExportItem,
                                                pIncludeDocProps,
                                                pKeepIRM,
                                                pCreateBookmarks,
                                                pDocStructureTags,
                                                pBitmapMissingFonts,
                                                pUseISO19005_1
                                            );
                        }
                        catch (Exception exc)
                        {
                            return new OfficeToXpsConversionResult(ConversionResult.ErrorUnableToExportToXps, "Word", exc);
                        }
                    }
                    else
                    {
                        return new OfficeToXpsConversionResult(ConversionResult.ErrorUnableToOpenOfficeFile);
                    }
                }
                finally
                {
                    // Close and release the Document object.
                    if (wordDocument != null)
                    {
                        wordDocument.Close();
                        wordDocument = null;
                    }

                    // Quit Word and release the ApplicationClass object.
                    if (wordApplication != null)
                    {
                        wordApplication.Quit();
                        wordApplication = null;
                    }

                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                }
            }
            catch (Exception exc)
            {
                return new OfficeToXpsConversionResult(ConversionResult.ErrorUnableToAccessOfficeInterop, "Word", exc);
            }

            resultFilePath = pExportFilePath;

            return new OfficeToXpsConversionResult(ConversionResult.OK, pExportFilePath);
        }
        #endregion
    }

    public class OfficeToXpsConversionResult
    {
        #region Properties
        public ConversionResult Result { get; set; }
        public string ResultText { get; set; }
        public Exception ResultError { get; set; }
        #endregion

        #region Constructors
        public OfficeToXpsConversionResult()
        {
            Result = ConversionResult.UnexpectedError;
            ResultText = string.Empty;
        }
        public OfficeToXpsConversionResult(ConversionResult result)
            : this()
        {
            Result = result;
        }
        public OfficeToXpsConversionResult(ConversionResult result, string resultText)
            : this(result)
        {
            ResultText = resultText;
        }
        public OfficeToXpsConversionResult(ConversionResult result, string resultText, Exception exc)
            : this(result, resultText)
        {
            ResultError = exc;
        }
        #endregion
    }

    public enum ConversionResult
    {
        OK = 0,
        InvalidFilePath = 1,
        InvalidFileExtension = 2,
        UnexpectedError = 3,
        ErrorUnableToInitializeOfficeApp = 4,
        ErrorUnableToOpenOfficeFile = 5,
        ErrorUnableToAccessOfficeInterop = 6,
        ErrorUnableToExportToXps = 7
    }
}
