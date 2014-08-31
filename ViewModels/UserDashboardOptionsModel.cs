using System.Collections.Generic;

namespace MVCEngineeringSystemApplication.ViewModels
{
    public class UserDashboardOptionsModel
    {
        public int UserId { get; private set; }
        public string UserName { get; private set; }
        public List<string> UserWidgets { get; private set; }

        public UserDashboardOptionsModel(User user)
        {
            UserId = user.UserId;
            UserName = user.Username;
        }

        public bool UpdateUserPreference()
        {
            return true;
        }
    }
}