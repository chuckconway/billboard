using System.Web.Mvc;
using System.Web.Security;
using Billboard.Data.Model;
using Newtonsoft.Json;

namespace Billboard.UI.Core
{
    //public class AuthenticatedBaseController : Controller
    //{
    //    /// <summary>
    //    /// Gets the user.
    //    /// </summary>
    //    /// <returns>User.</returns>
    //    public User GetUser()
    //    {
    //        User user = null;
    //        var cookie = Request.Cookies[FormsAuthentication.FormsCookieName];

    //        if (cookie != null)
    //        {
    //           var ticket = FormsAuthentication.Decrypt(cookie.Value);

    //            if (ticket != null)
    //            {
    //                string name = ticket.Name;
    //                user = JsonConvert.DeserializeObject<User>(name);
    //            }
    //        }

    //        return user;
    //    }
    //}
}