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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : NavigationWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            try
            {
                DBconnect dBconnect0 = new DBconnect();
                dBconnect0.modifyLegalDoc("docs/OriginalRentAgreement.xps", "RestoreRental");
                DBconnect dBconnect1 = new DBconnect();
                dBconnect1.modifyLegalDoc("docs/OriginalEmployeement.xps", "RestoreEmployee");
                DBconnect dBconnect2 = new DBconnect();
                dBconnect2.modifyLegalDoc("docs/OriginalEstablishment.xps", "RestoreEstablishment");
                DBconnect dBconnect3 = new DBconnect();
                dBconnect3.modifyLegalDoc("docs/OriginalRules.xps", "RestoreRules");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }


    }
}
