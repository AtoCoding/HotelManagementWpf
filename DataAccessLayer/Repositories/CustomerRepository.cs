using DataAccessLayer.Entities;
using DataAccessLayer.Repositories.Interface;

namespace DataAccessLayer.Repositories
{
    public class CustomerRepository : IRepository<Customer>
    {
        private List<Customer> _Customers;

        public CustomerRepository()
        {
            Customer c1 = new Customer(1, "Adam", "0936668963", "adam@gmail.com", DateTime.Now, CustomerStatus.Active, "@1");
            Customer c2 = new Customer(2, "Eva", "0938938963", "eva@gmail.com", DateTime.Now, CustomerStatus.Active, "@1");
            Customer c3 = new Customer(3, "Havan", "0885298963", "havan@gmail.com", DateTime.Now, CustomerStatus.Active, "@1");
            _Customers = new List<Customer>();
            _Customers = [c1, c2, c3];
        }

        public Customer Add(Customer data)
        {
            _Customers.Add(data);
            return data;
        }

        public bool Delete(int id)
        {
            Customer? customer = _Customers.FirstOrDefault(c => c.CustomerID == id);
            return _Customers.Remove(customer!);
        }

        public Customer? Get(int id)
        {
            return _Customers.FirstOrDefault(x => x.CustomerID == id);
        }

        public List<Customer> GetAll()
        {
            return _Customers;
        }

        public Customer? Update(Customer data)
        {
            Customer? customer = _Customers.FirstOrDefault(c => c.CustomerID == data.CustomerID);
            if (customer != null)
            {
                customer.CustomerFullName = data.CustomerFullName;
                customer.Telephone = data.Telephone;
                customer.EmailAddress = data.EmailAddress;
                customer.CustomerBirthday = data.CustomerBirthday;
                customer.CustomerStatus = data.CustomerStatus;
                customer.Password = data.Password;
            }
            return customer;
        }
    }
}
