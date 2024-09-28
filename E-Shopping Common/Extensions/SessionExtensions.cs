using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Text;

namespace E_Shopping_Common.Extensions
{
    public static class SessionExtensions
    {
        // Serialize an object and store it in the session as a byte array
        public static void Set<T>(this ISession session, string key, T value)
        {
            // Convert the object to JSON and then to a byte array
            var jsonData = JsonConvert.SerializeObject(value);
            var byteArray = Encoding.UTF8.GetBytes(jsonData);
            session.Set(key, byteArray); // Use Set to store the byte array
        }

        // Retrieve an object from the session and deserialize it
        public static T Get<T>(this ISession session, string key)
        {
            if (session.TryGetValue(key, out byte[] value))
            {
                // Convert the byte array back to a JSON string and then deserialize to the object
                var jsonData = Encoding.UTF8.GetString(value);
                return JsonConvert.DeserializeObject<T>(jsonData);
            }
            return default; // Return default if the key does not exist
        }
    }
}
