using System.Linq;
using System.Security.Claims;
using BusinessSystem.Database.Models;
using BusinessSystem.Database.Models.BusinessObjects;

namespace BusinessSystem.CRM.Manager
{
    public class UserManager
    {
        Database.DataBaseContext _dataContext;
        public UserManager(Database.DataBaseContext dataContext)
        {
            _dataContext = dataContext;
        }
        public UserEntityModel GetCurrentUser(ClaimsPrincipal claimsPrincipal)
        {
            var userClaims = claimsPrincipal.Claims.FirstOrDefault(x => x.Type == "UserId");
            if (userClaims == null)
                return null;

            int.TryParse(userClaims.Value, out int userId);
            var user = _dataContext.Users.GetAsync(userId).GetAwaiter().GetResult();
            return user;
        }
    }
}
