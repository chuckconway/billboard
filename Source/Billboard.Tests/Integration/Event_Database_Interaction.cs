using System;
using System.IO;
using Billboard.Data.Model;
using NHibernate;
using NUnit.Framework;
using StructureMap;

namespace Billboard.Tests.Integration
{
    [TestFixture]
    public class Event_Database_Interaction
    {
        [SetUp]
        public void Setup()
        {
            DependencyInjection.Setup();   
        }

        [Test]
        public void Event_CreateNewEvent_EventIsSuccessfullySaved()
        {
            ISession session = ObjectFactory.GetInstance<ISession>();

            User user = new User();
            user.DisplayName = Path.GetRandomFileName();
            user.Email = Path.GetRandomFileName();
            user.Password = Path.GetRandomFileName();
            user.Username = Path.GetRandomFileName();

            using (var trans = session.BeginTransaction())
            {
                session.Save(user);
                trans.Commit();
            }

            using (var trans = session.BeginTransaction())
            {
                Event evnt = new Event();
                evnt.Name = Path.GetRandomFileName();
                evnt.UserId = user.Id;
                evnt.StartTime = DateTime.Now;
                evnt.EndTime = DateTime.Now.AddDays(1);

                session.Save(evnt);
                trans.Commit();
                
            }
        }
    }
}
