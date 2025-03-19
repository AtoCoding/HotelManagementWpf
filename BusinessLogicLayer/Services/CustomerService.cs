using BusinessLogicLayer.Services.Interface;
using DataAccessLayer;
using DataAccessLayer.Entities;
using DataAccessLayer.Repositories;
using DataAccessLayer.Repositories.Interface;
using Microsoft.Extensions.Configuration;

namespace BusinessLogicLayer.Services
{
    public class CustomerService : IService<Customer>, IAccount<Customer>
    {
        private static CustomerService _Instance = null!;
        private readonly IRepository<Customer> _CustomerRepository;

        private CustomerService()
        {
            _CustomerRepository = CustomerRepository.GetInstance();
        }

        public static CustomerService GetInstance() => _Instance ??= new CustomerService();

        public Customer Add(Customer data)
        {
            if (data != null) return _CustomerRepository.Add(data);

            return null!;
        }

        /// <summary>
        /// Check authentication and authorization
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public (bool isAuthen, string role) CheckAuth(string email, string password)
        {
            IConfiguration config = Utils.GetConfiguration();

            var jsonEmail = config["AdminAccount:Email"];
            var jsonPassword = config["AdminAccount:Password"];

            bool check = string.Equals(email, jsonEmail) && string.Equals(password, jsonPassword);

            if (check)
            {
                return (true, "Admin");
            }
            else
            {
                List<Customer> customers = GetAll();
                bool isExisted = customers.FirstOrDefault(x => x.EmailAddress == email && x.Password == password) != null;
                return isExisted ? (isExisted, "Customer") : (isExisted, "Guest");
            }
        }

        public int Count()
        {
            return _CustomerRepository.Count();
        }

        public bool Delete(int id)
        {
            return _CustomerRepository.Delete(id);
        }

        public Customer? Get(int id)
        {
            return _CustomerRepository.Get(id);
        }

        public List<Customer> GetAll()
        {
            return _CustomerRepository.GetAll();
        }

        public List<Customer> Search(string? fullName, string? telephone, string? emailAddress)
        {
            return _CustomerRepository.Search(fullName, telephone, emailAddress);
        }

        public List<Customer> Search(string? description, string? typeName, int capacity)
        {
            return [];
        }

        public Customer? Update(Customer data)
        {
            if (data != null) return _CustomerRepository.Update(data);

            return null!;
        }
    }
}
