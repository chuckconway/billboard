using Billboard.Data.Model;
using Billboard.UI.Core;

namespace Billboard.Tests.Core
{
    public class MockAuthenticatedUser : IAuthenticatedUser
    {
        /// <summary>
        /// Gets the user.
        /// </summary>
        /// <returns>User.</returns>
        public User GetUserInfo()
        {
            return new User {Id = 1004};
        }

        public void SetUserInfo(User user)
        {
            throw new System.NotImplementedException();
        }
    }
}
