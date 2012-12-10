using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Billboard.Data.Model;
using Billboard.UI.Areas.Dashboard.Models;
using Billboard.UI.Core;
using NHibernate;

namespace Billboard.UI.Areas.Dashboard.Controllers
{
    public class IndexController : Controller
    {
        private readonly ISession _session;
        private readonly IAuthenticatedUser _authenticatedUser;
        
        /// <summary>
        /// Initializes a new instance of the <see cref="IndexController" /> class.
        /// </summary>
        /// <param name="session">The session.</param>
        /// <param name="authenticatedUser">The authenticated user.</param>
        public IndexController(ISession session, IAuthenticatedUser authenticatedUser)
        {
            _session = session;
            _authenticatedUser = authenticatedUser;
        }

        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <returns>ActionResult.</returns>
        public ActionResult Index()
        {
            User user = _authenticatedUser.GetUserInfo();
            IList<Event> evnts;

            using (var trans = _session.BeginTransaction())
            {
                evnts = _session.QueryOver<Event>()
                        .Where(e => e.UserId == user.Id)
                        .List();

                trans.Commit();
            }

            return View(new DashboardModel { Events = evnts });
        }

        /// <summary>
        /// Adds this instance.
        /// </summary>
        /// <returns>ActionResult.</returns>
        [HttpGet]
        public ActionResult Add()
        {
            GetTimezones();
            ViewData["timezone"] = SetSelectedTimezone(); 
            return View();
        }

        /// <summary>
        /// Adds the specified new event.
        /// </summary>
        /// <param name="newEvent">The new event.</param>
        /// <returns>ActionResult.</returns>
        [HttpPost]
        public ActionResult Add(NewEventModel newEvent)
        {
            var user = _authenticatedUser.GetUserInfo();
            var evt = MapCreateEventModel(newEvent);
            evt.UserId = user.Id;

            using (var tran = _session.BeginTransaction())
            {
                _session.Save(evt);
                tran.Commit();
            }

            ViewData["timezone"] = SetSelectedTimezone();
            return View();
        }

        /// <summary>
        /// Adds this instance.
        /// </summary>
        /// <returns>ActionResult.</returns>
        [HttpGet]
        public ActionResult Edit(int? id)
        {
            Event evt;
            using (var tran = _session.BeginTransaction())
            {
                evt = _session.Get<Event>(id);
                tran.Commit();
            }

            var selectedList = SetSelectedTimezone(evt.Timezone);
            ViewData["timezone"] = selectedList;
            return View(evt);
        }

        /// <summary>
        /// Adds the specified new event.
        /// </summary>
        /// <param name="editModel">The edit model.</param>
        /// <returns>ActionResult.</returns>
        [HttpPost]
        public ActionResult Edit(EditEventModel editModel)
        {
            var user = _authenticatedUser.GetUserInfo();
            var evt = MapEditEventModel(editModel);
            evt.UserId = user.Id;

            using (var tran = _session.BeginTransaction())
            {
                _session.Save(evt);
                tran.Commit();
            }

            ViewData["timezone"] = SetSelectedTimezone(evt.Timezone);
            return View();
        }

        /// <summary>
        /// Maps the specified new event.
        /// </summary>
        /// <param name="newEvent">The new event.</param>
        /// <returns>Event.</returns>
        private Event MapCreateEventModel(NewEventModel newEvent)
        {
            var evt = new Event
                            {
                                Name = newEvent.Name,
                                FormattedNumber = newEvent.Formatted,
                                Message = newEvent.Message,
                                EndTime = DateTime.Parse(newEvent.DateEnd + " " + newEvent.TimeEnd),
                                Number = newEvent.Number,
                                StartTime = DateTime.Parse(newEvent.DateStart + " " + newEvent.TimeStart),
                                Timezone = new Timezone {Id = newEvent.Timezone}
                            };
            return evt;
        }

        /// <summary>
        /// Maps the specified new event.
        /// </summary>
        /// <param name="edit">The edit.</param>
        /// <returns>Event.</returns>
        private Event MapEditEventModel(EditEventModel edit)
        {
            var evt = new Event
            {
                Id = edit.Id,
                FormattedNumber = edit.Formatted,
                Name = edit.Name,
                Message = edit.Message,
                StartTime = DateTime.Parse(edit.DateStart + " " + edit.TimeStart),
                Number = edit.Number,
                EndTime = DateTime.Parse(edit.DateEnd + " " + edit.TimeEnd),
                Timezone = new Timezone { Id = edit.Timezone }
            };

            return evt;
        }

        /// <summary>
        /// Gets the timezones.
        /// </summary>
        private SelectList SetSelectedTimezone(Timezone selectedTimezone = null)
        {
            int id = -1;

            if (selectedTimezone != null)
            {
                id = selectedTimezone.Id;
            }

            var zones = GetTimezones();
            return new SelectList(zones, "Id", "Name", id);
        }

        /// <summary>
        /// Gets the timezones.
        /// </summary>
        /// <returns>IList{Timezone}.</returns>
        private IList<Timezone> GetTimezones()
        {
            IList<Timezone> zones;

            using (var trans = _session.BeginTransaction())
            {
                zones = _session
                    .QueryOver<Timezone>()
                    .OrderBy(t=>t.Name).Asc
                    .List();
                trans.Commit();
            }
            
            return zones;
        }
    }
}
