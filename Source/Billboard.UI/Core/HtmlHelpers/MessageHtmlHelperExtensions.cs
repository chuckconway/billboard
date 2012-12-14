using System.Web.Mvc;

namespace Billboard.UI.Core.HtmlHelpers
{
    public static class MessageHtmlHelperExtensions
    {
        /// <summary>
        /// Successes the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns>MvcHtmlString.</returns>
        public static MvcHtmlString Success(this string message)
        {
            var msg = new MvcHtmlString(string.Empty);

            if (!string.IsNullOrEmpty(message))
            {
                msg = new MvcHtmlString(string.Format("<p style=\"margin-top:15px;\" class=\"text-success\" >{0}<p>", message));
            }

            return msg;
        }
    }
}