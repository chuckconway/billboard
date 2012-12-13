using System.Web.Mvc;
using Billboard.Data.Model;
using Billboard.UI.Areas.Dashboard.Models;
using Billboard.UI.Core;
using NHibernate;

namespace Billboard.UI.Areas.Dashboard.Controllers
{
    public class SettingsController : Controller
    {
        private readonly ISession _session;
        private readonly ITimezoneHydration _hydration;
        private readonly IAuthenticatedUser _authenticatedUser;

        /// <summary>
        /// Initializes a new instance of the <see cref="SettingsController" /> class.
        /// </summary>
        /// <param name="session">The session.</param>
        /// <param name="hydration">The hydration.</param>
        /// <param name="authenticatedUser">The authenticated user.</param>
        public SettingsController(ISession session, ITimezoneHydration hydration, IAuthenticatedUser authenticatedUser)
        {
            _session = session;
            _hydration = hydration;
            _authenticatedUser = authenticatedUser;
        }

        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <returns>ActionResult.</returns>
        public ActionResult Index()
        {
            User user = _authenticatedUser.GetUserInfo();

            var view = new SettingsView
            {
                User = user,
            };

            ViewData["timezone"] = _hydration.GetAndSetSelectedTimezone(user.Timezone);
            return View(view);
        }

        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <returns>ActionResult.</returns>
        [HttpPost]
        public ActionResult Index(PostSettingsView settings)
        {
            var user = SaveUser(settings);
            _authenticatedUser.SetUserInfo(user);

            var view = new SettingsView
                                    {
                                        User = user,
                                        Message = "User changes saved."
                                    };

            ViewData["timezone"] = _hydration.GetAndSetSelectedTimezone(user.Timezone);
            return View(view);
        }

        /// <summary>
        /// Saves the user.
        /// </summary>
        /// <param name="settings">The settings.</param>
        /// <returns>User.</returns>
        private User SaveUser(PostSettingsView settings)
        {
            User user = _authenticatedUser.GetUserInfo();

            user.DisplayName = settings.DisplayName;
            user.Email = settings.Email;
            user.Password = settings.Password;
            user.Timezone = new Timezone {Id = settings.Timezone};

            using (var tran = _session.BeginTransaction())
            {
                _session.Update(user);
                tran.Commit();
            }

            return user;
        }
    }
}
