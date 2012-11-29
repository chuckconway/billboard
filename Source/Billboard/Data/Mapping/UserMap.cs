using Billboard.Data.Model;
using FluentNHibernate.Mapping;

namespace Billboard.Data.Mapping
{
    public class UserMap : ClassMap<User>
    {
        public UserMap()
        {
            Id(x => x.Id);

            Map(x => x.Email)
                .Not.Nullable()
                .Length(250);

            Map(x => x.Password)
                .Nullable()
                .Length(250);

            Map(x => x.DisplayName)
                .Not.Nullable()
                .Length(100);

            Map(x => x.Username)
                .Not.Nullable()
                .Length(100);
        }
    }
}
