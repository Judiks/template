using THD.Domain.Models.Enums;

namespace THD.Domain.Models.AccountModels.Response
{
    public class UserResponse
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public UserRoleModel UserRole { get; set; }
    }
}
