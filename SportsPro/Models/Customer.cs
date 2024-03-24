using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace SportsPro.Models
{
    public class Customer
    {
		public int CustomerID { get; set; }

		[Required(ErrorMessage ="Please enter first name.")]
        [MaxLength(51)]
        [MinLength(1)]
        [RegularExpression("^([a-zA-Z0-9 .&'-]+)$", 
			ErrorMessage = "First Name must not contain special characters")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Please enter Last name.")]
        [MaxLength(51)]
        [MinLength(1)]
        [RegularExpression("^([a-zA-Z0-9 .&'-]+)$",
            ErrorMessage = "Last Name must not contain special characters")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Please Your Address.")]
        [MaxLength(51)]
        [MinLength(1)]
        [RegularExpression("^([a-zA-Z0-9 .&'-]+)$",
            ErrorMessage = "Address must not contain special characters")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Please enter City.")]
        [MaxLength(51)]
        [MinLength(1)]
        [RegularExpression("^([a-zA-Z0-9 .&'-]+)$",
            ErrorMessage = "City must not contain special characters")]
        public string City { get; set; }

        [Required(ErrorMessage = "Please enter State.")]
        [MaxLength(51)]
        [MinLength(1)]
        [RegularExpression("^([a-zA-Z0-9 .&'-]+)$",
            ErrorMessage = "State must not contain special characters")]
        public string State { get; set; }

        [Required(ErrorMessage = "Please postal code.")]
        [MaxLength(21)]
        [MinLength(1)]
        [RegularExpression("[^0-9]", ErrorMessage = "postal code must be numeric")]
        public string PostalCode { get; set; }

        [Required(ErrorMessage = "Please pick a country.")]
        public string CountryID { get; set; }
		public Country Country { get; set; }

        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$",
                   ErrorMessage = "Entered phone format is not valid. (999)-999-9999")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Please enter an Email.")]
        [StringLength(51)]
        [DataType(DataType.EmailAddress, ErrorMessage = "Email is not valid")]
        public string Email { get; set; }

		public string FullName => FirstName + " " + LastName;   // read-only property
	}
}