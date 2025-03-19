using System.Windows;
using BusinessLogicLayer.Services;
using BusinessLogicLayer.Services.Interface;
using DataAccessLayer.Entities;
using Wpf_Hms.Admin;

namespace Wpf_Hms
{
    /// <summary>
    /// Interaction logic for CustomerInformationWindow.xaml
    /// </summary>
    public partial class CustomerInformationWindow : Window
    {
        private readonly IService<Customer> _CustomerService;
       
        public CustomerInformationWindow()
        {
            InitializeComponent();
            _CustomerService = CustomerService.GetInstance();
            LoadCustomerInformation();
        }

        private void LoadCustomerInformation()
        {
            var customers = _CustomerService.GetAll();

            LoadDataWindow(customers);
        }

        private void LoadDataWindow(List<Customer> customers)
        {
            dgCustomer.ItemsSource = customers;
            dgCustomer.Items.Refresh();
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            CustomerProcessingWindow customerProcessingWindow = new(true, null!, dgCustomer);
            customerProcessingWindow.ShowDialog();
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            Customer customer = (Customer)dgCustomer.SelectedItem;
            if (customer == null)
            {
                MessageBox.Show("Please select a room to update");
                return;
            }
            CustomerProcessingWindow customerProcessingWindow = new(false, customer, dgCustomer);
            customerProcessingWindow.ShowDialog();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show(
                "Do you really want to delete this customer?",
                "Confirmation",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                Customer customer = (Customer)dgCustomer.SelectedItem;

                if (customer == null)
                {
                    MessageBox.Show("Please select a room to delete");
                    return;
                }

                if (_CustomerService.Delete(customer.CustomerID))
                {
                    MessageBox.Show("Delete successfully");
                }
                else
                {
                    MessageBox.Show("Delete failed");
                }
                LoadCustomerInformation();
            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            OptionWindow optionWindow = new(true);
            optionWindow.Show();
            this.Close();
        }

        private void btnQuit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            string? fullnameSearch = txtFullNameSearch.Text;
            string? telephoneSearch = txtTelephoneSearch.Text;
            string? emailAddressSearch = txtEmailAddressSearch.Text;


            var customers = _CustomerService.Search(fullnameSearch, telephoneSearch, emailAddressSearch);

            LoadDataWindow(customers);
        }
    }
}