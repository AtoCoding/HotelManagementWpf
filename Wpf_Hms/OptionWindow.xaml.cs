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
using System.Windows.Shapes;

namespace Wpf_Hms.Admin
{
    /// <summary>
    /// Interaction logic for OptionWindow.xaml
    /// </summary>
    public partial class OptionWindow : Window
    {
        /// <summary>
        /// Checking window for admin or customer
        /// </summary>
        public bool IsAdmin { get; set; } = false;

        public OptionWindow(bool isAdmin)
        {
            IsAdmin = isAdmin;
            InitializeComponent();
            Canvas_Loaded();
        }

        private void Canvas_Loaded()
        {
            if (IsAdmin)
            {
                AdminSession.Visibility = Visibility.Visible;
                CustomerSession.Visibility = Visibility.Hidden;
            } 
            else
            {
                AdminSession.Visibility = Visibility.Hidden;
                CustomerSession.Visibility = Visibility.Visible;
            }
        }

        private void BtnCustomerManagement_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnRoomManagement_Click(object sender, RoutedEventArgs e)
        {
            RoomInformationWindow roomInformationWindow = new RoomInformationWindow();
            roomInformationWindow.Show();
            this.Close();
        }
        
        private void BtnReportManagement_Click(object sender, RoutedEventArgs e)
        {
            //AdminMainWindow adminMainWindow = new AdminMainWindow();
            //adminMainWindow.Show();
            //this.Close();
        }

        private void BtnBookingHistory_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnProfile_Click(object sender, RoutedEventArgs e)
        {
            //AdminMainWindow adminMainWindow = new AdminMainWindow();
            //adminMainWindow.Show();
            //this.Close();
        }
    }
}
