using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace EntyTea.JsonConverters
{
    /// <summary>
    /// Serializes an entity to a JObject.
    /// </summary>
    /// <typeparam name="TEntity">the type of entity to serialize</typeparam>
    public interface IEntityJObjectSerializer<in TEntity>
    {
        /// <summary>
        /// Returns a JObject that represents the JSON data for this entity.
        /// </summary>
        /// <param name="entity">the entity to serialize or null</param>
        /// <returns>the JObject representation of the entity or null</returns>
        JObject SerializeToJObject(TEntity entity);
    }
}