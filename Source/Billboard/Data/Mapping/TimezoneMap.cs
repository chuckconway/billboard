using Billboard.Data.Model;
using FluentNHibernate.Mapping;

namespace Billboard.Data.Mapping
{
    public class TimezoneMap : ClassMap<Timezone>
    {
        public TimezoneMap()
        {
            Id(t => t.Id);
            Map(x => x.Name)
                .Not.Nullable()
                .Length(100);
            
            Map(x => x.OffsetHour)
                .Not.Nullable();

            Map(x => x.OffsetMinutes)
                .Not.Nullable();

        }
    }
}
