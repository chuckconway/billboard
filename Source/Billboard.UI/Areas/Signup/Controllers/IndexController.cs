using System.Collections.Generic;
using System.Globalization;
using System.Web.Mvc;
using Billboard.Data.Model;
using System.Linq;
using Billboard.UI.Areas.Signup.Models;
using NHibernate;

namespace Billboard.UI.Areas.Signup.Controllers
{
    public class IndexController : Controller
    {
        private readonly ISession _session;
        static IEnumerable<SelectListItem> _items;

        /// <summary>
        /// Initializes a new instance of the <see cref="IndexController" /> class.
        /// </summary>
        /// <param name="session">The session.</param>
        public IndexController(ISession session)
        {
            _session = session;
        }

        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <returns>ActionResult.</returns>
        [HttpGet]
        public ActionResult Index()
        {
            GetTimezones();
            return View(_items);
        }

        /// <summary>
        /// Gets the timezones.
        /// </summary>
        private void GetTimezones()
        {
            if (_items == null)
            {
                using (var trans = _session.BeginTransaction())
                {
                    var zones = _session
                        .QueryOver<Timezone>()
                        .List();
                    trans.Commit();

                    _items = zones.Select( i => new SelectListItem {Text = i.Name, Value = i.Id.ToString(CultureInfo.InvariantCulture)});
                }
            }
        }

        /// <summary>
        /// Indexes the specified username.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>ActionResult.</returns>
        [HttpPost]
        public ActionResult Index(SignupModel user)
        {
            using (var trans = _session.BeginTransaction())
            {
                _session.Save(new User
                                  {
                                      DisplayName = user.DisplayName,
                                      Email = user.Email,
                                      Password = user.Password,
                                      Timezone = new Timezone {Id = user.Timezone},
                                      Username = user.Username
                                  });
                trans.Commit();
            }

            GetTimezones();

            return View(_items);
        }


    }
}
