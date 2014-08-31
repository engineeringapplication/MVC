using System.Web;
using System.Web.Mvc;
using MVCEngineeringSystemApplication.Interfaces;
using MVCEngineeringSystemApplication.Models;
using MVCEngineeringSystemApplication.Services.Data;
using MVCEngineeringSystemApplication.Services.Login;

namespace MVCEngineeringSystemApplication.Controllers
{
    public class AuthoriseController : Controller
    {
        //
        // GET: /Authorise/
        private readonly ILogin _loginService = new LoginService();
        private readonly CookieService _cookieService = new CookieService(System.Web.HttpContext.Current.Request, System.Web.HttpContext.Current.Response);

        public ActionResult Login()
        {
            HttpCookie cookie = _cookieService.GetCookie("UserInfo");

            return View();
        }

        public JsonResult Authenticate(LoginModel model)
        {
            var user = _loginService.ValidateLogin(model.Username, model.Password);

            if  (user != null)
            {
                _cookieService.CreateUserCookie(user);               
                return Json(new {Authorised = true, RedirectUrl = Url.Action("Index", "Main")});
            }

            // Unauthorised
            return Json(new {Authorised = false});
        }
    }
}
