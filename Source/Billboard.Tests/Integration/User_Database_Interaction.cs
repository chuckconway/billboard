using System.Diagnostics;
using System.IO;
using Billboard.Data.Model;
using NHibernate;
using NUnit.Framework;
using StructureMap;

namespace Billboard.Tests.Integration
{
    [TestFixture]
    public class User_Database_Interaction
    {
        [SetUp]
        public void Setup()
        {
            DependencyInjection.Setup();
        }

        [Test]
        public void User_PersitToDatabase_UserIsSavedToTheDatabase()
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

            User userResult;

            using (var trans = session.BeginTransaction())
            {
                userResult = session.QueryOver<User>()
                       .Where(u => u.Username == user.Username)
                       .SingleOrDefault();

                trans.Commit();
            }

            Assert.NotNull(userResult, "User from database is not null.");
            StringAssert.AreEqualIgnoringCase(userResult.Username, user.Username, "Usernames are the same."); 
        }
    }
}
