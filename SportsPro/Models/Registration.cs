using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace SportsPro.Models
{
    public class Registration
    {
        public int RegistrationID { get; set; }

        [Required]
        public int CustomerID { get; set; }

        [Required]
        public int ProductID { get; set; }

        public Product Product { get; set; }


    }
}
