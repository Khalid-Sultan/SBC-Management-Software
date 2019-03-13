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
    /// Interaction logic for ViewAllEmployeesPage.xaml
    /// </summary>
    public partial class ViewAllEmployeesPage : Page
    {
        DataSet dataSet;
        public ViewAllEmployeesPage()
        {
            InitializeComponent();
            try
            {
                DBconnect dBconnect = new DBconnect();
                this.dataSet = dBconnect.GetAllEmployees();
                DataGrid.DataContext = dataSet;
                dBconnect.CloseConnection();
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
        private void GoToAddAnEmployee_Click(object sender, RoutedEventArgs e)
        {
            AddAnEmployeePage addAnEmployeePage = new AddAnEmployeePage();
            this.NavigationService.Navigate(addAnEmployeePage);
        }
        private void GoToDetailedEmployee_Click(object sender, RoutedEventArgs e)
        {
            DataSet newDataSet = new DataSet();
            DataTable details = newDataSet.Tables.Add("LoadDataBinding");
            details.Columns.Add("ID");
            details.Columns.Add("FirstName");
            details.Columns.Add("LastName");
            details.Columns.Add("Salary");
            details.Columns.Add("Position");
            details.Columns.Add("Region");
            details.Columns.Add("SubCity");
            details.Columns.Add("Phone");
            DataColumn dataColumn = new DataColumn("Image")
            {
                DataType = Type.GetType("System.Object")
            };
            details.Columns.Add(dataColumn);

            DataTable dataTable = dataSet.Tables[0];
            DataRow currentRow = null;
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                currentRow = dataTable.Rows[i];
                DateTime dateTime = (DateTime)currentRow[0];
                String FirstName = (String)currentRow[1];
                String LastName = (String)currentRow[2];
                String Phone = (String)currentRow[5];
                int Salary = (int)currentRow[4];
                String Position = (String)currentRow[3];
                string region = (string)currentRow[6];
                string subCity = (string)currentRow[7];
                byte[] employeeImageByte = (byte[])currentRow[8];
                MemoryStream strm = new MemoryStream();
                strm.Write(employeeImageByte, 0, employeeImageByte.Length);
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
                details.Rows.Add( dateTime, FirstName, LastName, Salary, Position, Phone, region, subCity,(object)imageBrush);
            }
            ViewAllEmployeesDetailsPage viewAllEmployeesDetailsPage = new ViewAllEmployeesDetailsPage(newDataSet);
            this.NavigationService.Navigate(viewAllEmployeesDetailsPage);
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
                try
                {
                    int i = DataGrid.SelectedIndex;
                    Object[] vs = ((DataRowView)DataGrid.Items[i]).Row.ItemArray;
                    DateTime dateTime = (DateTime)vs[0];
                    String FirstName = (String)vs[1];
                    String LastName = (String)vs[2];
                    String Phone = (String)vs[5];
                    int Salary = (int)vs[4];
                    String Position = (String)vs[3];
                    string region = (string)vs[6];
                    string subCity = (string)vs[7];

                    byte[] employeeImageByte = (byte[])vs[8];
                    MemoryStream strm = new MemoryStream();
                    strm.Write(employeeImageByte, 0, employeeImageByte.Length);
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
                    ModifyEmployeesPage modifyEmployeesPage = new ModifyEmployeesPage(dateTime, FirstName, LastName, Phone, Salary, Position,region,subCity,imageBrush);
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
                        DataGridCellInfo dataGridCellInfo = DataGrid.CurrentCell;
                        int i = DataGrid.SelectedIndex;
                        Object[] vs = ((DataRowView)DataGrid.Items[i]).Row.ItemArray;
                        if ((MessageBox.Show("Are you sure you want to remove this employee from your list? ", "Remove Employee", MessageBoxButton.YesNoCancel, MessageBoxImage.Exclamation)) == MessageBoxResult.Yes)
                        {
                            try
                            {
                                DateTime dateTime = (DateTime)vs[0];
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

        private void Selected(object sender, MouseButtonEventArgs e)
        {
            DBconnect dBconnect = new DBconnect();
            int i = DataGrid.SelectedIndex;
            try
            {
                Object[] vs = ((DataRowView)DataGrid.Items[i]).Row.ItemArray;
                byte[] employeeImageByte = (byte[])vs[8];
                if (employeeImageByte.Length > 0)
                {
                    MemoryStream strm = new MemoryStream();
                    strm.Write(employeeImageByte, 0, employeeImageByte.Length);
                    strm.Position = 0;
                    System.Drawing.Image img = System.Drawing.Image.FromStream(strm);
                    BitmapImage bi = new BitmapImage();
                    bi.BeginInit();
                    MemoryStream ms = new MemoryStream();
                    img.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
                    ms.Seek(0, SeekOrigin.Begin);
                    bi.StreamSource = ms;
                    bi.EndInit();
                    EmployeeImage.Background.SetValue(ImageBrush.ImageSourceProperty, bi);

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
