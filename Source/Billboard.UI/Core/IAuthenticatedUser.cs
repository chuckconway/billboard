using System.Web;
using System.Web.Security;
using Billboard.Data.Model;
using Newtonsoft.Json;

namespace Billboard.UI.Core
{
    public interface IAuthenticatedUser
    {
        /// <summary>
        /// Gets the user.
        /// </summary>
        /// <returns>User.</returns>
        User GetUserInfo();

        /// <summary>
        /// Sets the user info.
        /// </summary>
        /// <param name="user">The user.</param>
        void SetUserInfo(User user);
    }

    public class AuthenticatedUser : IAuthenticatedUser
    {
        /// <summary>
        /// Gets the user.
        /// </summary>
        /// <returns>User.</returns>
        public User GetUserInfo()
        {
            HttpContext context = HttpContext.Current;

            User user = null;
            var cookie = context.Request.Cookies[FormsAuthentication.FormsCookieName];

            if (cookie != null)
            {
                var ticket = FormsAuthentication.Decrypt(cookie.Value);

                if (ticket != null)
                {
                    var name = ticket.Name;
                    user = JsonConvert.DeserializeObject<User>(name);
                }
            }

            return user;
        }

        /// <summary>
        /// Sets the user info.
        /// </summary>
        /// <param name="user">The user.</param>
        public void SetUserInfo(User user)
        {
            HttpContext context = HttpContext.Current;
            var cookie = context.Request.Cookies[FormsAuthentication.FormsCookieName];

            if (cookie != null)
            {
                FormsAuthentication.SetAuthCookie(user.ToString(), false);
            }

        }
    }
}
