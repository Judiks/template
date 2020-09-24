using System.ComponentModel.DataAnnotations;
using THD.Domain.Models.Enums;

namespace THD.Domain.Models.AccountModels.Request
{
    public class CreateUserRequest
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public UserRoleModel UserRole { get; set; }
    }
}
