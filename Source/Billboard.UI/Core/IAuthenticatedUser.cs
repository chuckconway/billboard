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
    }

    public class AuthenticatedUser : IAuthenticatedUser
    {
        private HttpRequest _request;

        /// <summary>
        /// Gets the user.
        /// </summary>
        /// <returns>User.</returns>
        public User GetUserInfo()
        {
            _request = HttpContext.Current.Request;

            User user = null;
            var cookie = _request.Cookies[FormsAuthentication.FormsCookieName];

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
    }
}
