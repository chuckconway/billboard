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
    }
}
