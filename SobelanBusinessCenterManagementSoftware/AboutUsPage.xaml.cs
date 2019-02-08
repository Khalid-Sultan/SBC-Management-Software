using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace SBCManagementSoftware
{
    /// <summary>
    /// Interaction logic for AboutUsPage.xaml
    /// </summary>
    public partial class AboutUsPage : Page
    {
        DispatcherTimer timer;
        int ctr = 0;

        public AboutUsPage()
        {
            InitializeComponent();
            try
            {
                timer = new DispatcherTimer();
                timer.IsEnabled = true;
                timer.Interval = new TimeSpan(0, 0, 2);
                timer.Tick += new EventHandler(timer_Tick);
            }
            catch
            {

            }
        }
        void timer_Tick(object sender, EventArgs e)
        {
            ctr++;
            if (ctr > 5)
            {
                ctr = 1;
            }
            PlaySlideShow(ctr);
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ctr = 1;
            PlaySlideShow(ctr);
        }
        private void PlaySlideShow(int ctr)
        {
            BitmapImage image = new BitmapImage();
            image.BeginInit();
            string filename = "images/Plane0" + ctr + ".jpg";
            image.UriSource = new Uri(filename, UriKind.Relative);
            image.EndInit();
            myImage.Source = image;
            myImage.Stretch = Stretch.UniformToFill;
        }


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
    }
}
