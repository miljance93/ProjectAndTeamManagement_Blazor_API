using System.ComponentModel.DataAnnotations;

namespace Shared.DTOs
{
    public class UserForRegistrationDto
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public string RoleId { get; set; } // Id for Role
        public int? TeamId { get; set; }
    }
}
