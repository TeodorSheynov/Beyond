using System.Threading.Tasks;
using Beyond.Data.Models;
using Microsoft.AspNetCore.Identity;

namespace Beyond.Services.Interfaces
{
    public interface IAssignToRole
    {
        public Task<IdentityResult> Admin(User user);
        public Task<IdentityResult> User(User user);


    }
}