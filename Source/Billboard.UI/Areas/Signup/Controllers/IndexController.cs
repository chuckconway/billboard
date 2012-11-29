using System.Web.Mvc;

namespace Billboard.UI.Areas.Signup.Controllers
{
    public class IndexController : Controller
    {

        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <returns>ActionResult.</returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Indexes the specified username.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        /// <param name="confirm">The confirm.</param>
        /// <param name="displayname">The displayname.</param>
        /// <returns>ActionResult.</returns>
        public ActionResult Index(string username, string password, string confirm, string displayname)
        {
            return View();
        }


    }
}
