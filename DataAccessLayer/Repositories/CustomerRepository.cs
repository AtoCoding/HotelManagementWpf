using System.Globalization;
using DataAccessLayer.Entities;
using DataAccessLayer.Repositories.Interface;

namespace DataAccessLayer.Repositories
{
    public class CustomerRepository : IRepository<Customer>
    {
        private static CustomerRepository _Instance = null!;
        private List<Customer> _Customers;

        private CustomerRepository()
        {
            Customer c1 = new Customer(1, "Adam", "0936668963", "adam@gmail.com", DateTime.Parse("19/03/2003", new CultureInfo("fr-FR")), CustomerStatus.Active, "@1");
            Customer c2 = new Customer(2, "Eva", "0938938963", "eva@gmail.com", DateTime.Parse("25/05/2003", new CultureInfo("fr-FR")), CustomerStatus.Active, "@1");
            Customer c3 = new Customer(3, "Havan", "0885298963", "havan@gmail.com", DateTime.Parse("01/12/2000", new CultureInfo("fr-FR")), CustomerStatus.Deleted, "@1");
            _Customers = new() { c1, c2, c3 };
        }

        public static CustomerRepository GetInstance() => _Instance ??= new CustomerRepository();

        public Customer Add(Customer data)
        {
            _Customers.Add(data);
            return data;
        }

        public int Count()
        {
            return _Customers.Count;
        }

        public bool Delete(int id)
        {
            Customer? customer = _Customers.FirstOrDefault(c => c.CustomerID == id);

            if (customer != null)
            {
                customer.CustomerStatus = CustomerStatus.Deleted;
                return true;
            }

            return false;
        }

        public Customer? Get(int id)
        {
            return _Customers.FirstOrDefault(x => x.CustomerID == id);
        }

        public List<Customer> GetAll()
        {
            return _Customers;
        }

        public List<Customer> Search(string? description, string? typeName, int capacity)
        {
            return [];
        }

        public List<Customer> Search(string? fullName, string? telephone, string? emailAddress)
        {
            List<Customer> result = _Customers.ToList();

            if (!string.IsNullOrEmpty(fullName))
            {
                result.RemoveAll(x => !x.CustomerFullName!.ToLower().Contains(fullName.ToLower()));
            }
            if (!string.IsNullOrEmpty(telephone))
            {
                result.RemoveAll(x => !x.Telephone!.ToLower().Contains(telephone.ToLower()));
            }
            if (!string.IsNullOrEmpty(emailAddress))
            {
                result.RemoveAll(x => !x.EmailAddress!.ToLower().Contains(emailAddress.ToLower()));
            }

            return result;
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
