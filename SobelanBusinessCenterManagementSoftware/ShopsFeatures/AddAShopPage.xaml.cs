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

namespace SBCManagementSoftware
{
    /// <summary>
    /// Interaction logic for ModifyShopsListPage.xaml
    /// </summary>
    public partial class AddAShopPage : Page
    {
        string name, imageName;
        public AddAShopPage()
        {
            InitializeComponent();
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
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (imageName != "")
                {
                    FileStream fileStream = new FileStream(imageName, FileMode.Open, FileAccess.Read);
                    byte[] imageByte = new byte[fileStream.Length];
                    fileStream.Read(imageByte,0,System.Convert.ToInt32(fileStream.Length));
                    fileStream.Close();

                    int floor = 0;
                    int id = 0; 
                    int rentAmount = 0;
                    if (check(FloorBox.Text, IDBox.Text, RentAmountBox.Text, imageByte, ref floor, ref id, ref rentAmount) == 1)
                    {
                        DBconnect dBconnect = new DBconnect();
                        dBconnect.AddShop(floor, id, imageByte, rentAmount);

                        MessageBox.Show("Shop is added", "Added Shop");
                        ViewAllShopsPage viewAllShopsPage = new ViewAllShopsPage();
                        this.NavigationService.Navigate(viewAllShopsPage);
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }

        }

        private int check(String floorBoxText, String IDBoxText,String RentBoxText, byte[] imageByte, ref int floor, ref int iD,ref int rentAmount)
        {
            if( (floorBoxText.Length>0) && (IDBoxText.Length>0) && (RentBoxText.Length > 0))
            {
                if (int.TryParse(floorBoxText, out floor))
                {
                    if (int.TryParse(IDBoxText, out iD))
                    {
                        if (int.TryParse(RentBoxText, out rentAmount))
                        {
                            if ((floor >= 0) && (floor <= 5))
                            {
                                if ((rentAmount >= 5000))
                                {
                                    if ((imageByte.Length > 0))
                                    {
                                        return 1;
                                    }
                                    else
                                    {
                                        MessageBox.Show("A shop should have a location.");
                                        return 0;
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("A shop can only be rented for a value exceeding 5000 birrs.");
                                    return 0;
                                }
                            }
                            else
                            {
                                MessageBox.Show("Floors are numbered from 0 to 5.");
                                return 0;
                            }
                        }
                        else
                        {
                            MessageBox.Show("Invalid Characters were in Amount input.");
                            return 0;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Invalid Characters were in ID input.");
                        return 0;
                    }
                }
                else
                {
                    MessageBox.Show("Invalid Characters were in floor input.");
                    return 0;
                }
            }
            else
            {
                MessageBox.Show("A field is left empty.");
                return 0;
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                FileDialog fileDialog = new OpenFileDialog
                {
                    InitialDirectory = Environment.SpecialFolder.MyPictures.ToString(),
                    Filter = "Image File (*.jpg; *.jpeg)| *.jpg; *.jpeg",
                    DefaultExt = ".jpeg"
                };
                fileDialog.ShowDialog();
                {
                    name = fileDialog.SafeFileName;
                    imageName = fileDialog.FileName;
                    ImageSourceConverter imageSourceConverter = new ImageSourceConverter();
                    LocationImage.Background.SetValue(ImageBrush.ImageSourceProperty, imageSourceConverter.ConvertFromString(imageName));
                }
                fileDialog = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
    }
}
