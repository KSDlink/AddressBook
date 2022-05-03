using AddressBookBackend.Attributes;
using System.ComponentModel.DataAnnotations;

namespace ContactList.Models
{
    public class Contact
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Required]
        [IsEmail(ErrorMessage = "Введите корректный адрес электронной почты")]
        public string Email { get; set; }
        [Required]
        [IsPhoneNumber(ErrorMessage = "Введите корректный номер телефона")]
        public string PhoneNumber { get; set; }
        public string Gender { get; set; }
        public string Birthday { get; set; }

        public Contact()
        {
            FirstName = LastName = Email = PhoneNumber = Gender = Birthday = ""; //cs8618
        }
    }
}
