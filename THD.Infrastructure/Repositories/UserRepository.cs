using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using THD.Domain.Entities;
using THD.Domain.Repositories;

namespace THD.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        public ApplicationDbContext _applicationDbContext;

        protected DbSet<ApplicationUser> _userDbSet;
        public UserRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
            _userDbSet = _applicationDbContext.Set<ApplicationUser>();
        }

        public async Task<List<IdentityUserRole<string>>> GetAlUserRoles()
        {
            return await _applicationDbContext.UserRoles.ToListAsync();
        }
    }
}
