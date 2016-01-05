using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Hunter.Services.Rest
{
    /// <summary>
    /// The serializer that represents objects as HTTP parameters ready to be used in the URL.
    /// </summary>
    public class HttpParamsSerializer
    {
        /// <summary>
        /// Serializes the specified object into the HTTP parameters string.
        /// </summary>
        /// <param name="obj">The object to be serialized.</param>
        /// <returns>
        /// The HTTP parameters string.
        /// </returns>
        public string Serialize(object obj)
        {
	        var type = obj.GetType().GetTypeInfo();

            if (type.IsValueType || type.AsType() == typeof(string))
                throw new InvalidOperationException("Only complex types are supported");

            return serializeClass(obj);
        }

        public string Serialize(IDictionary<string, string> parameters)
        {
            if (parameters == null || parameters.Count == 0)
                return String.Empty;

            var builder = new StringBuilder();
            builder.Append("?");
            foreach (var pair in parameters)
            {
                builder.AppendFormat("{0}={1}&", pair.Key, Uri.EscapeDataString(pair.Value));
            }
            return builder.ToString().TrimEnd('&');
        }

        public string Serialize(IList<string> pathParts)
        {
            if (pathParts == null || pathParts.Count == 0)
                return String.Empty;
            var builder = new StringBuilder();
            foreach (var part in pathParts)
            {
                builder.AppendFormat("/{0}", Uri.EscapeUriString(part));
            }
            return builder.ToString();
        }

        private static string serializeClass(object obj)
        {
	        var properties = obj.GetType().GetRuntimeProperties().Where(pi => !pi.GetMethod.IsStatic | pi.GetMethod.IsPublic);

            var builder = new StringBuilder();
            builder.Append("?");
            foreach (var property in properties)
            {
                builder.AppendFormat("{0}={1}&", property.Name, property.GetValue(obj, new object[] { }));
            }

            // Delete last unnecessary "&".
            builder.Remove(builder.Length - 1, 1);

            return builder.ToString();
        }
    }
}
