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
    /// Interaction logic for ShopsPage.xaml
    /// </summary>
    public partial class ShopsPage : Page
    {
        public ShopsPage()
        {
            InitializeComponent();
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


        // Options Buttons
        private void ViewAllShopsButton_Click(object sender, RoutedEventArgs e)
        {
            ViewAllShopsPage viewAllShopsPage = new ViewAllShopsPage();
            this.NavigationService.Navigate(viewAllShopsPage);
        }
        private void RentOutAShopButton_Click(object sender, RoutedEventArgs e)
        {
            ViewAllShopsPage viewAllShopsPage = new ViewAllShopsPage();
            this.NavigationService.Navigate(viewAllShopsPage);
        }
        private void ClearOutAShoputton_Click(object sender, RoutedEventArgs e)
        {
            ViewAllShopsPage viewAllShopsPage = new ViewAllShopsPage();
            this.NavigationService.Navigate(viewAllShopsPage);
        }
        private void ModifyShopsListButton_Click(object sender, RoutedEventArgs e)
        {
            ViewAllShopsPage viewAllShopsPage = new ViewAllShopsPage();
            this.NavigationService.Navigate(viewAllShopsPage);
        }
    }
}