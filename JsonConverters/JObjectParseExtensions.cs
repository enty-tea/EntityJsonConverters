using LatticeUtils.Core;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntyTea.JsonConverters
{
    /// <summary>
    /// Additional methods that help with parsing values from a JObject.
    /// </summary>
    public static class JObjectParseExtensions
    {
        /// <summary>
        /// Returns the string value of the property with the specified name 
        /// or null if the property does not exist.
        /// </summary>
        /// <param name="jObject">the JObject in which to look for the property</param>
        /// <param name="name">the name of the property</param>
        /// <returns>the value of the property or null</returns>
        public static string PropertyStringValueOrDefault(this JObject jObject, string name)
        {
            var value = PropertyTokenValueOrDefault(jObject, name);
            if (value == null)
            {
                return null;
            }
            return value.ToString();
        }

        /// <summary>
        /// Returns the parsed value of the property with the specified name 
        /// or null if the property does not exist.
        /// </summary>
        /// <param name="jObject">the JObject in which to look for the property</param>
        /// <param name="name">the name of the property</param>
        /// <returns>the value of the property or null</returns>
        public static T PropertyValueOrDefault<T>(this JObject jObject, string name)
        {
            var value = PropertyTokenValueOrDefault(jObject, name);
            if (value == null)
            {
                return default(T);
            }

            // Some special handling for JToken and its subclasses (JObject, JArray).
            // Because the parse method will never be able to handle these.
            if (value is JToken && (typeof(JToken).IsAssignableFrom(typeof(T))))
            {
                return (T)(object)value;
            }

            return ParseUtils.TryParse<T>(value.ToString());
        }

        /// <summary>
        /// Returns the child object of the property with the specified name 
        /// or null if the property does not exist or the value of the property is not a JObject.
        /// </summary>
        /// <param name="jObject">the JObject in which to look for the property</param>
        /// <param name="name">the name of the property</param>
        /// <returns>the child object or null</returns>
        public static JObject PropertyObjectOrDefault(this JObject jObject, string name)
        {
            var value = PropertyTokenValueOrDefault(jObject, name);
            if (value == null)
            {
                return null;
            }

            return value as JObject;
        }

        /// <summary>
        /// Returns the child objects of the array property with the specified name 
        /// or null if the property does not exist or the value of the property is not a JArray.
        /// </summary>
        /// <param name="jObject">the JObject in which to look for the property</param>
        /// <param name="name">the name of the property</param>
        /// <returns>the child object array or null</returns>
        public static IEnumerable<JObject> PropertyObjectArrayOrDefault(this JObject jObject, string name)
        {
            var array = PropertyTokenValueOrDefault(jObject, name) as JArray;
            if (array == null)
            {
                return Enumerable.Empty<JObject>();
            }
            return array.OfType<JObject>();
        }

        /// <summary>
        /// Returns the values of the array property with the specified name 
        /// or null if the property does not exist or the value of the property is not a JArray.
        /// </summary>
        /// <param name="jObject">the JObject in which to look for the property</param>
        /// <param name="name">the name of the property</param>
        /// <returns>the values from the array or null</returns>
        public static IEnumerable<T> PropertyPrimitiveArrayOrDefault<T>(this JObject jObject, string name)
        {
            var array = PropertyTokenValueOrDefault(jObject, name) as JArray;
            if (array == null)
            {
                return Enumerable.Empty<T>();
            }
            return array.Select(x => x.ToString()).Select(ParseUtils.TryParse<T>);
        }

        #region Private

        /// <summary>
        /// Returns the string value of the property with the specified name 
        /// or null if the property does not exist.
        /// </summary>
        /// <param name="jObject">the JObject in which to look for the property</param>
        /// <param name="name">the name of the property</param>
        /// <returns>the value of the property or null</returns>
        private static JToken PropertyTokenValueOrDefault(this JObject jObject, string name)
        {
            if (jObject == null || string.IsNullOrWhiteSpace(name))
            {
                return null;
            }

            var property = jObject.Property(name);
            if (property == null)
            {
                return null;
            }

            var value = property.Value;
            if (value == null || value.Type == JTokenType.Null)
            {
                return null;
            }

            return value;
        }

        #endregion
    }
}
