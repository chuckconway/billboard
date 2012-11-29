using Billboard.Data.Model;
using FluentNHibernate.Mapping;

namespace Billboard.Data.Mapping
{
    public class MessageMap : ClassMap<Message>
    {
        public MessageMap()
        {
            Id(x => x.Id);

            Map(x => x.To)
                .Not.Nullable()
                .Length(50).Column("[To]");

            Map(x => x.From)
                .Nullable()
                .Length(250).Column("[From]");

            Map(x => x.Body)
                .Not.Nullable()
                .Length(4000);

            Map(x => x.Received)
                .Nullable();
        }
    }
}
