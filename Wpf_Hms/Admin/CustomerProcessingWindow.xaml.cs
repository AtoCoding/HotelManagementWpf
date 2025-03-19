using System.Linq;
using System.Windows;
using System.Windows.Controls;
using BusinessLogicLayer.Services;
using BusinessLogicLayer.Services.Interface;
using DataAccessLayer.Entities;

namespace Wpf_Hms
{
    /// <summary>
    /// Interaction logic for CustomerProcessingWindow.xaml
    /// </summary>
    public partial class CustomerProcessingWindow : Window
    {
        private readonly IService<Customer> _CustomerService;

        private int newCustomerId;

        private bool isCreateAction;

        private DataGrid dgCustomer;

        public CustomerProcessingWindow(bool isCreateAction, Customer customer, DataGrid dgCustomer)
        {
            InitializeComponent();
            _CustomerService = CustomerService.GetInstance();
            this.isCreateAction = isCreateAction;
            this.dgCustomer = dgCustomer;
            SetDefaultData(customer);
        }

        private void SetDefaultData(Customer customer)
        {
            LoadCustomerStatus();

            if (isCreateAction)
            {
                List<Customer> customers = _CustomerService.GetAll();
                newCustomerId = customers.Count + 1;

                bool isNotExisted = customers.FirstOrDefault(x => x.CustomerID == newCustomerId) == null;

                if (isNotExisted)
                {
                    lbTitle.Content = "Create Customer";
                    txtCustomerId.Text = newCustomerId.ToString();
                } 
                else
                {
                    MessageBox.Show("There is an error!");
                }
            }
            else
            {
                lbTitle.Content = "Update Customer";
                txtCustomerId.Text = customer.CustomerID.ToString();
                txtCustomerFullName.Text = customer.CustomerFullName?.ToString();
                txtTelephone.Text = customer.Telephone?.ToString();
                txtEmailAddress.Text = customer.EmailAddress?.ToString();
                dpCustomerBirthday.DisplayDate = customer.CustomerBirthday.Date;
                cbxCustomerStatus.SelectedValue = (int)customer.CustomerStatus;
            }
        }

        private void LoadCustomerStatus()
        {
            var customerStatus = Enum.GetValues(typeof(CustomerStatus))
                            .Cast<CustomerStatus>()
                            .Select(x => new {CustomerStatusId = (int)x, CustomerRoomStatusName = x.ToString()});

            cbxCustomerStatus.ItemsSource = customerStatus;
            cbxCustomerStatus.SelectedValuePath = "CustomerStatusId";
            cbxCustomerStatus.DisplayMemberPath = "CustomerRoomStatusName";
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            Customer customer = new()
            {
                CustomerID = int.Parse(txtCustomerId.Text),
                CustomerFullName = txtCustomerFullName.Text,
                Telephone = txtTelephone.Text,
                EmailAddress = txtEmailAddress.Text,
                CustomerBirthday = dpCustomerBirthday.DisplayDate,
                CustomerStatus = (CustomerStatus)cbxCustomerStatus.SelectedValue,
                Password = txtPassword.Text
            };

            if (isCreateAction)
            {   
                var dataAdd = _CustomerService.Add(customer);
                
                if (dataAdd != null)
                {
                    MessageBox.Show("Create successfully!");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("There is an error!");
                }
            } 
            else
            {
                var dataUpdate = _CustomerService.Update(customer);

                if (dataUpdate != null)
                {
                    MessageBox.Show("Update successfully!");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("There is an error!");
                }
            }

            dgCustomer.ItemsSource = _CustomerService.GetAll();
            dgCustomer.Items.Refresh();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
