using System;
using System.Web.Mvc;
using Billboard.Data.Model;
using Billboard.Services.Twillio;
using Billboard.UI.Models.Api;
using NHibernate;

namespace Billboard.UI.Controllers
{
    public class ApiController : Controller
    {
        private readonly ISession _session;
        private readonly ITwillioService _service;

        /// <summary>
        /// Initializes a new instance of the <see cref="ApiController" /> class.
        /// </summary>
        /// <param name="session">The session.</param>
        /// <param name="service"></param>
        public ApiController(ISession session, ITwillioService service)
        {
            _session = session;
            _service = service;
        }

        /// <summary>
        /// Gets the available numbers numbers.
        /// </summary>
        /// <returns>ActionResult.</returns>
        public ActionResult GetAvailableNumbersNumbers()
        {
            var numbers = _service.AvailablePhoneNumberResult();
            return Json(numbers, "application/json");
        }

        /// <summary>
        /// Gets the number.
        /// </summary>
        /// <returns>ActionResult.</returns>
        public ActionResult GetNumber()
        {
            var numbers = _service.AvailablePhoneNumberResult();
            var number = numbers[0];
            return Json(new {number.Number, Formatted = number.FormattedNumber});
        }

        /// <summary>
        /// Recieves the message.
        /// </summary>
        /// <returns>ActionResult.</returns>
        //[HttpPost]
        [AllowAnonymous]
        public ActionResult ReceiveMessage(TwillioMessage message)
        {
           var msg = AutoMapper.Mapper.DynamicMap<Message>(message);
           msg.Received = DateTime.UtcNow;
            
            using (var trans =_session.BeginTransaction())
            {
                _session.Save(msg);
                trans.Commit();
            }

            string xml = "<?xml version=\"1.0\"" +
            " encoding=\"UTF-8\" ?>" +
            "<Response>" +
            "</Response>";

            return Content(xml, "text/xml");
        }
    }
}
