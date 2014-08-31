using System.Collections.Generic;
using MVCEngineeringSystemApplication.DTO;

namespace MVCEngineeringSystemApplication.ViewModels
{
    public class NavigationViewModel
    {
        private IList<Navigation> _navigation { get; set; } 
        private IEnumerable<Navigation> Navigation { get; set; }

        private void BuildNavigationModel()
        {
            
        }
    }
}