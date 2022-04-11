using System.Threading.Tasks;
using Beyond.Data.Models;
using Beyond.Services.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Beyond.Services
{
    public class AssignToRole:IAssignToRole
    {
        private readonly UserManager<User> _userManager;
        public AssignToRole(UserManager<User> userManager)
        {
            _userManager = userManager;
        }
        public async Task<IdentityResult> Admin(User user)
        {
            var result= await _userManager.AddToRoleAsync(user, "Admin");
            return result;
        }

        public async Task<IdentityResult> User(User user)
        {
            var result= await _userManager.AddToRoleAsync(user, "User");
            return result;

        }
    }
}