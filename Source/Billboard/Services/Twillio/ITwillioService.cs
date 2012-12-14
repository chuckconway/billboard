using System.Collections.Generic;

namespace Billboard.Services.Twillio
{
    public interface ITwillioService
    {
        /// <summary>
        /// Availables the phone number result.
        /// </summary>
        /// <returns>AvailablePhoneNumberResult.</returns>
        List<PhoneNumber> AvailablePhoneNumberResult();

        /// <summary>
        /// Releases the number.
        /// </summary>
        /// <param name="numberSid">The number sid.</param>
        void ReleaseNumber(string numberSid);

        /// <summary>
        /// Procures the number.
        /// </summary>
        /// <param name="phoneNumber">The phone number.</param>
        string ProcureNumber(string phoneNumber);

    }

    public class PhoneNumber
    {
        /// <summary>
        /// Gets or sets the number.
        /// </summary>
        /// <value>The number.</value>
        public string Number { get; set; }

        /// <summary>
        /// Gets or sets the formatted number.
        /// </summary>
        /// <value>The formatted number.</value>
        public string FormattedNumber { get; set; }

        /// <summary>
        /// Gets or sets the number sid.
        /// </summary>
        /// <value>The number sid.</value>
        public string NumberSid { get; set; }
    }
}
