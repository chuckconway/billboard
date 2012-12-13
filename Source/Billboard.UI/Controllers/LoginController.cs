using System.Web.Mvc;
using System.Web.Security;
using Billboard.Data.Model;
using Billboard.UI.Areas.Dashboard.Controllers;
using Billboard.UI.Core;
using NHibernate;

namespace Billboard.UI.Controllers
{
    [AllowAnonymous]
    public class LoginController : Controller
    {
        private readonly ISession _session;
        private readonly IAuthenticatedUser _authenticatedUser;

        /// <summary>
        /// Initializes a new instance of the <see cref="IndexController" /> class.
        /// </summary>
        /// <param name="session">The session.</param>
        public LoginController(ISession session, IAuthenticatedUser authenticatedUser)
        {
            _session = session;
            _authenticatedUser = authenticatedUser;
        }

        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <returns>ActionResult.</returns>
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Indexes the specified username.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        /// <returns>ActionResult.</returns>
        [HttpPost]
        public ActionResult Index(string username, string password)
        {
            ActionResult result = Redirect("/Dashboard/index");
            User user = GetUser(username, password);

            if (user == null)
            {
                result = View();
            }
            else
            {
                FormsAuthentication.SetAuthCookie(user.ToString(), false); 
            }

            return result;
        }

        /// <summary>
        /// Gets the user.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        /// <returns>User.</returns>
        private User GetUser(string username, string password)
        {
            User user;
            using (var trans = _session.BeginTransaction())
            {
                user = _session.QueryOver<User>()
                    .Where(u => u.Username == username && u.Password == password)
                    .SingleOrDefault();

                trans.Commit();
            }
            return user;
        }
    }
}
