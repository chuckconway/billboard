using System;

namespace Billboard.Data.Model
{
    public class Message
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        /// <value>The id.</value>
        public virtual int Id { get; set; }

        /// <summary>
        /// Gets or sets from.
        /// </summary>
        /// <value>From.</value>
        public virtual string From { get; set; }

        /// <summary>
        /// Gets or sets to.
        /// </summary>
        /// <value>To.</value>
        public virtual string To { get; set; }

        /// <summary>
        /// Gets or sets the body.
        /// </summary>
        /// <value>The body.</value>
        public virtual string Body { get; set; }

        /// <summary>
        /// Gets or sets the recieved.
        /// </summary>
        /// <value>The recieved.</value>
        public virtual DateTime Received { get; set; }

        /// <summary>
        /// Gets or sets the account sid.
        /// </summary>
        /// <value>The account sid.</value>
        public virtual string AccountSid { get; set; }
        
        /// <summary>
        /// Gets or sets to zip.
        /// </summary>
        /// <value>To zip.</value>
        public virtual string ToZip { get; set; }

        /// <summary>
        /// Gets or sets from state.
        /// </summary>
        /// <value>From state.</value>
        public virtual string FromState { get; set; }

        /// <summary>
        /// Gets or sets to city.
        /// </summary>
        /// <value>To city.</value>
        public virtual string ToCity { get; set; }

        /// <summary>
        /// Gets or sets the SMS sid.
        /// </summary>
        /// <value>The SMS sid.</value>
        public virtual string SmsSid { get; set; }

        /// <summary>
        /// Gets or sets to state.
        /// </summary>
        /// <value>To state.</value>
        public virtual string ToState { get; set; }

        /// <summary>
        /// Gets or sets to country.
        /// </summary>
        /// <value>To country.</value>
        public virtual string ToCountry { get; set; }

        /// <summary>
        /// Gets or sets from country.
        /// </summary>
        /// <value>From country.</value>
        public virtual string FromCountry { get; set; }

        /// <summary>
        /// Gets or sets the SMS messagte sid.
        /// </summary>
        /// <value>The SMS messagte sid.</value>
        public virtual string SmsMessageSid { get; set; }

        /// <summary>
        /// Gets or sets the API version.
        /// </summary>
        /// <value>The API version.</value>
        public virtual string ApiVersion { get; set; }
    }
}
