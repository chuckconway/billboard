using System.Collections.Generic;
using Billboard.Data.Model;
using NHibernate;
using NUnit.Framework;
using StructureMap;

namespace Billboard.Tests.Integration
{
    public class Timezone_Database_Interaction
    {
        [SetUpAttribute]
        public void Setup()
        {
            DependencyInjection.Setup();
        }

        [Test]
        public void Timezone_RetrieveFromDatabase_SuccessfullyGetItems()
        {
            var session = ObjectFactory.GetInstance<ISession>();
            IList<Timezone> items = new List<Timezone>();
            using (var trans = session.BeginTransaction())
            {
                 items = session.QueryOver<Timezone>()
                       .List();
                trans.Commit();
            }

            Assert.Greater(items.Count, 0, "At least one timeone was retreived.");
        }
    }
}
