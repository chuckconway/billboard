using Billboard.Data.Model;
using FluentNHibernate.Mapping;

namespace Billboard.Data.Mapping
{
    public class AliasMapping : ClassMap<Alias>
    {
        public AliasMapping()
        {
            Id(a => a.Id);

            Map(a => a.Name)
                .Not.Nullable()
                .Length(150);

            Map(a => a.Number)
                .Not.Nullable()
                .Length(50);

            HasOne(a => a.Event)
                .ForeignKey("EventId");
        }
    }
}
