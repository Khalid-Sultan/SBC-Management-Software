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
    /// Interaction logic for ModifyEmployeesPage.xaml
    /// </summary>
    public partial class ModifyEmployeesPage : Page
    {
        String dateTime;
        public ModifyEmployeesPage()
        {
            InitializeComponent();
            FirstNameBox.Text = "Unknown";
            LastNameBox.Text = "Unknown";
            PhoneNumberBox.Text = "Unknown";
            SalaryBox.Text = "Unknown";
        }
        public ModifyEmployeesPage(DateTime dateTime,String FirstName, String LastName, String PhoneNumber, int Salary, String Position)
        {
            InitializeComponent();
            this.dateTime = dateTime.ToString("yyyy-MM-dd HH:mm:ss") ;
            FirstNameBox.Text = FirstName;
            LastNameBox.Text = LastName;
            PhoneNumberBox.Text = PhoneNumber;
            SalaryBox.Text = $"{Salary}";
            PositionBox.Text = Position;
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
        private void LegalDocumentsButton_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            LegalDocumentsPage legalDocumentsPage = new LegalDocumentsPage();
            this.NavigationService.Navigate(legalDocumentsPage);
        }
        private void AboutUsButton_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            AboutUsPage aboutUsPage = new AboutUsPage();
            this.NavigationService.Navigate(aboutUsPage);
        }
        private void ModifyButton_Click(object sender, RoutedEventArgs e)
        {
            DBconnect dBconnect = new DBconnect();
            String firstName = FirstNameBox.Text;
            String lastName = LastNameBox.Text;
            String phoneNumber = PhoneNumberBox.Text;
            String salary = SalaryBox.Text;
            String position = PositionBox.Text;
            dBconnect.ModifyEmployee(dateTime,firstName, lastName, phoneNumber, salary, position);
            EmployeesPage employeesPage = new EmployeesPage();
            this.NavigationService.Navigate(employeesPage);

        }

    }
}
