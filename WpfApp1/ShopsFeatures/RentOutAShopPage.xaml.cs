﻿using System;
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

        public RentOutAShopPage(int iD)
        {
            InitializeComponent();
            this.iD = iD;
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
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DBconnect dBconnect = new DBconnect();
            String firstName = FirstNameBox.Text;
            String lastName = LastNameBox.Text;
            String phoneNumber = PhoneNumberBox.Text;
            String rentDuration = RentDurationBox.Text;
            dBconnect.RentShop(iD, firstName, lastName, phoneNumber, rentDuration);
            MessageBox.Show("Shop is Rented", "Rented Shop");
            ViewAllShopsPage viewAllShopsPage = new ViewAllShopsPage();
            this.NavigationService.Navigate(viewAllShopsPage);

        }
    }
}
