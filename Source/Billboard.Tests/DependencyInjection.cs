using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Billboard.Data.Mapping;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using StructureMap;

namespace Billboard.Tests
{
    public class DependencyInjection
    {
        /// <summary> Registers the dependency injection. </summary>
        public static void Setup()
        {
            ObjectFactory.Initialize(x => x.Scan(s =>
            {

                x.For<ISession>().Use(CreateSessionFactory().OpenSession);

                s.TheCallingAssembly();
                s.WithDefaultConventions();

            }));

            ObjectFactory.AssertConfigurationIsValid();
        }

        /// <summary>
        /// Creates the session factory.
        /// </summary>
        /// <returns>ISessionFactory.</returns>
        private static ISessionFactory CreateSessionFactory()
        {
            return Fluently.Configure()
                    .Database(MsSqlConfiguration.MsSql2012
                    .ConnectionString(c => c
                        .FromConnectionStringWithKey("sql")))
                    .Mappings(m => m.FluentMappings.AddFromAssemblyOf<UserMap>())
                    .BuildSessionFactory();
        }
    }
}
