using System;
using System.Linq;

using System.Collections.Generic;
using System.Web.Mvc;


using Billboard.Data.Model;
using Billboard.Json;
using Billboard.Services.Twillio;
using Billboard.UI.Areas.Dashboard.Models;
using Billboard.UI.Core;
using NHibernate;


namespace Billboard.UI.Areas.Dashboard.Controllers
{
    public class IndexController : Controller
    {
        private readonly ISession _session;
        private readonly IAuthenticatedUser _authenticatedUser;
        private readonly ITimezoneHydration _timezoneHydration;
        private readonly ITwillioService _service;

        /// <summary>
        /// Initializes a new instance of the <see cref="IndexController" /> class.
        /// </summary>
        /// <param name="session">The session.</param>
        /// <param name="authenticatedUser">The authenticated user.</param>
        /// <param name="timezoneHydration">The timezone hydration.</param>
        /// <param name="service"></param>
        public IndexController(ISession session, IAuthenticatedUser authenticatedUser, ITimezoneHydration timezoneHydration, ITwillioService service)
        {
            _session = session;
            _authenticatedUser = authenticatedUser;
            _timezoneHydration = timezoneHydration;
            _service = service;
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
            var items = evt.ConvertAll(ConvertToEventJsonView);
            return items;
        }

        /// <summary>
        /// Converts to event json view.
        /// </summary>
        /// <param name="a">A.</param>
        /// <returns>EventJsonView.</returns>
        private EventJsonView ConvertToEventJsonView(Event a)
        {
            var start = ConvertToLocal(a.StartTime, a.Timezone.Id);
            var end = ConvertToLocal(a.EndTime, a.Timezone.Id);

            return new EventJsonView
                       {
                           Id = a.Id,
                           EndTime = end.ToShortTimeString(),
                           EndDate = end.ToShortDateString(),
                           FormattedNumber = a.FormattedNumber,
                           Name = a.Name,
                           Number = a.Number,
                           Price = a.Price,
                           StartTime = start.ToShortTimeString(),
                           StartDate = start.ToShortDateString(),
                           TimezoneName = a.Timezone.Name,
                           UserId = a.UserId,
                       };
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
            var user = _authenticatedUser.GetUserInfo();
            var addEvent = new AddEventView();

            ViewData["timezone"] = _timezoneHydration.GetAndSetSelectedTimezone(user.Timezone);
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

            evt.NumberSid = _service.ProcureNumber(evt.Number);

            using (var tran = _session.BeginTransaction())
            {
                _session.Save(evt);
                tran.Commit();
            }

            ViewData["timezone"] = _timezoneHydration.GetAndSetSelectedTimezone();
            addEvent.Message = string.Format("New event created. Phone number: {0}", evt.FormattedNumber);

            return View(addEvent);
        }

        /// <summary>
        /// Adds this instance.
        /// </summary>
        /// <returns>ActionResult.</returns>
        [HttpGet]
        public ActionResult Edit(int? id)
        {
            var editEvent = new EditEventView();

            if (id != null)
            {
                var evt = GetEvent(id.Value);

                var selectedList = _timezoneHydration.GetAndSetSelectedTimezone(evt.Timezone);
                ViewData["timezone"] = selectedList;
                editEvent.Event = ConvertToEventJsonView(evt); 
            }

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
            var oldEvent = GetEvent(editModel.Id);
            var editEvent = new EditEventView();
            var evt = MapEditEventModel(oldEvent,editModel);

            if (oldEvent.Number != editModel.Number)
            {
                evt.NumberSid = _service.ProcureNumber(evt.Number);
                ReleaseOldNumber(editModel);
            }

            using (var trans =_session.BeginTransaction())
            {
                _session.Update(evt);
                trans.Commit();
            }

            ViewData["timezone"] = _timezoneHydration.GetAndSetSelectedTimezone(evt.Timezone);
            editEvent.Message = "The event has been updated.";
            editEvent.Event = ConvertToEventJsonView(evt);

            return View(editEvent);
        }

        /// <summary>
        /// Releases the old number.
        /// </summary>
        /// <param name="editModel">The edit model.</param>
        private void ReleaseOldNumber(EditEventModel editModel)
        {
            var e = GetEvent(editModel.Id);
            _service.ReleaseNumber(e.NumberSid);
        }

        /// <summary>
        /// Gets the event.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>Event.</returns>
        private Event GetEvent(int id)
        {
            using (var tran = _session.BeginTransaction())
            {
                var oldEvent = _session.Get<Event>(id);
                tran.Commit();
                _session.Flush();

                return oldEvent;
            }
        }

        /// <summary>
        /// Deletes the specified id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>ActionResult.</returns>
        public ActionResult Delete(int id)
        {
            var e = GetEvent(id);
            _service.ReleaseNumber(e.NumberSid);

            return Content(string.Empty);
        }

        /// <summary>
        /// Converts to UTC.
        /// </summary>
        /// <param name="localtime">The localtime.</param>
        /// <param name="timezone">The timezone.</param>
        /// <returns>DateTime.</returns>
        private DateTime ConvertToUtc(DateTime localtime, int timezone)
        {
           var tz = _timezoneHydration.GetTimezones().Single(t => t.Id == timezone);
            var offset = new DateTimeOffset(localtime, new TimeSpan(tz.OffsetHour, tz.OffsetMinutes, 0));

            return offset.UtcDateTime;
        }

        /// <summary>
        /// Converts to local.
        /// </summary>
        /// <param name="utcTime">The UTC time.</param>
        /// <param name="timezone">The timezone.</param>
        /// <returns>DateTime.</returns>
        private DateTime ConvertToLocal(DateTime utcTime, int timezone)
        {
            var tz = _timezoneHydration.GetTimezones().Single(t => t.Id == timezone);
            return utcTime.AddHours(tz.OffsetHour).AddMinutes(tz.OffsetMinutes);
        }

        /// <summary>
        /// Maps the specified new event.
        /// </summary>
        /// <param name="newEvent">The new event.</param>
        /// <returns>Event.</returns>
        private Event MapCreateEventModel(NewEventModel newEvent)
        {
            Func<string, string, int, DateTime> toUtc = (d, t, i) => ConvertToUtc(DateTime.Parse(d + " " + t), i);

            var evt = new Event
                            {
                                Name = newEvent.Name,
                                BackgroundColor = newEvent.BackgroundColor,
                                TextColor = newEvent.TextColor,
                                FormattedNumber = newEvent.Formatted,
                                EndTime = toUtc(newEvent.DateEnd, newEvent.TimeEnd, newEvent.Timezone),
                                Number = newEvent.Number,
                                StartTime = toUtc(newEvent.DateStart, newEvent.TimeStart, newEvent.Timezone),
                                Timezone = new Timezone {Id = newEvent.Timezone}
                            };
            return evt;
        }

        /// <summary>
        /// Maps the specified new event.
        /// </summary>
        /// <param name="edit">The edit.</param>
        /// <returns>Event.</returns>
        private Event MapEditEventModel(Event e, EditEventModel edit)
        {
            Func<string, string, int, DateTime> toUtc = (d, t, i) => ConvertToUtc(DateTime.Parse(d + " " + t), i);

            e.FormattedNumber = edit.Formatted;
            e.BackgroundColor = edit.BackgroundColor;
            e.TextColor = edit.TextColor;
            e.Name = edit.Name;
            e.StartTime = toUtc(edit.DateStart, edit.TimeStart, edit.Timezone);
            e.Number = edit.Number;
            e.EndTime = toUtc(edit.DateEnd, edit.TimeEnd, edit.Timezone);
            e.Timezone = new Timezone {Id = edit.Timezone};

            return e;
        }
        
    }
}
