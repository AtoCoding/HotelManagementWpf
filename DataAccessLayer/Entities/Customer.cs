using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Entities
{
    public enum CustomerStatus
    {
        Active = 1,
        Deleted = 2
    }

    public class Customer
    {
        public int CustomerID { get; set; }

        [Required(ErrorMessage = "Full Name is required.")]
        [StringLength(100, ErrorMessage = "Full Name cannot exceed 100 characters.")]
        public string? CustomerFullName { get; set; }

        [Required(ErrorMessage = "Telephone is required.")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Telephone must be 10 digits.")]
        public string? Telephone { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string? EmailAddress { get; set; }

        [Required(ErrorMessage = "Birthday is required.")]
        [DataType(DataType.Date, ErrorMessage = "Invalid date format")]
        public DateTime? CustomerBirthday { get; set; }

        [Required(ErrorMessage = "Status is required.")]
        public CustomerStatus? CustomerStatus { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "Password must be between 6 and 20 characters.")]
        public string? Password { get; set; }

        public Customer()
        {
        }

        public Customer(int customerID, string? customerFullName, string? telephone, string? emailAddress, DateTime customerBirthday, CustomerStatus customerStatus, string? password)
        {
            CustomerID = customerID;
            CustomerFullName = customerFullName;
            Telephone = telephone;
            EmailAddress = emailAddress;
            CustomerBirthday = customerBirthday;
            CustomerStatus = customerStatus;
            Password = password;
        }
    }
}
