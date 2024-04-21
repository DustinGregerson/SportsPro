using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace SportsPro.Models
{
    public class Customer
    {
		public int CustomerID { get; set; }

		[Required(ErrorMessage ="First name is required.")]
        [MaxLength(51)]
        [MinLength(1)]
        [RegularExpression("^[a-zA-Z]+$", 
			ErrorMessage = "First Name must not contain special characters or numbers")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required")]
        [MaxLength(51)]
        [MinLength(1)]
        [RegularExpression("^[a-zA-Z]+$",
            ErrorMessage = "Last Name must not contain special characters or numbers")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Address is required.")]
        [MaxLength(51)]
        [MinLength(1)]
        [RegularExpression("^([a-zA-Z0-9\\s])+$",
            ErrorMessage = "Address must not contain special characters")]
        public string Address { get; set; }

        [Required(ErrorMessage = "City is required.")]
        [MaxLength(51)]
        [MinLength(1)]
        [RegularExpression("^[a-zA-Z\\s]+$",
            ErrorMessage = "City must not contain special characters or numbers")]
        public string City { get; set; }

        [Required(ErrorMessage = "State is required.")]
        [RegularExpression("^[A-Z]{2}$",
            ErrorMessage = "State must be abbreviated and contain capital letters ie.SD")]
        public string State { get; set; }

        [Required(ErrorMessage = "Postal code is required.")]
        [MaxLength(21)]
        [MinLength(1)]
        [RegularExpression("^[0-9]+$", ErrorMessage = "postal code must be numeric")]
        public string PostalCode { get; set; }

        [Required(ErrorMessage = "Required.")]
        public string CountryID { get; set; }
		public Country Country { get; set; }

        [Required(ErrorMessage = "Phone number is required.")]
        [StringLength(14)]
        [RegularExpression(@"^\(\d{3}\)\s*\d{3}-\d{4}$", ErrorMessage = "Phone number must be in the format (999) 999-9999.")]
        public string Phone { get; set; }




        [Required(ErrorMessage = "Please enter a valid email address.")]
        [StringLength(51)]
        [DataType(DataType.EmailAddress, ErrorMessage = "Email is not valid")]
        public string Email { get; set; }

        public string FullName => FirstName + " " + LastName;   // read-only property
	}
}