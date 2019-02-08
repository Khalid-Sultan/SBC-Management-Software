using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
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
    /// Interaction logic for ViewEmployeeDocPage.xaml
    /// </summary>
    public partial class ViewEmployeeDocPage : Page
    {
        System.Windows.Xps.Packaging.XpsDocument xpsDoc;
        public ViewEmployeeDocPage()
        {
            InitializeComponent();
            try
            {
                DBconnect dBconnect = new DBconnect();
                xpsDoc = dBconnect.getLegalDoc("Employee");
                if (xpsDoc != null)
                {
                    DocViewer.Document = xpsDoc.GetFixedDocumentSequence();
                    DocViewer.FitToWidth();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
        // Main Nav Bar Buttons
        private void HomeButton_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if(xpsDoc!=null) xpsDoc.Close();
            HomePage homePage = new HomePage();
            this.NavigationService.Navigate(homePage);
        }
        private void ShopsButton_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if(xpsDoc!=null) xpsDoc.Close();
            ShopsPage shopsPage = new ShopsPage();
            this.NavigationService.Navigate(shopsPage);
        }
        private void EmployeesButton_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if(xpsDoc!=null) xpsDoc.Close();
            EmployeesPage employeesPage = new EmployeesPage();
            this.NavigationService.Navigate(employeesPage);
        }
        private void AboutUsButton_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if(xpsDoc!=null) xpsDoc.Close();
            AboutUsPage aboutUsPage = new AboutUsPage();
            this.NavigationService.Navigate(aboutUsPage);
        }
        private void LegalDocumentsButton_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if(xpsDoc!=null) xpsDoc.Close();
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

        private void BackOut_Click(object sender, RoutedEventArgs e)
        {
            if(xpsDoc!=null) xpsDoc.Close();
            EmployeeDocPage employeeDocPage = new EmployeeDocPage();
            this.NavigationService.Navigate(employeeDocPage);
        }
    }
}
