using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using Billboard.Data.Mapping;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using StructureMap;

namespace Billboard.UI
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            RegisterDependencyInjection();
        }

        private void RegisterDependencyInjection()
        {
            ObjectFactory.Initialize(x => x.Scan(s =>
            {
                //x.For<IDatabase>().Use(new MsSqlDatabase());
                //x.For<IConfigurationService>().Use<MomntzConfiguration>();
                x.For<ISession>().HttpContextScoped().Use(CreateSessionFactory().OpenSession);
                //x.For<IProjectionProcessor>().Use<ProjectionProcessor>();

                s.TheCallingAssembly();
                s.WithDefaultConventions();

                //s.ConnectImplementationsToTypesClosing(typeof(IFormHandler<>));
               // s.ConnectImplementationsToTypesClosing(typeof(IQueryHandler<,>));

                //x.For<IInjection>().Use(new StructureMapInjection());
            }));

            //ModelBinders.Binders.Add(typeof(NewTag), new NewTagModelBinder());
           // ModelBinders.Binders.Add(typeof(UsernameAndPassword), new UsernameAndPasswordModelBinder());

            ObjectFactory.AssertConfigurationIsValid();
            DependencyResolver.SetResolver(new StructureMapDependencyResolver());
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

        internal class StructureMapDependencyResolver : IDependencyResolver
        {
            /// <summary>
            /// Resolves singly registered services that support arbitrary object creation.
            /// </summary>
            /// <param name="serviceType">The type of the requested service or object.</param>
            /// <returns>The requested service or object.</returns>
            public object GetService(Type serviceType)
            {
                if (serviceType.IsAbstract || serviceType.IsInterface)
                {
                    return ObjectFactory.TryGetInstance(serviceType);
                }

                return ObjectFactory.GetInstance(serviceType);
            }

            /// <summary>
            /// Resolves multiply registered services.
            /// </summary>
            /// <param name="serviceType">The type of the requested services.</param>
            /// <returns>The requested services.</returns>
            public IEnumerable<object> GetServices(Type serviceType)
            {
                return ObjectFactory.GetAllInstances(serviceType).Cast<object>();
            }
        }
    }
}