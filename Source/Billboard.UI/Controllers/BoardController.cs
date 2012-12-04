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
            return View(new BoardView {EventId = id});
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


            //using (var trans = _session.BeginTransaction())
            //{
            //  evt = _session.QueryOver<Event>()
            //            .Where(e => e.StartTime <= now)
            //            .And(e => e.EndTime >= now)
            //            .And(e => e.Id == id)
            //            .SingleOrDefault();
                
            //    msges = query.List<Message>();
            //    trans.Commit();
            //}


            //var messages = new[]
            //                   {
            //                       new {Id=Randomizer.NextNumber(1000), From="916.123.1076", Body="Lorem ipsum dolor sit amet, consectetur adipiscing elit. Maecenas erat nunc, varius quis fermentum sed, convallis eget mi. Sed tellus tellus, malesuada sed sollicitudin et, interdum ut sapien. ", Received=DateTime.Now.ToShortTimeString()},
            //                       new {Id=Randomizer.NextNumber(1000), From="916.123.9076", Body="Lorem ipsum dolor sit amet, consectetur adipiscing elit. Maecenas erat nunc, varius quis fermentum sed, convallis eget mi. Sed tellus tellus, malesuada sed sollicitudin et, interdum ut sapien. ", Received=DateTime.Now.ToShortTimeString()},
            //                       new {Id=Randomizer.NextNumber(1000), From="916.123.0076", Body="Lorem ipsum dolor sit amet, consectetur adipiscing elit. Maecenas erat nunc, varius quis fermentum sed, convallis eget mi. Sed tellus tellus, malesuada sed sollicitudin et, interdum ut sapien. ", Received=DateTime.Now.ToShortTimeString()},
            //                       new {Id=Randomizer.NextNumber(1000), From="916.123.9076", Body="Lorem ipsum dolor sit amet, consectetur adipiscing elit. Maecenas erat nunc, varius quis fermentum sed, convallis eget mi. Sed tellus tellus, malesuada sed sollicitudin et, interdum ut sapien. ", Received=DateTime.Now.ToShortTimeString()},
            //                       new {Id=Randomizer.NextNumber(1000), From="916.123.9076", Body="Lorem ipsum dolor sit amet, consectetur adipiscing elit. Maecenas erat nunc, varius quis fermentum sed, convallis eget mi. Sed tellus tellus, malesuada sed sollicitudin et, interdum ut sapien. ", Received=DateTime.Now.ToShortTimeString()},
            //                       new {Id=Randomizer.NextNumber(1000), From="916.123.8076", Body="Lorem ipsum dolor sit amet, consectetur adipiscing elit. Maecenas erat nunc, varius quis fermentum sed, convallis eget mi. Sed tellus tellus, malesuada sed sollicitudin et, interdum ut sapien. ", Received=DateTime.Now.ToShortTimeString()},
            //                       new {Id=Randomizer.NextNumber(1000), From="916.123.9076", Body="Lorem ipsum dolor sit amet, consectetur adipiscing elit. Maecenas erat nunc, varius quis fermentum sed, convallis eget mi. Sed tellus tellus, malesuada sed sollicitudin et, interdum ut sapien. ", Received=DateTime.Now.ToShortTimeString()},
            //                       new {Id=Randomizer.NextNumber(1000), From="916.123.9076", Body="Lorem ipsum dolor sit amet, consectetur adipiscing elit. Maecenas erat nunc, varius quis fermentum sed, convallis eget mi. Sed tellus tellus, malesuada sed sollicitudin et, interdum ut sapien. ", Received=DateTime.Now.ToShortTimeString()},
            //                       new {Id=Randomizer.NextNumber(1000), From="916.123.9075", Body="Lorem ipsum dolor sit amet, consectetur adipiscing elit. Maecenas erat nunc, varius quis fermentum sed, convallis eget mi. Sed tellus tellus, malesuada sed sollicitudin et, interdum ut sapien. ", Received=DateTime.Now.ToShortTimeString()},
            //                       new {Id=Randomizer.NextNumber(1000), From="916.123.9076", Body="Lorem ipsum dolor sit amet, consectetur adipiscing elit. Maecenas erat nunc, varius quis fermentum sed, convallis eget mi. Sed tellus tellus, malesuada sed sollicitudin et, interdum ut sapien. ", Received=DateTime.Now.ToShortTimeString()},
            //                       new {Id=Randomizer.NextNumber(1000), From="916.123.6076", Body="Lorem ipsum dolor sit amet, consectetur adipiscing elit. Maecenas erat nunc, varius quis fermentum sed, convallis eget mi. Sed tellus tellus, malesuada sed sollicitudin et, interdum ut sapien. ", Received=DateTime.Now.ToShortTimeString()},
            //                       new {Id=Randomizer.NextNumber(1000), From="916.123.9074", Body="Lorem ipsum dolor sit amet, consectetur adipiscing elit. Maecenas erat nunc, varius quis fermentum sed, convallis eget mi. Sed tellus tellus, malesuada sed sollicitudin et, interdum ut sapien. ", Received=DateTime.Now.ToShortTimeString()},
            //                       new {Id=Randomizer.NextNumber(1000), From="916.123.9076", Body="Lorem ipsum dolor sit amet, consectetur adipiscing elit. Maecenas erat nunc, varius quis fermentum sed, convallis eget mi. Sed tellus tellus, malesuada sed sollicitudin et, interdum ut sapien. ", Received=DateTime.Now.ToShortTimeString()},
            //                       new {Id=Randomizer.NextNumber(1000), From="916.123.9073", Body="Lorem ipsum dolor sit amet, consectetur adipiscing elit. Maecenas erat nunc, varius quis fermentum sed, convallis eget mi. Sed tellus tellus, malesuada sed sollicitudin et, interdum ut sapien. ", Received=DateTime.Now.ToShortTimeString()},
            //                       new {Id=Randomizer.NextNumber(1000), From="916.123.9076", Body="Lorem ipsum dolor sit amet, consectetur adipiscing elit. Maecenas erat nunc, varius quis fermentum sed, convallis eget mi. Sed tellus tellus, malesuada sed sollicitudin et, interdum ut sapien. ", Received=DateTime.Now.ToShortTimeString()},
            //                   };

            string json = JsonConvert.SerializeObject(items);
            return Content(json, "application/json; charset=utf-8");
        }

    }
}
