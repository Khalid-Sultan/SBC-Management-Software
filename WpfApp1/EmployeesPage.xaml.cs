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
    /// Interaction logic for EmployeesPage.xaml
    /// </summary>
    public partial class EmployeesPage : Page
    {
        public EmployeesPage()
        {
            InitializeComponent();
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
        private void ViewAllEmployeesButton_Click(object sender, RoutedEventArgs e)
        {
            ViewAllEmployeesPage viewAllEmployeesPage = new ViewAllEmployeesPage();
            this.NavigationService.Navigate(viewAllEmployeesPage);
        }
        private void AddAnEmployeeButton_Click(object sender, RoutedEventArgs e)
        {
            AddAnEmployeePage addAnEmployeePage = new AddAnEmployeePage();
            this.NavigationService.Navigate(addAnEmployeePage);
        }
        private void RemoveAnEmployeeButton_Click(object sender, RoutedEventArgs e)
        {
            ViewAllEmployeesPage viewAllEmployeesPage = new ViewAllEmployeesPage();
            this.NavigationService.Navigate(viewAllEmployeesPage);
        }
        private void ModifyEmployeesListButton_Click(object sender, RoutedEventArgs e)
        {
            ViewAllEmployeesPage viewAllEmployeesPage = new ViewAllEmployeesPage();
            this.NavigationService.Navigate(viewAllEmployeesPage);
        }
    }
}