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
using System.IO;
using Microsoft.Win32;
using System.Drawing.Imaging;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace SBCManagementSoftware
{
    /// <summary>
    /// Interaction logic for ViewAllShopsDetailedPage.xaml
    /// </summary>
    public partial class ViewAllShopsDetailedPage : Page
    {
        DataSet dataSet;
        public ViewAllShopsDetailedPage(DataSet dataSet)
        {
            InitializeComponent();
            try
            {
                DBconnect dBconnect = new DBconnect();
                this.dataSet = dataSet;
                DataGrid.DataContext = dataSet;
                dBconnect.CloseConnection();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
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

        private void GoToClearOutShop_Click(object sender, RoutedEventArgs e)
        {
            if (DataGrid.SelectedIndex == -1)
            {
                MessageBox.Show("Please Select A Shop To Rent It.", "Shop Not Selected");
                return;
            }
            else
            {
                try { 
                    int i = DataGrid.SelectedIndex;
                    Object[] vs = ((DataRowView)DataGrid.Items[i]).Row.ItemArray;
                    int floor = Convert.ToInt32(vs[0]);
                    int ID = Convert.ToInt32(vs[1]);
                    int Status = Convert.ToInt32(vs[2]);
                    if (Status == 0)
                    {
                        MessageBox.Show("Please Select Another Shop To Clear Out. This One is already Cleared Out.", "Shop is Already Cleared Out.");
                        return;
                    }
                    else
                    {
                        DBconnect dBconnect = new DBconnect();
                        dBconnect.ClearOutShop(floor,ID);
                        MessageBox.Show("Shop is Clear Out", "Cleared Out Shop");
                        ViewAllShopsPage viewAllShopsPage = new ViewAllShopsPage();
                        this.NavigationService.Navigate(viewAllShopsPage);
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

        private void GoToModifyShop_Click(object sender, RoutedEventArgs e)
        {
            if (DataGrid.SelectedIndex == -1)
            {
                MessageBox.Show("Please Select A Shop To Modify It.", "Shop Not Selected");
                return;
            }
            else
            {
                int i = DataGrid.SelectedIndex;
                try { 
                    Object[] vs = ((DataRowView)DataGrid.Items[i]).Row.ItemArray;
                    int floor = Convert.ToInt32(vs[0]);
                    int ID = Convert.ToInt32(vs[1]);
                    int Status = Convert.ToInt32(vs[2]);
                    int rentAmount = Convert.ToInt32(vs[6]);
                    ImageBrush locationImageByte = (ImageBrush)vs[16];
                    if (Status == 1)
                    {
                        MessageBox.Show("Please Select Another Shop To Modify. This One is already rented out. You can not enter negotiations while a shop is already in use.", "Shop is Already Rented Out.");
                        return;
                    }
                    else
                    {
                        ModifyShopsListPage modifyShopsListPage = new ModifyShopsListPage(ID,floor,rentAmount,locationImageByte);
                        this.NavigationService.Navigate(modifyShopsListPage);

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

        private void GoToRentOutShop_Click(object sender, RoutedEventArgs e)
        {
            if (DataGrid.SelectedIndex == -1)
            {
                MessageBox.Show("Please Select A Shop To Rent It.", "Shop Not Selected");
                return;
            }
            else
            {
                int i = DataGrid.SelectedIndex;
                try { 
                    Object[] vs = ((DataRowView)DataGrid.Items[i]).Row.ItemArray;
                    int floor = Convert.ToInt32(vs[0]);
                    int ID = Convert.ToInt32(vs[1]);
                    int Status = Convert.ToInt32(vs[2]);
                    int rentAmount = Convert.ToInt32(vs[6]);
                    if (Status == 1)
                    {
                        MessageBox.Show("Please Select Another Shop To Rent It. This One is already Rented Out.", "Shop is Already Rented Out.");
                        return;
                    }
                    else
                    {
                        RentOutAShopPage rentOutAShopPage = new RentOutAShopPage(ID,floor, rentAmount);
                        this.NavigationService.Navigate(rentOutAShopPage);
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
        private void ExtendRent_Click(object sender, RoutedEventArgs e)
        {
            if (DataGrid.SelectedIndex == -1)
            {
                MessageBox.Show("Please Select A Shop To Extend its Rent.", "Shop Not Selected");
                return;
            }
            else
            {
                int i = DataGrid.SelectedIndex;
                try { 
                    Object[] vs = ((DataRowView)DataGrid.Items[i]).Row.ItemArray;
                    int floor = Convert.ToInt32(vs[0]);
                    int ID = Convert.ToInt32(vs[1]);
                    int Status = Convert.ToInt32(vs[2]);
                    int rentAmount = int.Parse(ResponseTextBox.Text);
                    DateTime dateTime = (DateTime)vs[4];
                    DateTime finalDate = dateTime.AddMonths(rentAmount);
                    if (rentAmount <= 3)
                    {
                        rentAmount += (int)vs[8];
                        if (Status == 0)
                        {
                            MessageBox.Show("Please Select Another Shop To Extend its Rent. This One is not rented out.", "Shop is not rented out.");
                            return;
                        }
                        else
                        {
                            DBconnect dBconnect = new DBconnect();
                            dBconnect.ExtendRent(floor,ID,finalDate,rentAmount);
                            DataSet dataSet = dBconnect.GetAllShops();
                            DataGrid.DataContext = dataSet;
                            dBconnect.CloseConnection();
                        }
                    }
                    else
                    {
                        MessageBox.Show("You can't extend your rent unless you are 3 months short.");
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
        private void Selected(object sender, MouseButtonEventArgs e)
        {
            try
            {
                DBconnect dBconnect = new DBconnect();
                int i = DataGrid.SelectedIndex;
                Object[] vs = ((DataRowView)DataGrid.Items[i]).Row.ItemArray;
                int Status = Convert.ToInt32(vs[2]);
                if (Status == 1)
                {
                    ExtensionMonths.Visibility = Visibility.Visible;
                    ResponseTextBox.Visibility = Visibility.Visible;
                    ExtendRentButton.Visibility = Visibility.Visible;
                    RentOutButton.Visibility = Visibility.Hidden;
                }
                else
                {
                    ExtensionMonths.Visibility = Visibility.Hidden;
                    ResponseTextBox.Visibility = Visibility.Hidden;
                    ExtendRentButton.Visibility = Visibility.Hidden;
                    RentOutButton.Visibility = Visibility.Visible;
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
