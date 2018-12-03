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

using MySql.Data;
using MySql.Data.MySqlClient;
using System.Data;

namespace SBCManagementSoftware
{
    /// <summary>
    /// Interaction logic for ViewAllEmployeesPage.xaml
    /// </summary>
    public partial class ViewAllEmployeesPage : Page
    {
        public ViewAllEmployeesPage()
        {
            InitializeComponent();
            DBconnect dBconnect = new DBconnect();
            DataSet dataSet = dBconnect.GetAllEmployees();
            DataGrid.DataContext = dataSet;
            dBconnect.CloseConnection();
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

        private void GoToAddAnEmployee_Click(object sender, RoutedEventArgs e)
        {
            AddAnEmployeePage addAnEmployeePage = new AddAnEmployeePage();
            this.NavigationService.Navigate(addAnEmployeePage);
        }
        private void GoToModifyAnEmployee_Click(object sender, RoutedEventArgs e)
        {
            if (DataGrid.SelectedIndex==-1)
            {
                MessageBox.Show("Please Select An Employee To Modify It.","Employee Not Selected");
                return;
            }
            else
            {
                DataGridCellInfo dataGridCellInfo = DataGrid.CurrentCell;
                int i = DataGrid.SelectedIndex;
                Object[] vs = ((DataRowView)DataGrid.Items[i]).Row.ItemArray;
                DateTime dateTime = (DateTime)vs[0];
                String FirstName = (String)vs[1];
                String LastName = (String)vs[2];
                String Phone = (String)vs[5];
                int Salary = (int)vs[4];
                String Position = (String)vs[3];
                ModifyEmployeesPage modifyEmployeesPage = new ModifyEmployeesPage(dateTime,FirstName, LastName, Phone, Salary, Position);
                this.NavigationService.Navigate(modifyEmployeesPage);
            }
        }
        private void GoToRemoveAnEmployee_Click(object sender, RoutedEventArgs e)
        {
            if (DataGrid.SelectedIndex == -1)
            {
                MessageBox.Show("Please Select An Employee To Modify It.", "Employee Not Selected");
                return;
            }
            else
            {
                if((MessageBox.Show("Are you sure you want to remove this employee from your list? ", "Remove Employee", MessageBoxButton.YesNoCancel, MessageBoxImage.Exclamation))==MessageBoxResult.Yes){
                    DataGridCellInfo dataGridCellInfo = DataGrid.CurrentCell;
                    int i = DataGrid.SelectedIndex;
                    Object[] vs = ((DataRowView)DataGrid.Items[i]).Row.ItemArray;
                    DateTime dateTime = (DateTime)vs[0];
                    DBconnect dBconnect = new DBconnect();
                    dBconnect.RemoveEmployee(dateTime);
                    MessageBox.Show("Employee is removed", "Removed Employee");
                    ViewAllEmployeesPage viewAllEmployeesPage = new ViewAllEmployeesPage();
                    this.NavigationService.Navigate(viewAllEmployeesPage);
                }
                else
                {
                    return;
                }

            }
        }
    }
}
