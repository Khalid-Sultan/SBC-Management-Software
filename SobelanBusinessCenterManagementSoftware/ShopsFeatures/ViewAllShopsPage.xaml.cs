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
    /// Interaction logic for ViewAllShopsPage.xaml
    /// </summary>
    public partial class ViewAllShopsPage : Page
    {
        DataSet dataSet;
        public ViewAllShopsPage()
        {
            InitializeComponent();
            try
            {
                DBconnect dBconnect = new DBconnect();
                this.dataSet = dBconnect.GetAllShops();
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
                try
                {
                    int i = DataGrid.SelectedIndex;
                    Object[] vs = ((DataRowView)DataGrid.Items[i]).Row.ItemArray;
                    int Floor = (int)vs[0];
                    int iD = (int)vs[1];
                    int Status = (int)vs[3];
                    if (Status == 0)
                    {
                        MessageBox.Show("Please Select Another Shop To Clear Out. This One is already Cleared Out.", "Shop is Already Cleared Out.");
                        return;
                    }
                    else
                    {
                        DBconnect dBconnect = new DBconnect();
                        dBconnect.ClearOutShop(Floor, iD);
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
                try
                {
                    int i = DataGrid.SelectedIndex;
                    Object[] vs = ((DataRowView)DataGrid.Items[i]).Row.ItemArray;
                    int floor = (int)vs[0];
                    int ID = (int)vs[1];
                    byte[] locationImageByte = (byte[])vs[2];
                    int Status = (int)vs[3];
                    int rentAmount = (int)vs[7];

                    MemoryStream strm = new MemoryStream();
                    strm.Write(locationImageByte, 0, locationImageByte.Length);
                    strm.Position = 0;
                    System.Drawing.Image img = System.Drawing.Image.FromStream(strm);
                    BitmapImage bi = new BitmapImage();
                    bi.BeginInit();
                    MemoryStream ms = new MemoryStream();
                    img.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
                    ms.Seek(0, SeekOrigin.Begin);
                    bi.StreamSource = ms;
                    bi.EndInit();

                    if (Status == 1)
                    {
                        MessageBox.Show("Please Select Another Shop To Modify. This One is already rented out. You can not enter negotiations while a shop is already in use.", "Shop is Already Rented Out.");
                        return;
                    }
                    else
                    {
                        ModifyShopsListPage modifyShopsListPage = new ModifyShopsListPage(ID, floor, rentAmount, bi);
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
                try
                {
                    int i = DataGrid.SelectedIndex;
                    Object[] vs = ((DataRowView)DataGrid.Items[i]).Row.ItemArray;
                    int floor = (int)vs[0];
                    int ID = (int)vs[1];
                    int Status = (int)vs[3];
                    int rentAmount = (int)vs[7];
                    if (Status == 1)
                    {
                        MessageBox.Show("Please Select Another Shop To Rent It. This One is already Rented Out.", "Shop is Already Rented Out.");
                        return;
                    }
                    else
                    {
                        RentOutAShopPage rentOutAShopPage = new RentOutAShopPage(ID, floor, rentAmount);
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
                try
                {
                    int i = DataGrid.SelectedIndex;
                    Object[] vs = ((DataRowView)DataGrid.Items[i]).Row.ItemArray;
                    int floor = (int)vs[0];
                    int ID = (int)vs[1];
                    int Status = (int)vs[3];
                    int rentAmount = int.Parse(ResponseTextBox.Text);
                    DateTime dateTime = (DateTime)vs[5];
                    DateTime finalDate = dateTime.AddMonths(rentAmount);
                    if (rentAmount <= 3)
                    {

                        rentAmount += (int)vs[6];
                        if (Status == 0)
                        {
                            MessageBox.Show("Please Select Another Shop To Extend its Rent. This One is not rented out.", "Shop is not rented out.");
                            return;
                        }
                        else
                        {
                            DBconnect dBconnect = new DBconnect();
                            dBconnect.ExtendRent(floor, ID, finalDate, rentAmount);
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
            DBconnect dBconnect = new DBconnect();
            int i = DataGrid.SelectedIndex;
            try
            {
                Object[] vs = ((DataRowView)DataGrid.Items[i]).Row.ItemArray;
                int Status = (int)vs[3];
                byte[] locationImageByte = (byte[])vs[2];
                ShopLocation.Visibility = Visibility.Visible;
                LocationImage.Visibility = Visibility.Visible;
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
                if (locationImageByte.Length > 0)
                {
                    MemoryStream strm = new MemoryStream();
                    strm.Write(locationImageByte, 0, locationImageByte.Length);
                    strm.Position = 0;
                    System.Drawing.Image img = System.Drawing.Image.FromStream(strm);
                    BitmapImage bi = new BitmapImage();
                    bi.BeginInit();
                    MemoryStream ms = new MemoryStream();
                    img.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
                    ms.Seek(0, SeekOrigin.Begin);
                    bi.StreamSource = ms;
                    bi.EndInit();
                    LocationImage.Background.SetValue(ImageBrush.ImageSourceProperty, bi);

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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DataSet newDataSet = new DataSet();
            DataTable details = newDataSet.Tables.Add("LoadDataBinding");
            details.Columns.Add("Floor");
            details.Columns.Add("ID");
            details.Columns.Add("Status");
            details.Columns.Add("BeginDate");
            details.Columns.Add("EndDate");
            details.Columns.Add("Duration");
            details.Columns.Add("Price");
            details.Columns.Add("Total");
            details.Columns.Add("Tax");
            details.Columns.Add("RenterFirstName");
            details.Columns.Add("RenterLastName");
            details.Columns.Add("Name");
            details.Columns.Add("City");
            details.Columns.Add("SubCity");
            details.Columns.Add("County");
            details.Columns.Add("HouseNumber");
            DataColumn dataColumn = new DataColumn("ShopImage")
            {
                DataType = Type.GetType("System.Object")
            };
            details.Columns.Add(dataColumn);

            DataTable dataTable = dataSet.Tables[0];
            DataRow currentRow = null;
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                currentRow = dataTable.Rows[i];
                /*             0int Floor
                               1int Id
                               2byte[] ShopImage
                               3int Status
                               4DateTime BeginDate
                               5DateTime EndDate
                               6int Duration
                               7int Price
                               8int Total
                               9int Tax
                               10string RenterFirstName
                               11string RenterLastName
                               12string Name
                               13string City
                               14string SubCity
                               15string County
                               16string HouseNumber */
                int Floor = (int)currentRow[0];
                int Id = (int)currentRow[1];
                byte[] ShopImage = (byte[])currentRow[2];
                int Status = (int)currentRow[3];
                DateTime BeginDate, EndDate;
                try
                {
                    BeginDate = (DateTime)currentRow[4];

                }
                catch
                {
                    BeginDate = DateTime.MinValue;
                }
                try
                {
                    EndDate = (DateTime)currentRow[5];

                }
                catch
                {
                    EndDate = DateTime.MinValue;
                }
                int Duration = (int)currentRow[6];
                int Price = (int)currentRow[7];
                int Total = (int)currentRow[8];
                int Tax = (int)currentRow[9];
                string RenterFirstName = (string)currentRow[10];
                string RenterLastName = (string)currentRow[11];
                string Name = (string)currentRow[12];
                string City = (string)currentRow[13];
                string SubCity = (string)currentRow[14];
                string County = (string)currentRow[15];
                string HouseNumber = (string)currentRow[16];

                MemoryStream strm = new MemoryStream();
                strm.Write(ShopImage, 0, ShopImage.Length);
                strm.Position = 0;
                System.Drawing.Image img = System.Drawing.Image.FromStream(strm);
                BitmapImage bi = new BitmapImage();
                bi.BeginInit();
                MemoryStream ms = new MemoryStream();
                img.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
                ms.Seek(0, SeekOrigin.Begin);
                bi.StreamSource = ms;
                bi.EndInit();
                ImageBrush imageBrush = new ImageBrush(bi);
                details.Rows.Add(Floor, Id, Status, BeginDate, EndDate, Duration, Price, Total, Tax, RenterFirstName, RenterLastName, Name, City, SubCity, County, HouseNumber, (object)imageBrush);
            }
            ViewAllShopsDetailedPage viewAllShopsDetailsPage = new ViewAllShopsDetailedPage(newDataSet);
            this.NavigationService.Navigate(viewAllShopsDetailsPage);
        }
    }
}
