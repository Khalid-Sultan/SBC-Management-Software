using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SBCManagementSoftware
{
    /// <summary>
    /// Interaction logic for OtherDocPage.xaml
    /// </summary>
    public partial class OtherDocPage : Page
    {
        System.Windows.Xps.Packaging.XpsDocument xpsDoc;
        public static bool officeFileOpen_Status = false;

        public OtherDocPage()
        {
            InitializeComponent();
        }
        // Main Nav Bar Buttons
        private void HomeButton_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            HomePage homePage = new HomePage();
            this.NavigationService.Navigate(homePage);
        }
        private void ShopsButton_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            ShopsPage shopsPage = new ShopsPage();
            this.NavigationService.Navigate(shopsPage);
        }
        private void EmployeesButton_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            EmployeesPage employeesPage = new EmployeesPage();
            this.NavigationService.Navigate(employeesPage);
        }
        private void AboutUsButton_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            AboutUsPage aboutUsPage = new AboutUsPage();
            this.NavigationService.Navigate(aboutUsPage);
        }
        private void LegalDocumentsButton_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            LegalDocumentsPage legalDocumentsPage = new LegalDocumentsPage();
            this.NavigationService.Navigate(legalDocumentsPage);
        }
        private void Button_MouseEnter(object sender, MouseEventArgs e)
        {
            TabItem tabItem = (TabItem)sender;
            tabItem.Foreground = System.Windows.Media.Brushes.Black;
        }

        private void Button_MouseLeave(object sender, MouseEventArgs e)
        {
            TabItem tabItem = (TabItem)sender;
            tabItem.Foreground = System.Windows.Media.Brushes.White;
        }


        private void ViewEstablishmentDoc_Click(object sender, RoutedEventArgs e)
        {
            if(xpsDoc!=null) xpsDoc.Close();
            ViewEstablishmentDocPage viewEstablishmentDocPage = new ViewEstablishmentDocPage();
            this.NavigationService.Navigate(viewEstablishmentDocPage);
        }
        private void UpdateEstablishmentDoc_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
            string xpsFilePath = String.Empty;
            openFileDialog.DefaultExt = ".txt";
            openFileDialog.Filter = "Word Files(*.docx;*.doc)|*.docx;*.doc";
            Nullable<bool> result = openFileDialog.ShowDialog();
            if (result == true)
            {
                string filename = openFileDialog.FileName;
                xpsFilePath = Environment.CurrentDirectory.ToString() + "\\" + openFileDialog.SafeFileName + ".xps";
                var convertResults = OfficeToXps.ConvertToXps(filename, ref xpsFilePath);
                switch (convertResults.Result)
                {
                    case ConversionResult.OK:
                        xpsDoc = new System.Windows.Xps.Packaging.XpsDocument(xpsFilePath, FileAccess.ReadWrite);
                        if(xpsDoc!=null) xpsDoc.Close();
                        System.Windows.MessageBox.Show("File is Converted To XPS");
                        try
                        {
                            DBconnect dBconnect = new DBconnect();
                            dBconnect.modifyLegalDoc(xpsFilePath, "Establishment");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message.ToString());
                        }
                        break;
                    case ConversionResult.InvalidFilePath:
                        System.Windows.MessageBox.Show("Error happened when handling bad file path or file missing");
                        break;
                    case ConversionResult.ErrorUnableToInitializeOfficeApp:
                        System.Windows.MessageBox.Show("Error happened when handling Office 2007 (Word | Excel | PowerPoint) not installed");
                        break;
                    case ConversionResult.ErrorUnableToOpenOfficeFile:
                        System.Windows.MessageBox.Show("Error happened when handling source file being locked or invalid permissions");
                        break;
                    case ConversionResult.ErrorUnableToAccessOfficeInterop:
                        System.Windows.MessageBox.Show("Error happened when handling Office 2007 (Word | Excel | PowerPoint) not installed");
                        break;
                    case ConversionResult.ErrorUnableToExportToXps:
                        System.Windows.MessageBox.Show("Error happened when handling Microsoft Save As PDF or XPS Add-In missing for 2007");
                        break;
                    default:
                        System.Windows.MessageBox.Show("Couldn't Convert doc file to xps format");
                        break;
                }
            }
        }
        private void ViewRulesDoc_Click(object sender, RoutedEventArgs e)
        {
            ViewRulesDocPage viewRulesDocPage = new ViewRulesDocPage();
            this.NavigationService.Navigate(viewRulesDocPage);
        }
        private void UpdateRulesDoc_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
            string xpsFilePath = String.Empty;
            openFileDialog.DefaultExt = ".txt";
            openFileDialog.Filter = "Word Files(*.docx;*.doc)|*.docx;*.doc";
            Nullable<bool> result = openFileDialog.ShowDialog();
            if (result == true)
            {
                string filename = openFileDialog.FileName;
                xpsFilePath = Environment.CurrentDirectory.ToString() + "\\" + openFileDialog.SafeFileName + ".xps";
                var convertResults = OfficeToXps.ConvertToXps(filename, ref xpsFilePath);
                switch (convertResults.Result)
                {
                    case ConversionResult.OK:
                        xpsDoc = new System.Windows.Xps.Packaging.XpsDocument(xpsFilePath, FileAccess.ReadWrite);
                        if(xpsDoc!=null) xpsDoc.Close();
                        System.Windows.MessageBox.Show("File is Converted To XPS");
                        try
                        {
                            DBconnect dBconnect = new DBconnect();
                            dBconnect.modifyLegalDoc(xpsFilePath, "Rules");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message.ToString());
                        }
                        break;
                    case ConversionResult.InvalidFilePath:
                        System.Windows.MessageBox.Show("Error happened when handling bad file path or file missing");
                        break;
                    case ConversionResult.ErrorUnableToInitializeOfficeApp:
                        System.Windows.MessageBox.Show("Error happened when handling Office 2007 (Word | Excel | PowerPoint) not installed");
                        break;
                    case ConversionResult.ErrorUnableToOpenOfficeFile:
                        System.Windows.MessageBox.Show("Error happened when handling source file being locked or invalid permissions");
                        break;
                    case ConversionResult.ErrorUnableToAccessOfficeInterop:
                        System.Windows.MessageBox.Show("Error happened when handling Office 2007 (Word | Excel | PowerPoint) not installed");
                        break;
                    case ConversionResult.ErrorUnableToExportToXps:
                        System.Windows.MessageBox.Show("Error happened when handling Microsoft Save As PDF or XPS Add-In missing for 2007");
                        break;
                    default:
                        System.Windows.MessageBox.Show("Couldn't Convert doc file to xps format");
                        break;
                }
            }
        }
    }

}
