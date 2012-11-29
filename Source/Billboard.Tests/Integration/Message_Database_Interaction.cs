using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Billboard.Data.Model;
using NHibernate;
using NUnit.Framework;
using StructureMap;

namespace Billboard.Tests.Integration
{
    [TestFixture]
    public class Message_Database_Interaction
    {
        [SetUp]
        public void Setup()
        {
            DependencyInjection.Setup();
        }

        [Test]
        public void Message_CreateNewMessage_MessageIsSuccessfullySaved()
        {
            ISession session = ObjectFactory.GetInstance<ISession>();
            
            Message message = new Message();
            message.Body = Path.GetRandomFileName();
            message.From = new Random().Next().ToString(CultureInfo.InvariantCulture);
            message.To = new Random().Next().ToString(CultureInfo.InvariantCulture);
            message.Received = DateTime.Now;

            using (var trans = session.BeginTransaction())
            {
                session.Save(message);
                trans.Commit();
            }

        }
    }
}
