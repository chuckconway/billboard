using System.Collections.Generic;
using System.Web.Mvc;
using Billboard.Data.Model;

namespace Billboard.UI.Core
{
    public interface ITimezoneHydration
    {
        /// <summary>
        /// Gets the and set selected timezone.
        /// </summary>
        /// <param name="selectedTimezone">The selected timezone.</param>
        /// <returns>SelectList.</returns>
        SelectList GetAndSetSelectedTimezone(Timezone selectedTimezone = null);

        /// <summary>
        /// Gets the timezones.
        /// </summary>
        /// <returns>IEnumerable{Timezone}.</returns>
        IEnumerable<Timezone> GetTimezones();
    }
}