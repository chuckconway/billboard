using System;

namespace Billboard.Data.Model
{
    public class Event
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        /// <value>The id.</value>
        public virtual int Id { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        public virtual string Name { get; set; }

        /// <summary>
        /// Gets or sets the number id.
        /// </summary>
        /// <value>The number id.</value>
        public virtual string NumberSid { get; set; }

        /// <summary>
        /// Gets or sets the start time.
        /// </summary>
        /// <value>The start time.</value>
        public virtual DateTime StartTime { get; set; }

        /// <summary>
        /// Gets or sets the end time.
        /// </summary>
        /// <value>The end time.</value>
        public virtual DateTime EndTime { get; set; }

        /// <summary>
        /// Gets or sets the number.
        /// </summary>
        /// <value>The number.</value>
        public virtual string Number { get; set; }

        /// <summary>
        /// Gets or sets the formatted number.
        /// </summary>
        /// <value>The formatted number.</value>
        public virtual string FormattedNumber { get; set; }

        /// <summary>
        /// Gets or sets the user id.
        /// </summary>
        /// <value>The user id.</value>
        public virtual int UserId { get; set; }

        /// <summary>
        /// Gets or sets the price.
        /// </summary>
        /// <value>The price.</value>
        public virtual decimal Price { get; set; }

        /// <summary>
        /// Gets or sets the venue.
        /// </summary>
        /// <value>The venue.</value>
        public virtual string BackgroundColor { get; set; }

        /// <summary>
        /// Gets or sets the venue.
        /// </summary>
        /// <value>The venue.</value>
        public virtual string TextColor { get; set; }

        /// <summary>
        /// Gets or sets the timezone.
        /// </summary>
        /// <value>The timezone.</value>
        public virtual Timezone Timezone { get; set; }
    }
}
