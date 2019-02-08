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
    /// Interaction logic for ModifyEmployeesPage.xaml
    /// </summary>
    public partial class ModifyEmployeesPage : Page
    {
        String dateTime,name,imageName;
        public ModifyEmployeesPage()
        {
            InitializeComponent();
            FirstNameBox.Text = "Unknown";
            LastNameBox.Text = "Unknown";
            PhoneNumberBox.Text = "Unknown";
            SalaryBox.Text = "Unknown";
        }
        public ModifyEmployeesPage(DateTime dateTime, String FirstName, String LastName, String PhoneNumber, int Salary, String Position, BitmapImage bi)
        {
            InitializeComponent();
            this.dateTime = dateTime.ToString("yyyy-MM-dd HH:mm:ss");
            FirstNameBox.Text = FirstName;
            LastNameBox.Text = LastName;
            PhoneNumberBox.Text = PhoneNumber;
            SalaryBox.Text = $"{Salary}";
            PositionBox.Text = Position;
            EmployeeImage.Background.SetValue(ImageBrush.ImageSourceProperty, bi);
        }
        public ModifyEmployeesPage(DateTime dateTime, String FirstName, String LastName, String PhoneNumber, int Salary, String Position, string region, string subCity, ImageBrush bi)
        {
            InitializeComponent();
            this.dateTime = dateTime.ToString("yyyy-MM-dd HH:mm:ss");
            FirstNameBox.Text = FirstName;
            LastNameBox.Text = LastName;
            PhoneNumberBox.Text = PhoneNumber;
            SalaryBox.Text = $"{Salary}";
            PositionBox.Text = Position;
            RegionBox.Text = region;
            SubCityBox.Text = subCity;
            EmployeeImage.Background.SetValue(ImageBrush.ImageSourceProperty, bi.ImageSource);
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
            try
            {
                if (imageName != "")
                {
                    FileStream fileStream = new FileStream(imageName, FileMode.Open, FileAccess.Read);
                    byte[] imageByte = new byte[fileStream.Length];
                    fileStream.Read(imageByte, 0, System.Convert.ToInt32(fileStream.Length));
                    fileStream.Close();

                    string firstName = "";
                    string lastName = "";
                    string phoneNumber = "";
                    string salary = "";
                    string position = "";
                    string region = "";
                    string subCity = "";
                    if (check(FirstNameBox.Text, LastNameBox.Text, PhoneNumberBox.Text, SalaryBox.Text, PositionBox.Text, RegionBox.Text, SubCityBox.Text, imageByte, ref firstName, ref lastName, ref phoneNumber, ref salary, ref position, ref region, ref subCity) == 1)
                    {
                        try
                        {
                            DBconnect dBconnect = new DBconnect();
                            dBconnect.ModifyEmployee(dateTime, firstName, lastName, phoneNumber, salary, position, region, subCity, imageByte);

                            MessageBox.Show("Employee is modified.", "Modified Employee");
                            EmployeesPage employeesPage = new EmployeesPage();
                            this.NavigationService.Navigate(employeesPage);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message.ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
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
                    EmployeeImage.Background.SetValue(ImageBrush.ImageSourceProperty, imageSourceConverter.ConvertFromString(imageName));
                }
                fileDialog = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private int check(string FirstNameBox, string LastNameBox, string PhoneNumberBox, string SalaryBox, string PositionBox, string regionBox, string SubCityBox, byte[] imageByte, ref string firstName, ref string lastName, ref string phoneNumber, ref string salary, ref string position, ref string region, ref string subCity)
        {
            string regExp = "~!@#$%^&*()_/*-+[]\\;'/.,{}|:'<>?`1234567890";
            string symbols = "~!@#$%^&*()_/*-+[]\\;'/.,{}|:'<>?`";
            string numbers = "1234567890";
            if ((FirstNameBox.Length > 0) && (LastNameBox.Length > 0) && (PhoneNumberBox.Length > 0) && (SalaryBox.Length > 0) && (PositionBox.Length > 0))
            {
                foreach (char c in FirstNameBox)
                {
                    if (regExp.Contains(c))
                    {
                        MessageBox.Show("First name included an illegal character.");
                        return 0;
                    }
                }
                if (FirstNameBox.Length < 3)
                {
                    MessageBox.Show("First name should have more than 3 characters");
                    return 0;
                }
                firstName = FirstNameBox;

                foreach (char c in LastNameBox)
                {
                    if (regExp.Contains(c))
                    {
                        MessageBox.Show("Last name included an illegal character.");
                        return 0;
                    }
                }
                if (LastNameBox.Length < 3)
                {
                    MessageBox.Show("First name should have more than 3 characters");
                    return 0;
                }
                lastName = LastNameBox;

                if (PhoneNumberBox.Length != 10)
                {
                    MessageBox.Show("A phone number should be 10 digits only.");
                    return 0;
                }
                foreach (char c in PhoneNumberBox)
                {
                    if (!(numbers.Contains(c)))
                    {
                        MessageBox.Show("Phone number included an illegal character.");
                        return 0;
                    }
                }
                phoneNumber = PhoneNumberBox;

                if (SalaryBox.Length <= 2)
                {
                    MessageBox.Show("A salary should be more than 100 birr.");
                    return 0;
                }
                foreach (char c in SalaryBox)
                {
                    if (!(numbers.Contains(c)))
                    {
                        MessageBox.Show("Salary included an illegal character.");
                        return 0;
                    }
                }
                salary = SalaryBox;
                position = PositionBox;
                region = regionBox;
                foreach (char c in SubCityBox)
                {
                    if (symbols.Contains(c))
                    {
                        MessageBox.Show("Sub city included an illegal character.");
                        return 0;
                    }
                }
                if (SubCityBox.Length <= 2)
                {
                    MessageBox.Show("A sub city should contain more than 3 characters.");
                    return 0;
                }
                subCity = SubCityBox;
                if (imageByte.Length > 0)
                {
                    return 1;
                }
                else
                {
                    MessageBox.Show("Could not process image");
                    return 0;
                }
            }
            else
            {
                MessageBox.Show("A field is left empty.");
                return 0;
            }
        }

    }
}
