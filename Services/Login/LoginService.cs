using System.Linq;
using MVCEngineeringSystemApplication.Interfaces;

namespace MVCEngineeringSystemApplication.Services.Login
{
    public class LoginService : ILogin
    {
        private readonly EngineeringSystemEntities _entities = new EngineeringSystemEntities();

        public User ValidateLogin(string username, string password)
        {
            return _entities.Users.FirstOrDefault(u => u.Username == username && u.Password == password);
        }
    }
}