using System.Collections.Generic;
using THD.Domain.Entities;
using THD.Domain.Models.AccountModels.Response;

namespace THD.Domain.Helpers
{
    public interface IJwtHelper
    {
        JwtAuthentificationResponse GenerateToken(ApplicationUser user, IEnumerable<string> userRoles);
    }
}
