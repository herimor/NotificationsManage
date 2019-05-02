using eNotification.DAL;
using eNotification.Domain;
using eNotification.Extensions;
using System.Web.Mvc;
using System.Web.Security;

namespace NotificationService.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet]
        public ActionResult Login()
        {
            return View("Login");
        }

        [HttpPost]
        public ActionResult Login(User user)
        {
            ViewBag.Error = "Проверьте корректность ввода данных";

            if (!ModelState.IsValid)
            {
                return View("Login");
            }
            var dataProvider = new SqlUserProvider();
            var dbUser = dataProvider.GetUserByLogin(user.Login);

            if (user.Login == dbUser.Login &&
                user.Password == dbUser.Password.Decrypt())
                FormsAuthentication.RedirectFromLoginPage(user.Login, false);

            return View("Login");
        }

        [HttpPost]
        public ActionResult Logout()
        {
            Session.Clear();
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Account");
        }
    }
}