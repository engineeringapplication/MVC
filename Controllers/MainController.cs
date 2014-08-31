using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using MVCEngineeringSystemApplication.Interfaces;
using MVCEngineeringSystemApplication.Models;
using MVCEngineeringSystemApplication.Services.Data;
using MVCEngineeringSystemApplication.Services.Data.MVCEngineeringApplication.Services;
using MVCEngineeringSystemApplication.ViewModels;

namespace MVCEngineeringSystemApplication.Controllers
{
    public class MainController : Controller
    {
        //
        // GET: /Main/
        private UserService _userService = new UserService();
        private readonly CookieService _cookieService = new CookieService(System.Web.HttpContext.Current.Request, System.Web.HttpContext.Current.Response);
        private readonly IMockDataService _service = new ADODataService();
        private UserPreferenceService _preferenceService;
        private const int PageSize = 10;

        public ActionResult Index()
        {
            int userId;
            int pageId = 1;
            var userInfo = _cookieService.GetCookie("UserInfo");

            if (userInfo != null)
            {
                int.TryParse(userInfo["UserId"], out userId);
                User user = _userService.GenerateUser(userId);

                int level = 1;
                if (Session["Level"] != null)
                {
                    level = (int)Session["Level"];
                }

                DataViewModel model = new DataViewModel(_service, pageId, level);
                int valuesTotake = PageSize * pageId;
                IEnumerable<DataRow> row = model.ModelDataSet.Tables[0].AsEnumerable().ToList();
                var structure = row.Skip(valuesTotake).Take(PageSize).ToList();

                Session["PagedItems"] = structure;

                return View(new MainViewModel(user));
            }

            throw new InvalidOperationException("No user information found");
        }

        public ActionResult UserDashboardOptions()
        {
            var userInfo = _cookieService.GetCookie("UserInfo");
            int userId;
            int.TryParse(userInfo["UserId"], out userId);
            User user = _userService.GenerateUser(userId);
            UserDashboardOptionsModel model = new UserDashboardOptionsModel(user); 

            return View(model);
        }

        public JsonResult UpdateUserDashboardSettings(int chartId)
        {
            var userInfo = _cookieService.GetCookie("UserInfo");
            int userId;
            int.TryParse(userInfo["UserId"], out userId);

            _preferenceService = new UserPreferenceService(userId);
            _preferenceService.UpdateMainChart(chartId);
            
            return Json(new {success = true}, JsonRequestBehavior.AllowGet);
        }

    }
}
