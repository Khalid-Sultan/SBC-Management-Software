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
using System.IO;

namespace SBCManagementSoftware
{
    /// <summary>
    /// Interaction logic for ViewAllEmployeesDetailsPage.xaml
    /// </summary>
    public partial class ViewAllEmployeesDetailsPage : Page
    {
        DataSet dataSet;
        public ViewAllEmployeesDetailsPage(DataSet dataSet)
        {
            InitializeComponent();
            this.dataSet = dataSet;
            DataGrid.DataContext = dataSet;
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
        private void GoToAddAnEmployee_Click(object sender, RoutedEventArgs e)
        {
            AddAnEmployeePage addAnEmployeePage = new AddAnEmployeePage();
            this.NavigationService.Navigate(addAnEmployeePage);
        }
        private void GoToModifyAnEmployee_Click(object sender, RoutedEventArgs e)
        {
            if (DataGrid.SelectedIndex == -1)
            {
                MessageBox.Show("Please Select An Employee To Modify It.", "Employee Not Selected");
                return;
            }
            else
            {
                try
                {
                    int i = DataGrid.SelectedIndex;
                    Object[] vs = ((DataRowView)DataGrid.Items[i]).Row.ItemArray;
                    DateTime dateTime = Convert.ToDateTime(vs[0]);
                    String FirstName = Convert.ToString(vs[1]);
                    String LastName = Convert.ToString(vs[2]);
                    int Salary = Convert.ToInt32(vs[3]);
                    String Position = Convert.ToString(vs[4]);
                    String Phone = Convert.ToString(vs[5]);
                    string region = Convert.ToString(vs[6]);
                    string subCity = Convert.ToString(vs[7]);

                    ImageBrush employeeImageByte = (ImageBrush)vs[8];

                    ModifyEmployeesPage modifyEmployeesPage = new ModifyEmployeesPage(dateTime, FirstName, LastName, Phone, Salary, Position, region, subCity, employeeImageByte);
                    this.NavigationService.Navigate(modifyEmployeesPage);

                }
                catch (ArgumentOutOfRangeException)
                {
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
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
                try
                {
                    int i = DataGrid.SelectedIndex;
                    Object[] vs = ((DataRowView)DataGrid.Items[i]).Row.ItemArray;
                    if ((MessageBox.Show("Are you sure you want to remove this employee from your list? ", "Remove Employee", MessageBoxButton.YesNoCancel, MessageBoxImage.Exclamation)) == MessageBoxResult.Yes)
                    {
                        try
                        {
                            DateTime dateTime = Convert.ToDateTime(vs[0]);
                            DBconnect dBconnect = new DBconnect();
                            dBconnect.RemoveEmployee(dateTime);
                            MessageBox.Show("Employee is removed", "Removed Employee");
                            ViewAllEmployeesPage viewAllEmployeesPage = new ViewAllEmployeesPage();
                            this.NavigationService.Navigate(viewAllEmployeesPage);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message.ToString());
                        }
                    }
                    else
                    {
                        return;
                    }
                }
                catch (ArgumentOutOfRangeException)
                {
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }

            }
        }
    }
}
