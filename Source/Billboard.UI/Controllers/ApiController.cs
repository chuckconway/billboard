using System;
using System.IO;
using System.Text;
using System.Web.Mvc;
using Billboard.Data.Model;
using Billboard.UI.Core;
using NHibernate;
using Twilio;

namespace Billboard.UI.Controllers
{
    public class ApiController : Controller
    {
        private readonly IAuthenticatedUser _user;
        private readonly ISession _session;

        /// <summary>
        /// Initializes a new instance of the <see cref="ApiController" /> class.
        /// </summary>
        /// <param name="user">The user.</param>
        public ApiController(IAuthenticatedUser user, ISession session)
        {
            _user = user;
            _session = session;
        }

        /// <summary>
        /// Gets the available numbers numbers.
        /// </summary>
        /// <returns>ActionResult.</returns>
        public ActionResult GetAvailableNumbersNumbers()
        {
            var numbers = AvailablePhoneNumberResult();
            return Json(numbers.AvailablePhoneNumbers, "application/json");
        }

        /// <summary>
        /// Gets the number.
        /// </summary>
        /// <returns>ActionResult.</returns>
        public ActionResult GetNumber()
        {
            var numbers = AvailablePhoneNumberResult();
            var number = numbers.AvailablePhoneNumbers[0];
            return Json(new { Number = number.PhoneNumber, Formatted = number.FriendlyName});
        }

        /// <summary>
        /// Recieves the message.
        /// </summary>
        /// <returns>ActionResult.</returns>
        [AcceptVerbs("POST")]
        [AllowAnonymous]
        public ActionResult RecieveMessage()
        {
            var body = GetBytes();
            var json = Encoding.Default.GetString(body);

            var message = new Message();
            message.To = "5305213453";
            message.From = "523432324";
            message.Body = json;
            message.Received = DateTime.UtcNow;
            
            using (var trans =_session.BeginTransaction())
            {
                _session.Save(message);
                trans.Commit();
            }

            return Content(string.Empty);
        }

        /// <summary> Gets the body. </summary>
        /// <returns> The body. </returns>
        protected byte[] GetBytes()
        {
            byte[] bytes;
            using (var binaryReader = new BinaryReader(Request.InputStream))
            {
                bytes = binaryReader.ReadBytes(Request.ContentLength);
            }

            return bytes;
        }

        /// <summary>
        /// Availables the phone number result.
        /// </summary>
        /// <returns>AvailablePhoneNumberResult.</returns>
        public AvailablePhoneNumberResult AvailablePhoneNumberResult()
        {
            User user = _user.GetUserInfo();
            var twilio = new TwilioRestClient("ACfb0d36e8c09202b11963bfac14ddadda", "9be05400b471c889b4f42bbc084b74cf");

            var searchParms = new AvailablePhoneNumberListRequest();
            //searchParms.AreaCode = "907";

            var numbers = twilio.ListAvailableLocalPhoneNumbers("US", searchParms);
            return numbers;
        }
    }
}
