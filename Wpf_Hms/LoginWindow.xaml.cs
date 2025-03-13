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
using BusinessLogicLayer.Services;
using Wpf_Hms.Admin;

namespace Wpf_Hms
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private readonly CustomerService _CustomerService;

        public LoginWindow()
        {
            InitializeComponent();
            _CustomerService = new CustomerService();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string email = txtEmail.Text;
            string password = txtPassword.Text;

            (bool isAuthen, string role) = _CustomerService.CheckAuth(email, password);

            if (isAuthen)
            {
                switch (role)
                {
                    case "Admin":
                        OptionWindow adminOptionWindow = new OptionWindow(true);                        
                        adminOptionWindow.Show();
                        this.Close();
                        break;
                    case "Customer":
                        OptionWindow customerOptionWindow = new OptionWindow(false);
                        customerOptionWindow.Show();
                        this.Close();
                        break;
                    default:
                        break;
                }
            } 
            else
            {
                MessageBox.Show("Invalid email or password");
            }

            //if (isAuthenticated && isAdmin)
            //{
            //    AdminMainWindow adminWindow = new AdminMainWindow();
            //    adminWindow.Show();
            //    this.Close();
            //}
            //else if (isAuthenticated && !isAdmin)
            //{
            //    CustomerMainWindow customerWindow = new CustomerMainWindow();
            //    customerWindow.Show();
            //    this.Close();
            //}
            //else
            //{
            //    MessageBox.Show("Invalid email or password", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            //}
        }

        private void QuitButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
