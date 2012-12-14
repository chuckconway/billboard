using System.Collections.Generic;
using System.Linq;
using Twilio;

namespace Billboard.Services.Twillio
{
    public class TwillioService : ITwillioService
    {
        readonly TwilioRestClient _twilio = new TwilioRestClient("ACfb0d36e8c09202b11963bfac14ddadda", "9be05400b471c889b4f42bbc084b74cf");

        /// <summary>
        /// Availables the phone number result.
        /// </summary>
        /// <returns>AvailablePhoneNumberResult.</returns>
        public List<PhoneNumber> AvailablePhoneNumberResult()
        {
            var searchParms = new AvailablePhoneNumberListRequest();

            var numbers = _twilio.ListAvailableLocalPhoneNumbers("US", searchParms);
            return numbers.AvailablePhoneNumbers.Select(i=>new PhoneNumber {FormattedNumber = i.FriendlyName, Number = i.PhoneNumber}).ToList();
        }

        /// <summary>
        /// Releases the number.
        /// </summary>
        /// <param name="incomingNumberSid">The incoming number sid.</param>
        public void ReleaseNumber(string incomingNumberSid)
        {
            _twilio.DeleteIncomingPhoneNumber(incomingNumberSid);
        }

        /// <summary>
        /// Procures the number.
        /// </summary>
        /// <param name="phoneNumber">The phone number.</param>
        public string ProcureNumber(string phoneNumber)
        {
           var number = _twilio.AddIncomingPhoneNumber(new PhoneNumberOptions { PhoneNumber = phoneNumber, SmsMethod = "POST", SmsUrl = "http://3cjr.com/api/receivemessage" });
            return number.Sid;
        }
    
    }
}