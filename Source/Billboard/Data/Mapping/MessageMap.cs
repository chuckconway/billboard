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

            Map(x => x.AccountSid)
                .Nullable()
                .Length(50);

            Map(x => x.ToCity)
                .Nullable()
                .Length(50);

            Map(x => x.SmsSid)
                .Nullable()
                .Length(50);

            Map(x => x.FromCountry)
                .Nullable()
                .Length(50);

            Map(x => x.ToCountry)
                .Nullable()
                .Length(50);

            Map(x => x.SmsMessageSid)
                .Nullable()
                .Length(50);

            Map(x => x.ApiVersion)
                .Nullable()
                .Length(50);

            Map(x => x.FromState)
                .Nullable()
                .Length(50);

            Map(x => x.ToZip)
                .Nullable()
                .Length(50);

            Map(x => x.ToState)
                .Nullable()
                .Length(50);
        }
    }
}
