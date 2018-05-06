using StoreBL.Utils;
using StoreModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace StoreBL.Services.Interfaces
{
    public interface IUserService: IDisposable
    {
        Task<OperationDetails> Create(ApplicationUser user, string password);
        Task<ClaimsIdentity> Authenticate(string email, string password);

        IEnumerable<ApplicationUser> GetUsers();
        IEnumerable<ApplicationUser> GetUsers(OrderStatus status);

        ApplicationUser GetUserById(string userId);

       
    }
}
