using System.Collections.Generic;
using System.Linq;

namespace MVCEngineeringSystemApplication.Services.Data
{
    public class UserPreferenceService
    {
        private readonly EngineeringSystemEntities _entities = new EngineeringSystemEntities();
        private int _userId { get; set; }

        public UserPreferenceService(int userId)
        {
            _userId = userId;
        }

        public List<UserDashboardOption> GetUserPreferences()
        {
            return _entities.UserDashboardOptions.Where(ud => ud.UserId == _userId).ToList();
        }

        public bool UpdateMainChart(int chartId)
        {
            var existingOptions = _entities.UserDashboardOptions.Where(ud => ud.UserId == _userId);

            if (existingOptions.Count() == 1)
            {
                existingOptions.First().UserDashboardOption1 = chartId;
                _entities.SaveChanges();
                return true;
            }

            return false;
        }
    }
}