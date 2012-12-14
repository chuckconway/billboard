using System.Collections.Generic;
using Billboard.Data.Model;
using Billboard.UI.Models.Board;

namespace Billboard.UI.Core.Services
{
    public interface IMessageService
    {
        /// <summary>
        /// Gets the messages.
        /// </summary>
        /// <param name="evt">The evt.</param>
        /// <returns>List{BoardMessage}.</returns>
        List<BoardMessage> GetMessages(Event evt);
    }
}
