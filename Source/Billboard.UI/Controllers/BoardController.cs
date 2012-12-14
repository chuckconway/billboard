using System.Web.Mvc;
using Billboard.Data.Model;
using Billboard.UI.Core.Services;
using Billboard.UI.Models.Board;
using Newtonsoft.Json;
using ISession = NHibernate.ISession;

namespace Billboard.UI.Controllers
{
    public class BoardController : Controller
    {
        private readonly ISession _session;
        private readonly IMessageService _messageService;

        /// <summary>
        /// Initializes a new instance of the <see cref="BoardController" /> class.
        /// </summary>
        /// <param name="session">The session.</param>
        /// <param name="messageService">The message service.</param>
        public BoardController(ISession session, IMessageService messageService)
        {
            _session = session;
            _messageService = messageService;
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

            var items = _messageService.GetMessages(evt);

            string json = JsonConvert.SerializeObject(items);
            return Content(json, "application/json; charset=utf-8");
        }

    }
}
