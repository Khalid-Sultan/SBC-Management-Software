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
    /// Interaction logic for ModifyShopsListPage.xaml
    /// </summary>
    public partial class ModifyShopsListPage : Page
    {
        private int iD;


        public ModifyShopsListPage(int iD)
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
            int floor = int.Parse(FloorBox.Text);
            int areaSize = int.Parse(AreaSizeBox.Text);
            int rentAmount = int.Parse(RentAmountBox.Text);
            dBconnect.ModifyShop(iD,floor, areaSize, rentAmount);
            MessageBox.Show("Shop is modified", "Modified Shop");
            ViewAllShopsPage viewAllShopsPage = new ViewAllShopsPage();
            this.NavigationService.Navigate(viewAllShopsPage);

        }
    }
}
