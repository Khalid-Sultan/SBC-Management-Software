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
    /// Interaction logic for RentOutAShopPage.xaml
    /// </summary>
    public partial class RentOutAShopPage : Page
    {
        private int iD;
        private int floor;
        private int price;

        public RentOutAShopPage(int Id,int Floor,int Price)
        {
            InitializeComponent();
            this.iD = Id;
            this.floor = Floor;
            this.price = Price;
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
                DBconnect dBconnect = new DBconnect();
                String firstName = ""; 
                String lastName = "";
                string name = ""; 
                string houseNo = "";
                string county = "";
                int rentDuration = 0;
                string region = "";
                string subCity = "";
                if (check(
                    FirstNameBox.Text, LastNameBox.Text, 
                    NameBox.Text, HouseNumberBox.Text, 
                    CountyBox.Text, RentDurationBox.Text, 
                    RegionBox.Text, SubCityBox.Text, 
                    ref firstName, ref lastName, 
                    ref name, ref houseNo, 
                    ref county, ref rentDuration,
                    ref region, ref subCity) == 1)
                {
                    dBconnect.RentShop(floor, iD, rentDuration,price, firstName, lastName, name, region, subCity, county, houseNo);
                    MessageBox.Show("Shop is Rented", "Rented Shop");
                    ViewAllShopsPage viewAllShopsPage = new ViewAllShopsPage();
                    this.NavigationService.Navigate(viewAllShopsPage);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }

        }
        private int check(
                string FirstNameBox, string LastNameBox,
                string NameBox, string HouseNumberBox,
                string CountyBox, string RentDurationBox,
                string RegionBox, string SubCityBox,
                ref string firstName, ref string lastName,
                ref string name, ref string houseNo,
                ref string county, ref int rentDuration,
                ref string region, ref string subCity)
        {
            string regExp = "~!@#$%^&*()_/*-+[]\\;'/.,{}|:'<>?`1234567890";
            string symbols = "~!@#$%^&*()_/*-+[]\\;'/.,{}|:'<>?`";
            string numbers = "1234567890";
            if ((FirstNameBox.Length > 0) && (LastNameBox.Length > 0) && (NameBox.Length > 0) && (HouseNumberBox.Length > 0) && 
                (CountyBox.Length > 0) && (RentDurationBox.Length > 0) && (RegionBox.Length > 0) && (SubCityBox.Length > 0))
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

                foreach (char c in NameBox)
                {
                    if (regExp.Contains(c))
                    {
                        MessageBox.Show("Company name included an illegal character.");
                        return 0;
                    }
                }
                if (NameBox.Length < 3)
                {
                    MessageBox.Show("Company name should have more than 3 characters");
                    return 0;
                }
                name = NameBox;

                if (HouseNumberBox.Length < 2 || HouseNumberBox.Length > 5)
                {
                    MessageBox.Show("A house number should be between 2 and 5 digits only.");
                    return 0;
                }
                foreach (char c in HouseNumberBox)
                {
                    if (!(numbers.Contains(c)))
                    {
                        MessageBox.Show("House number included an illegal character.");
                        return 0;
                    }
                }
                houseNo = HouseNumberBox;

                if (CountyBox.Length <=0 || CountyBox.Length > 2)
                {
                    MessageBox.Show("A county number should be 1 or 2 digits long only.");
                    return 0;
                }
                foreach (char c in CountyBox)
                {
                    if (!(numbers.Contains(c)))
                    {
                        MessageBox.Show("County number included an illegal character.");
                        return 0;
                    }
                }
                county = CountyBox;
                
                rentDuration = Convert.ToInt32(RentDurationBox);

                region = RegionBox;
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
                return 1;
            }
            else
            {
                MessageBox.Show("A field is left empty.");
                return 0;
            }
        }

    }
}
