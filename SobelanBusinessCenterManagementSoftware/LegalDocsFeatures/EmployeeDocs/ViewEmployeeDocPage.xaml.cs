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
        private void HomeButton_MouseLeftButtonUp(object sender, RoutedEventArgs e)
        {
            HomePage homePage = new HomePage();
            this.NavigationService.Navigate(homePage);
        }
        private void HomeButton_PanelEntered(object sender, MouseEventArgs e)
        {
            HomeButtonPanel.Margin = new Thickness(0, 0, 0, 0);
        }
        private void HomeButton_PanelLeft(object sender, MouseEventArgs e)
        {
            HomeButtonPanel.Margin = new Thickness(0, 0, 120, 0);
        }

        private void ShopsButton_MouseLeftButtonUp(object sender, RoutedEventArgs e)
        {
            ShopsPage shopsPage = new ShopsPage();
            this.NavigationService.Navigate(shopsPage);
        }
        private void ShopsButton_PanelEntered(object sender, MouseEventArgs e)
        {
            ShopsButtonPanel.Margin = new Thickness(0, 0, 0, 0);
        }
        private void ShopsButton_PanelLeft(object sender, MouseEventArgs e)
        {
            ShopsButtonPanel.Margin = new Thickness(0, 0, 120, 0);
        }

        private void EmployeesButton_MouseLeftButtonUp(object sender, RoutedEventArgs e)
        {
            EmployeesPage employeesPage = new EmployeesPage();
            this.NavigationService.Navigate(employeesPage);
        }
        private void EmployeesButton_PanelEntered(object sender, MouseEventArgs e)
        {
            EmployeesButtonPanel.Margin = new Thickness(0, 0, 0, 0);
        }
        private void EmployeesButton_PanelLeft(object sender, MouseEventArgs e)
        {
            EmployeesButtonPanel.Margin = new Thickness(0, 0, 120, 0);
        }

        private void LegalDocumentsButton_MouseLeftButtonUp(object sender, RoutedEventArgs e)
        {
            LegalDocumentsPage legalDocumentsPage = new LegalDocumentsPage();
            this.NavigationService.Navigate(legalDocumentsPage);
        }
        private void LegalDocumentsButton_PanelEntered(object sender, MouseEventArgs e)
        {
            LegalDocumentsPanel.Margin = new Thickness(0, 0, 0, 0);
        }
        private void LegalDocumentsButton_PanelLeft(object sender, MouseEventArgs e)
        {
            LegalDocumentsPanel.Margin = new Thickness(0, 0, 120, 0);
        }

        private void AboutUsButton_MouseLeftButtonUp(object sender, RoutedEventArgs e)
        {
            AboutUsPage aboutUsPage = new AboutUsPage();
            this.NavigationService.Navigate(aboutUsPage);
        }
        private void AboutUsButton_PanelEntered(object sender, MouseEventArgs e)
        {
            AboutButtonPanel.Margin = new Thickness(0, 0, 0, 0);
        }
        private void AboutUsButton_PanelLeft(object sender, MouseEventArgs e)
        {
            AboutButtonPanel.Margin = new Thickness(0, 0, 120, 0);
        }
        private void BackOut_Click(object sender, RoutedEventArgs e)
        {
            if(xpsDoc!=null) xpsDoc.Close();
            EmployeeDocPage employeeDocPage = new EmployeeDocPage();
            this.NavigationService.Navigate(employeeDocPage);
        }
    }
}
