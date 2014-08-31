using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Web;
using System.Web.Http.Routing;
using MVCEngineeringSystemApplication.DTO;
using MVCEngineeringSystemApplication.Models;
using MVCEngineeringSystemApplication.Services.Data;
using MVCEngineeringSystemApplication.Services.Data.MVCEngineeringApplication.Services;

namespace MVCEngineeringSystemApplication.ViewModels
{
    public class MainViewModel
    {
        private readonly User LoggedInUser;
        private readonly UserService _userService;
        private UserPreferenceService preferenceService;
        public readonly string Username;
        public readonly List<string> UserRole;
        public List<Navigation> UserNavigation { get; private set; }
        public DataViewModel PaginationViewModel { get; set; }
        public UserDashboardOption UserDashboardPreference { get; set; }
        public bool ShowDataGrid { get; set; }

        public MainViewModel(User user)
        {
            Username = user.Username;
            _userService = new UserService();
            UserRole = new List<string>();
            PaginationViewModel = new DataViewModel(new ADODataService(), Constants.Constants.StartingPage, Constants.Constants.PagingLevel);
            preferenceService = new UserPreferenceService(user.UserId);
            UserDashboardPreference = preferenceService.GetUserPreferences().FirstOrDefault();

            if (LoggedInUser == null)
            {
                LoggedInUser = _userService.GenerateUser(user.UserId);
            }
            
            foreach (var role in user.UserRoles)
            {
                UserRole.Add(role.Role.Description);    
            }

            BuildRoleNavigationStructure();
        }

        private void BuildRoleNavigationStructure()
        {
            if (UserNavigation == null)
            {
                UserNavigation = new List<Navigation>();
                Navigation userOptions = new Navigation();
                Navigation userOptions2 = new Navigation();
                userOptions.LinkDescription = "Dashboard";
                userOptions2.LinkDescription = "Options";
                
                UserNavigation.Add(userOptions);
                UserNavigation.Add(userOptions2);
            }

            bool isAdmin = _userService.IsAdmin(LoggedInUser);

            if (isAdmin)
            {
                Navigation adminOptions = new Navigation();
                adminOptions.LinkDescription = "Administative Options";
                UserNavigation.Add(adminOptions);
            }
        }
    }
}