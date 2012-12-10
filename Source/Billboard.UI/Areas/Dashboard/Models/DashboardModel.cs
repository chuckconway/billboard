using System.Collections.Generic;
using Billboard.Data.Model;

namespace Billboard.UI.Areas.Dashboard.Models
{
    public class DashboardModel
    {
        /// <summary>
        /// Gets or sets the events.
        /// </summary>
        /// <value>The events.</value>
        public IList<Event> Events { get; set; }

    }
}