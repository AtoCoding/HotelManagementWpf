using System.ComponentModel.DataAnnotations;
using System.Windows;
using System.Windows.Controls;
using BusinessLogicLayer.Services;
using BusinessLogicLayer.Services.Interface;
using DataAccessLayer.Entities;
using ValidationResult = System.ComponentModel.DataAnnotations.ValidationResult;

namespace Wpf_Hms.Admin
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
                txtCustomerFullName.Text = customer.CustomerFullName;
                txtTelephone.Text = customer.Telephone;
                txtEmailAddress.Text = customer.EmailAddress;
                dpCustomerBirthday.SelectedDate = customer.CustomerBirthday;
                cbxCustomerStatus.SelectedValue = (int)customer.CustomerStatus!;
                txtPassword.Text = customer.Password;
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
                CustomerFullName = txtCustomerFullName.Text ?? string.Empty,
                Telephone = txtTelephone.Text ?? string.Empty,
                EmailAddress = txtEmailAddress.Text ?? string.Empty,
                CustomerBirthday = dpCustomerBirthday.SelectedDate != null ? dpCustomerBirthday.SelectedDate : null,
                CustomerStatus = cbxCustomerStatus.SelectedValue != null ? (CustomerStatus)cbxCustomerStatus.SelectedValue : null,
                Password = txtPassword.Text ?? string.Empty
            };

            var validationResults = new List<ValidationResult>();
            var context = new ValidationContext(customer);

            bool isValid = Validator.TryValidateObject(customer, context, validationResults, true);

            if (!isValid)
            {
                string errorMessages = string.Join("\n", validationResults.Select(e => e.ErrorMessage));
                MessageBox.Show(errorMessages, "Validation Errors", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
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
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
