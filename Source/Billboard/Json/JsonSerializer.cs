using System;
using System.Runtime.Serialization;
using Newtonsoft.Json.Serialization;

namespace Billboard.Json
{
    public class NHibernateContractResolver : DefaultContractResolver
    {
        /// <summary>
        /// Determines which contract type is created for the given type.
        /// </summary>
        /// <param name="objectType">Type of the object.</param>
        /// <returns>A <see cref="T:Newtonsoft.Json.Serialization.JsonContract" /> for the given type.</returns>
        protected override JsonContract CreateContract(Type objectType)
        {
            if (objectType.IsAutoClass
                  && objectType.Namespace == null
                  && typeof(ISerializable).IsAssignableFrom(objectType))
            {

                return base.CreateObjectContract(objectType);
            }

            return base.CreateContract(objectType);
        }
    }
}
