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
using System.Data;
using MySql.Data;
using MySql.Data.MySqlClient;
    
namespace SBCManagementSoftware
{
    /// <summary>
    /// Interaction logic for ViewAllShopsPage.xaml
    /// </summary>
    public partial class ViewAllShopsPage : Page
    {
        public ViewAllShopsPage()
        {
            InitializeComponent();
            DBconnect dBconnect = new DBconnect();
            DataSet dataSet = dBconnect.GetAllShops();
            DataGrid.DataContext = dataSet;
            dBconnect.CloseConnection();
        }
        private void ShopsButton_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            ShopsPage shopsPage = new ShopsPage();
            this.NavigationService.Navigate(shopsPage);
        }
        // Main Nav Bar Buttons
        private void HomeButton_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            HomePage homePage = new HomePage();
            this.NavigationService.Navigate(homePage);
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
        private void GoToClearOutShop_Click(object sender, RoutedEventArgs e)
        {
            if (DataGrid.SelectedIndex == -1)
            {
                MessageBox.Show("Please Select A Shop To Rent It.", "Shop Not Selected");
                return;
            }
            else
            {
                DataGridCellInfo dataGridCellInfo = DataGrid.CurrentCell;
                int i = DataGrid.SelectedIndex;
                Object[] vs = ((DataRowView)DataGrid.Items[i]).Row.ItemArray;
                int ID = (int)vs[0];
                sbyte Status = (sbyte)vs[2];
                if (Status == 0)
                {
                    MessageBox.Show("Please Select Another Shop To Clear Out. This One is already Cleared Out.", "Shop is Already Cleared Out.");
                    return;
                }
                else
                {
                    DBconnect dBconnect = new DBconnect();
                    dBconnect.ClearOutShop(ID);
                    MessageBox.Show("Shop is Clear Out", "Cleared Out Shop");
                    ViewAllShopsPage viewAllShopsPage = new ViewAllShopsPage();
                    this.NavigationService.Navigate(viewAllShopsPage);
                }
            }
        }

        private void GoToModifyShop_Click(object sender, RoutedEventArgs e)
        {
            if (DataGrid.SelectedIndex == -1)
            {
                MessageBox.Show("Please Select A Shop To Modify It.", "Shop Not Selected");
                return;
            }
            else
            {
                DataGridCellInfo dataGridCellInfo = DataGrid.CurrentCell;
                int i = DataGrid.SelectedIndex;
                Object[] vs = ((DataRowView)DataGrid.Items[i]).Row.ItemArray;
                int ID = (int)vs[0];
                sbyte Status = (sbyte)vs[2];
                if (Status == 1)
                {
                    MessageBox.Show("Please Select Another Shop To Modify. This One is already rented out. You can not enter negotiations while a shop is already in use.", "Shop is Already Rented Out.");
                    return;
                }
                else
                {
                    ModifyShopsListPage modifyShopsListPage = new ModifyShopsListPage(ID);
                    this.NavigationService.Navigate(modifyShopsListPage);

                }

            }
        }

        private void GoToRentOutShop_Click(object sender, RoutedEventArgs e)
        {
            if (DataGrid.SelectedIndex == -1)
            {
                MessageBox.Show("Please Select A Shop To Rent It.", "Shop Not Selected");
                return;
            }
            else
            {
                DataGridCellInfo dataGridCellInfo = DataGrid.CurrentCell;
                int i = DataGrid.SelectedIndex;
                Object[] vs = ((DataRowView)DataGrid.Items[i]).Row.ItemArray;
                int ID = (int)vs[0];
                sbyte Status = (sbyte)vs[2];
                if (Status == 1)
                {
                    MessageBox.Show("Please Select Another Shop To Rent It. This One is already Rented Out.", "Shop is Already Rented Out.");
                    return;
                }
                else
                {
                    RentOutAShopPage rentOutAShopPage = new RentOutAShopPage(ID);
                    this.NavigationService.Navigate(rentOutAShopPage);
                }
            }

        }
    }
}
