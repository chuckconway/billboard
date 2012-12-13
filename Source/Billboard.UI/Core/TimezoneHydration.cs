using System.Collections.Generic;
using System.Web.Mvc;
using Billboard.Data.Model;
using NHibernate;

namespace Billboard.UI.Core
{
    public class TimezoneHydration : ITimezoneHydration
    {
        private readonly ISession _session;

        /// <summary>
        /// Initializes a new instance of the <see cref="TimezoneHydration" /> class.
        /// </summary>
        /// <param name="session">The session.</param>
        public TimezoneHydration(ISession session)
        {
            _session = session;
        }

        /// <summary>
        /// Gets the timezones.
        /// </summary>
        public SelectList GetAndSetSelectedTimezone(Timezone selectedTimezone = null)
        {
            var id = -1;

            if (selectedTimezone != null)
            {
                id = selectedTimezone.Id;
            }

            var zones = GetTimezones();
            return new SelectList(zones, "Id", "Name", id);
        }

        /// <summary>
        /// Gets the timezones.
        /// </summary>
        /// <returns>IList{Timezone}.</returns>
        private IEnumerable<Timezone> GetTimezones()
        {
            IList<Timezone> zones;

            using (var trans = _session.BeginTransaction())
            {
                zones = _session
                    .QueryOver<Timezone>()
                    .OrderBy(t => t.Name).Asc
                    .List();
                trans.Commit();
            }

            return zones;
        }
    }
}
