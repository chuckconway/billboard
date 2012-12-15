namespace Billboard.UI.Areas.Dashboard.Models
{
    public class NewEventModel
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the start time.
        /// </summary>
        /// <value>The start time.</value>
        public string DateStart { get; set; }

        /// <summary>
        /// Gets or sets the end time.
        /// </summary>
        /// <value>The end time.</value>
        public string TimeStart { get; set; }

        /// <summary>
        /// Gets or sets the start time.
        /// </summary>
        /// <value>The start time.</value>
        public string DateEnd { get; set; }

        /// <summary>
        /// Gets or sets the end time.
        /// </summary>
        /// <value>The end time.</value>
        public string TimeEnd { get; set; }

        /// <summary>
        /// Gets or sets the timezone.
        /// </summary>
        /// <value>The timezone.</value>
        public int Timezone { get; set; }

        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        /// <value>The message.</value>
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets the number.
        /// </summary>
        /// <value>The number.</value>
        public string Number { get; set; }

        /// <summary>
        /// Gets or sets the venue.
        /// </summary>
        /// <value>The venue.</value>
        public string Venue { get; set; }

        /// <summary>
        /// Gets or sets the number messages displayed.
        /// </summary>
        /// <value>The number messages displayed.</value>
        public byte NumberMessagesDisplayed { get; set; }

        /// <summary>
        /// Gets or sets the formatted.
        /// </summary>
        /// <value>The formatted.</value>
        public string Formatted { get; set; }
    }
}