using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.DTO
{

    public class UserAccountDTO
    {

        [Required]
        [MaxLength(50)]
        [MinLength(3)]
        public string UserName { get; set; }=string.Empty;

        [EmailAddress]
        [Required] 
        public string Email { get; set; }=string.Empty ;

        [Required]
        [MinLength(10)]
        public string PhoneNumber { get; set; } = string.Empty;

        [DataType(DataType.Password)]
        [Required]
        public string Password { get; set; }=string.Empty;

        [DataType(DataType.Password)]
        [Required]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }=string.Empty;

        // New property for admin role
        public bool IsAdmin { get; set; } = false;
        

    }
}
