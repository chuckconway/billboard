using System.IO;
using Newtonsoft.Json;

namespace Billboard.Json
{
    public class JsonResolver
    {
        /// <summary>
        /// Serializeds the object.
        /// </summary>
        /// <returns>System.String.</returns>
        public string Serialize(object value)
        {
            var json = new JsonSerializer
                           {
                               ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                               ContractResolver = new NHibernateContractResolver()
                           };

            var stringWriter = new StringWriter();
            JsonWriter jsonWriter = new JsonTextWriter(stringWriter);
            json.Serialize(jsonWriter, value);
            string serializedObject = stringWriter.ToString();
            return serializedObject;
        }
    }
}