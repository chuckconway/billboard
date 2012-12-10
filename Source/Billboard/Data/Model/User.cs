using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization;
using NHibernate.Proxy;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Billboard.Data.Model
{
    public class User
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        /// <value>The id.</value>
        public virtual int Id { get; set; }

        /// <summary>
        /// Gets or sets the username.
        /// </summary>
        /// <value>The username.</value>
        public virtual string Username { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>The password.</value>
        public virtual string Password { get; set; }

        /// <summary>
        /// Gets or sets the display name.
        /// </summary>
        /// <value>The display name.</value>
        public virtual string DisplayName { get; set; }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>The email.</value>
        public virtual string Email { get; set; }

        /// <summary>
        /// Gets or sets the timezone.
        /// </summary>
        /// <value>The timezone.</value>
        public virtual Timezone Timezone { get; set; }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>A <see cref="System.String" /> that represents this instance.</returns>
        public override string ToString()
        {
            var serializedObject = SerializedObject();
            return serializedObject;
        }

        /// <summary>
        /// Serializeds the object.
        /// </summary>
        /// <returns>System.String.</returns>
        private string SerializedObject()
        {
            var json = new JsonSerializer
                           {
                               ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                               ContractResolver = new NHibernateContractResolver()
                           };

            var stringWriter = new StringWriter();
            JsonWriter jsonWriter = new JsonTextWriter(stringWriter);
            json.Serialize(jsonWriter, this);
            string serializedObject = stringWriter.ToString();
            return serializedObject;
        }
    }

    public class NHibernateContractResolver : DefaultContractResolver
    {
        protected override JsonContract CreateContract(System.Type objectType)
        {
            /* Behavior in base we're overriding:
            if (typeof(ISerializable).IsAssignableFrom(objectType))
                return CreateISerializableContract(objectType);
            //*/

            if (objectType.IsAutoClass
                  && objectType.Namespace == null
                  && typeof(ISerializable).IsAssignableFrom(objectType))
            {

                return base.CreateObjectContract(objectType);
            }

            return base.CreateContract(objectType);
        }
        ///// <summary>
        ///// Gets the serializable members for the type.
        ///// </summary>
        ///// <param name="objectType">The type to get serializable members for.</param>
        ///// <returns>The serializable members for the type.</returns>
        //protected override List<MemberInfo> GetSerializableMembers(Type objectType)
        //{
        //    if (typeof(DefaultLazyInitializer).IsAssignableFrom(objectType))
        //    {
        //        return new List<MemberInfo>();
        //    }

        //    return base.GetSerializableMembers(objectType);
        //}
    }
}
