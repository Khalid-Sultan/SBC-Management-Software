using Microsoft.Win32;
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
using System.Windows.Forms;

namespace SBCManagementSoftware
{
    /// <summary>
    /// Interaction logic for ModifyLegalDocsPage.xaml
    /// </summary>
    public partial class ModifyLegalDocsPage : Page
    {
        public static bool officeFileOpen_Status = false;

        public ModifyLegalDocsPage()
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
        private void Button_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            TabItem tabItem = (TabItem)sender;
            tabItem.Foreground = System.Windows.Media.Brushes.Black;
        }

        private void Button_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            TabItem tabItem = (TabItem)sender;
            tabItem.Foreground = System.Windows.Media.Brushes.White;
        }

        private void ModifyRentalDoc_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DBconnect dBconnect = new DBconnect();
                dBconnect.modifyLegalDocAndReplace("RestoreRental", "Rental");
                System.Windows.MessageBox.Show("Successfully Restored");
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message.ToString());
            }
        }
        private void ModifyEmployeeDoc_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DBconnect dBconnect = new DBconnect();
                dBconnect.modifyLegalDocAndReplace("RestoreEmployee", "Employee");
                System.Windows.MessageBox.Show("Successfully Restored");
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message.ToString());
            }
        }
        private void ModifyEstablishmentDoc_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DBconnect dBconnect = new DBconnect();
                dBconnect.modifyLegalDocAndReplace("RestoreEstablishment", "Establishment");
                System.Windows.MessageBox.Show("Successfully Restored");
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message.ToString());
            }
        }
        private void ModifyRulesDoc_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DBconnect dBconnect = new DBconnect();
                dBconnect.modifyLegalDocAndReplace("RestoreRules", "Rules");
                System.Windows.MessageBox.Show("Successfully Restored");
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message.ToString());
            }
        }

    }
}
