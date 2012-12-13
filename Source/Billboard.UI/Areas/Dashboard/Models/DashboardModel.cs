using System.Collections.Generic;

namespace Billboard.UI.Areas.Dashboard.Models
{
    public class DashboardModel
    {
        /// <summary>
        /// Gets or sets the events.
        /// </summary>
        /// <value>The events.</value>
        public IList<EventJsonView> Events { get; set; }

    }
}