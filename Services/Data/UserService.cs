using System.Linq;
using MVCEngineeringSystemApplication.Constants;

namespace MVCEngineeringSystemApplication.Services.Data
{
    public class UserService
    {
        private readonly EngineeringSystemEntities _entities = new EngineeringSystemEntities();

        public User GenerateUser(int userId)
        {
            return _entities.Users.FirstOrDefault(u => u.UserId == userId);
        }

        public bool IsAdmin(User user)
        {
            if (user.UserRoles.Any(role => role.Role.RoleId == (int) UserRoles.Admin))
            {
                return true;
            }

            return false;
        }
    }
}