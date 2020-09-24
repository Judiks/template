using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace THD.Domain.Repositories
{
    public interface IUserRepository
    {
        Task<List<IdentityUserRole<string>>> GetAlUserRoles();
    }
}
