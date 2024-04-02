using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRUDStoredProcedure.Models
{
    public class Employee
    {
        [Key]
        public int ID { get; set; }

        [DisplayName("First Name")]
        [Required]
        public string Firstname { get; set; }

        [DisplayName("Last Name")]
        [Required]
        public string LastName { get; set; }


        [DisplayName("Date of Birth")]
        [Required]
        public DateTime DateOfBirth { get; set; }


        [DisplayName("Email Address")]
        [Required]
        public string Email { get; set; }

        [Required]
        public double Salary { get; set; }


        [NotMapped]
        public string FullName
        {
            get
            {
                return Firstname + " " + LastName;
            }
        }
    }
}
