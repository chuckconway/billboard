using System.Web.Mvc;

using System.Linq;
using Billboard.Data.Model;
using Hypersonic;

namespace Billboard.UI.Controllers
{
    public class MessagesController : Controller
    {
        private readonly IDatabase _database;

        /// <summary>
        /// Initializes a new instance of the <see cref="MessagesController" /> class.
        /// </summary>
        /// <param name="database">The database.</param>
        public MessagesController(IDatabase database)
        {
            _database = database;
        }

        /// <summary>
        /// Events this instance.
        /// </summary>
        /// <returns>ActionResult.</returns>
        [AllowAnonymous]
        public ActionResult Event(int id)
        {
           var message = _database.List<Message, object>("Message_GetMessageByEventId", new {eventid=id}).ToList();
           return View(message);
        }

    }
}
