using System;
using System.Linq;

using System.Collections.Generic;
using System.Web.Mvc;


using Billboard.Data.Model;
using Billboard.Json;
using Billboard.UI.Areas.Dashboard.Models;
using Billboard.UI.Core;
using NHibernate;
using Twilio;

namespace Billboard.UI.Areas.Dashboard.Controllers
{
    public class IndexController : Controller
    {
        private readonly ISession _session;
        private readonly IAuthenticatedUser _authenticatedUser;
        private readonly ITimezoneHydration _timezoneHydration;

        /// <summary>
        /// Initializes a new instance of the <see cref="IndexController" /> class.
        /// </summary>
        /// <param name="session">The session.</param>
        /// <param name="authenticatedUser">The authenticated user.</param>
        /// <param name="timezoneHydration">The timezone hydration.</param>
        public IndexController(ISession session, IAuthenticatedUser authenticatedUser, ITimezoneHydration timezoneHydration)
        {
            _session = session;
            _authenticatedUser = authenticatedUser;
            _timezoneHydration = timezoneHydration;
        }

        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <returns>ActionResult.</returns>
        public ActionResult Index()
        {
            var evnts = GetEvents();
            return View(new DashboardModel { Events = evnts });
        }

        /// <summary>
        /// Events this instance.
        /// </summary>
        /// <returns>ActionResult.</returns>
        public ActionResult Events()
        {
            var resolver = new JsonResolver();
            var evt = GetEvents();

            return Content(resolver.Serialize(evt), "application/json");
        }

        /// <summary>
        /// Gets the events.
        /// </summary>
        /// <returns>List{EventJsonView}.</returns>
        private List<EventJsonView> GetEvents()
        {
           var evt = GetAllEventsForUser();

            var items = evt.ConvertAll(
                a => new EventJsonView
                         {
                             Id = a.Id,
                             EndTime =  a.EndTime.ToShortTimeString(),
                             EndDate = a.EndTime.ToShortDateString(),
                             FormattedNumber = a.FormattedNumber,
                             Message = a.Message,
                             Name = a.Name,
                             Number = a.Number,
                             Price = a.Price,
                             StartTime = a.StartTime.ToShortTimeString(),
                             StartDate = a.StartTime.ToShortDateString(),
                             TimezoneName = a.Timezone.Name,
                             UserId = a.UserId
                         }
                );

            return items;
        }

        /// <summary>
        /// Gets the events.
        /// </summary>
        /// <returns>IList{Event}.</returns>
        private List<Event> GetAllEventsForUser()
        {
            var user = _authenticatedUser.GetUserInfo();
            List<Event> evnts;

            using (var trans = _session.BeginTransaction())
            {
                evnts = _session.QueryOver<Event>()
                                .Where(e => e.UserId == user.Id)
                                .OrderBy(e => e.StartTime).Desc
                                .List()
                                .ToList();

                trans.Commit();
            }

            return evnts;
        }

        /// <summary>
        /// Adds this instance.
        /// </summary>
        /// <returns>ActionResult.</returns>
        [HttpGet]
        public ActionResult Add()
        {
            var addEvent = new AddEventView();

            ViewData["timezone"] = _timezoneHydration.GetAndSetSelectedTimezone();
            return View(addEvent);
        }

        /// <summary>
        /// Adds the specified new event.
        /// </summary>
        /// <param name="newEvent">The new event.</param>
        /// <returns>ActionResult.</returns>
        [HttpPost]
        public ActionResult Add(NewEventModel newEvent)
        {
            var addEvent = new AddEventView();

            var user = _authenticatedUser.GetUserInfo();
            var evt = MapCreateEventModel(newEvent);
            evt.UserId = user.Id;

            ProcurePhoneNumber(evt.Number);


            using (var tran = _session.BeginTransaction())
            {
                _session.Save(evt);
                tran.Commit();
            }

            ViewData["timezone"] = _timezoneHydration.GetAndSetSelectedTimezone();
            return View(addEvent);
        }

        /// <summary>
        /// Procures the phone number.
        /// </summary>
        /// <param name="phoneNumber">The phone number.</param>
        private void ProcurePhoneNumber(string phoneNumber)
        {
            var twilio = new TwilioRestClient("ACfb0d36e8c09202b11963bfac14ddadda", "9be05400b471c889b4f42bbc084b74cf");
           var number = twilio.AddIncomingPhoneNumber(new PhoneNumberOptions {PhoneNumber = phoneNumber, SmsMethod = "POST", SmsUrl = "http://3cjr.com/api/receivemessage"});
        }

        /// <summary>
        /// Adds this instance.
        /// </summary>
        /// <returns>ActionResult.</returns>
        [HttpGet]
        public ActionResult Edit(int? id)
        {
            var editEvent = new EditEventView();

            Event evt;
            using (var tran = _session.BeginTransaction())
            {
                evt = _session.Get<Event>(id);
                tran.Commit();
            }

            var selectedList = _timezoneHydration.GetAndSetSelectedTimezone(evt.Timezone);
            ViewData["timezone"] = selectedList;
            editEvent.Event = evt;

            return View(editEvent);
        }

        /// <summary>
        /// Adds the specified new event.
        /// </summary>
        /// <param name="editModel">The edit model.</param>
        /// <returns>ActionResult.</returns>
        [HttpPost]
        public ActionResult Edit(EditEventModel editModel)
        {
            var editEvent = new EditEventView();

            var user = _authenticatedUser.GetUserInfo();
            var evt = MapEditEventModel(editModel);
            evt.UserId = user.Id;

            using (var tran = _session.BeginTransaction())
            {
                _session.Update(evt);
                tran.Commit();
            }

            ViewData["timezone"] = _timezoneHydration.GetAndSetSelectedTimezone(evt.Timezone);
            editEvent.Message = "The event has been updated.";
            editEvent.Event = evt;

            return View(editEvent);
        }

        /// <summary>
        /// Deletes the specified id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>ActionResult.</returns>
        public ActionResult Delete(int id)
        {
            using (var tran = _session.BeginTransaction())
            {
                var evt = _session.Get<Event>(id);
                _session.Delete(evt);
                tran.Commit();
            }

            return Content(string.Empty);
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
        
    }
}
