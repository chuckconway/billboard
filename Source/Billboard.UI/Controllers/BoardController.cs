using System;
using System.Web.Mvc;
using Billboard.Data.Model;
using Billboard.UI.Models.Board;
using Hypersonic;
using Newtonsoft.Json;
using ISession = NHibernate.ISession;

namespace Billboard.UI.Controllers
{
    public class BoardController : Controller
    {
        private readonly ISession _session;
        private readonly IDatabase _database;

        public BoardController(ISession session, IDatabase database)
        {
            _session = session;
            _database = database;
        }


        /// <summary>
        /// Indexes the specified id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>ActionResult.</returns>
        public ActionResult Index(int id)
        {
            Event evt;

            using (var trans = _session.BeginTransaction())
            {
                evt = _session.QueryOver<Event>()
                                    .Where(e => e.Id == id)
                                    .SingleOrDefault();

                trans.Commit();
            }

            return View(new BoardView { Event = evt });
        }

        /// <summary>
        /// Messageses the specified id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>ActionResult.</returns>
        public ActionResult Messages(int id)
        {
            Event evt;

            using (var trans = _session.BeginTransaction())
            {
                evt = _session.QueryOver<Event>()
                                    .Where(e => e.Id == id)
                                    .SingleOrDefault();

                trans.Commit();
            }

            DateTime now = DateTime.UtcNow.AddHours(evt.Timezone.OffsetHour);
            now = now.AddMinutes(evt.Timezone.OffsetMinutes);

            var items =_database.List<Message, object>("[dbo].[Message_GetMessageByEventId]", new {eventId = id, now});

            string json = JsonConvert.SerializeObject(items);
            return Content(json, "application/json; charset=utf-8");
        }

    }
}
