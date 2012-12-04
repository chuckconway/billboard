﻿using Billboard.Data.Model;
using FluentNHibernate.Mapping;

namespace Billboard.Data.Mapping
{
    public class EventMap : ClassMap<Event>
    {
        public EventMap()
        {
            Id(x => x.Id);

            Map(x => x.Name)
                .Not.Nullable()
                .Length(200);

            Map(x => x.StartTime)
                .Not.Nullable();

            Map(x => x.EndTime)
                .Not.Nullable();

            Map(x => x.Number)
                .Nullable()
                .Length(50);

            Map(x => x.UserId)
                .Not.Nullable();

            Map(x => x.Price)
                .Nullable();

            References(m => m.Timezone, "TimezoneId");
        }
    }
}
