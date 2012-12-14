namespace Billboard.UI.Areas.Dashboard.Models
{
    public class EditEventView
    {
        /// <summary>
        /// Gets or sets the event.
        /// </summary>
        /// <value>The event.</value>
        public EventJsonView Event { get; set; }

        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        /// <value>The message.</value>
        public string Message { get; set; }
    }
}