using System;
using System.Collections.Generic;
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
    /// Interaction logic for LegalDocumentsPage.xaml
    /// </summary>
    public partial class LegalDocumentsPage : Page
    {
        public LegalDocumentsPage()
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

        // Options Buttons
        private void RentalDocButton_Click(object sender, RoutedEventArgs e)
        {
            RentalDocPage rentalDocPage = new RentalDocPage();
            this.NavigationService.Navigate(rentalDocPage);
        }
        private void EmployeeDocButton_Click(object sender, RoutedEventArgs e)
        {
            EmployeeDocPage employeeDocPage = new EmployeeDocPage();
            this.NavigationService.Navigate(employeeDocPage);
        }
        private void OtherDoc_Click(object sender, RoutedEventArgs e)
        {
            OtherDocPage otherDocPage = new OtherDocPage();
            this.NavigationService.Navigate(otherDocPage);
        }
        private void ModifyLegalDocsButton_Click(object sender, RoutedEventArgs e)
        {
            ModifyLegalDocsPage modifyLegalDocsPage = new ModifyLegalDocsPage();
            this.NavigationService.Navigate(modifyLegalDocsPage);
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

    }
}
