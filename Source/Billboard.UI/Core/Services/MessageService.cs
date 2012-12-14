using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Billboard.Data.Model;
using Billboard.UI.Models.Board;
using Hypersonic;
using ISession = NHibernate.ISession;

namespace Billboard.UI.Core.Services
{
    public class MessageService : IMessageService
    {
        private readonly ISession _session;
        private readonly IDatabase _database;

        IDictionary<string, Alias> _aliasLookup = new Dictionary<string, Alias>();

        /// <summary>
        /// Initializes a new instance of the <see cref="MessageService" /> class.
        /// </summary>
        /// <param name="session">The session.</param>
        /// <param name="database">The database.</param>
        public MessageService(ISession session, IDatabase database)
        {
            _session = session;
            _database = database;
        }

        /// <summary>
        /// Gets the messages.
        /// </summary>
        /// <param name="evt">The evt.</param>
        /// <returns>List{BoardMessage}.</returns>
        public List<BoardMessage> GetMessages(Event evt)
        {
            var messages = _database.List<Message, object>("[dbo].[Message_GetMessageByEventId]", new { eventId = evt.Id }).ToList();
            
            using (var trans = _session.BeginTransaction())
            {
                //var i = _session.QueryOver<Alias>()
                //                .Where(a => a.Event == evt)
                //                .List()
                //                .ToDictionary(a=>a.Number);

                trans.Commit();
            }

            messages = SetHashTag(evt, messages);

            var items = messages.Select(m => ConvertToBoardMessage(m, evt.Timezone));
            return items.ToList();
        }

        /// <summary>
        /// Sets the hash tag.
        /// </summary>
        /// <param name="evt">The evt.</param>
        /// <param name="messages">The messages.</param>
        /// <returns>List{Message}.</returns>
        private List<Message> SetHashTag(Event evt, List<Message> messages)
        {
            foreach (var message in messages)
            {
                if (message.Body.Contains("#"))
                {
                    //Find HashTag
                    var collection = Regex.Matches(message.Body, @"(?:\s|\A|^)[##]+([A-Za-z0-9-_]+)");
                    string name = collection[0].Value;

                    message.Body = message.Body.Replace(name, string.Empty);
                    
                    //Remove Hashtag
                    name = name.Replace("#", string.Empty);
                    SaveAlias(evt, name, message);
                }
            }

            return messages;
        }

        /// <summary>
        /// Saves the alias.
        /// </summary>
        /// <param name="evt">The evt.</param>
        /// <param name="name">The name.</param>
        /// <param name="message">The message.</param>
        private void SaveAlias(Event evt, string name, Message message)
        {
            bool hasTagInDictionary = _aliasLookup.ContainsKey(message.From);
            var alias = new Alias { Event = evt, Name = name, Number = message.From };

            //get Instance of existing hashtag
            if (hasTagInDictionary)
            {
                alias = _aliasLookup[message.From];
                alias.Name = name;
            }

            //save hashtage
            using (var trans = _session.BeginTransaction())
            {
                _session.SaveOrUpdate(alias);
                trans.Commit();
            }

            //update hashtag collection
            if (!hasTagInDictionary)
            {
                _aliasLookup.Add(message.From, alias);
            }
        }

        /// <summary>
        /// Converts Messages to BoardMessage.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="timezone">The timezone.</param>
        /// <returns>BoardMessage.</returns>
        private BoardMessage ConvertToBoardMessage(Message message, Timezone timezone)
        {
            string number = string.Format("{0:(###) ###-####}", Convert.ToInt64(message.From.Replace("+1", string.Empty)));
            string @from = _aliasLookup.ContainsKey(message.From) ? string.Format("{0} {1}", _aliasLookup[message.From].Name, number) : number;

            return new BoardMessage
                       {
                           From = @from,
                           Message = message.Body,
                           Time = message.Received.AddHours(timezone.OffsetHour).AddMinutes(timezone.OffsetMinutes).ToShortTimeString()
                       };
        }
    }
}