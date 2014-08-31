using System;
using System.Globalization;
using System.Web;

namespace MVCEngineeringSystemApplication.Services.Data
{
    public class CookieService
    {
        private readonly HttpRequest _request;
        private readonly HttpResponse _response;
        private const int CookieExpirationTime = 0;

        public CookieService(HttpRequest request, HttpResponse response)
        {
            _request = request;
            _response = response;
        }

        public bool CreateUserCookie(User user)
        {
            HttpCookie cookie = new HttpCookie("UserInfo");
            cookie["Test"] = "1";
            cookie.Values.Add("Username", user.Username);
            cookie.Values.Add("UserId", user.UserId.ToString(CultureInfo.CurrentCulture));
            cookie.Expires = DateTime.Now.AddDays(1);

            foreach (var role in user.UserRoles)
            {
                cookie.Values.Add("UserRole", role.Role.Description);
            }

            _response.Cookies.Add(cookie);
            return true;
        }

        public bool AddCookie(string name, string value)
        {
            HttpCookie cookie = new HttpCookie(name, value);

            _response.Cookies.Add(cookie);

            return true;
        }

        public HttpCookie GetCookie(string name)
        {
            return _request.Cookies.Get(name);
        }

        public bool RemoveCookie(string name)
        {
            _request.Cookies.Remove(name);
            return true;
        }
    }
}