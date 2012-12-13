using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Billboard.UI.Models.Api
{
    public class TwillioMessage
    {
        /// <summary>
        /// Gets or sets the account sid.
        /// </summary>
        /// <value>The account sid.</value>
        public string AccountSid { get; set; }

        /// <summary>
        /// Gets or sets the body.
        /// </summary>
        /// <value>The body.</value>
        public string Body { get; set; }

        /// <summary>
        /// Gets or sets from.
        /// </summary>
        /// <value>From.</value>
        public string From { get; set; }

        /// <summary>
        /// Gets or sets to zip.
        /// </summary>
        /// <value>To zip.</value>
        public string ToZip { get; set; }

        /// <summary>
        /// Gets or sets from state.
        /// </summary>
        /// <value>From state.</value>
        public string FromState { get; set; }

        /// <summary>
        /// Gets or sets to city.
        /// </summary>
        /// <value>To city.</value>
        public string ToCity { get; set; }

        /// <summary>
        /// Gets or sets the SMS sid.
        /// </summary>
        /// <value>The SMS sid.</value>
        public string SmsSid { get; set; }

        /// <summary>
        /// Gets or sets to state.
        /// </summary>
        /// <value>To state.</value>
        public string ToState { get; set; }

        /// <summary>
        /// Gets or sets to.
        /// </summary>
        /// <value>To.</value>
        public string To { get; set; }

        /// <summary>
        /// Gets or sets to country.
        /// </summary>
        /// <value>To country.</value>
        public string ToCountry { get; set; }

        /// <summary>
        /// Gets or sets from country.
        /// </summary>
        /// <value>From country.</value>
        public string FromCountry { get; set; }

        /// <summary>
        /// Gets or sets the SMS messagte sid.
        /// </summary>
        /// <value>The SMS messagte sid.</value>
        public string SmsMessageSid { get; set; }

        /// <summary>
        /// Gets or sets the API version.
        /// </summary>
        /// <value>The API version.</value>
        public string ApiVersion { get; set; }
    }
}