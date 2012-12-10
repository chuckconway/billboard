using System.Web.Mvc;
using Billboard.Data.Model;
using Billboard.UI.Core;
using Twilio;

namespace Billboard.UI.Controllers
{
    public class ApiController : Controller
    {
        private readonly IAuthenticatedUser _user;

        /// <summary>
        /// Initializes a new instance of the <see cref="ApiController" /> class.
        /// </summary>
        /// <param name="user">The user.</param>
        public ApiController(IAuthenticatedUser user)
        {
            _user = user;
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
