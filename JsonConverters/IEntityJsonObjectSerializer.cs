using System;
using System.Collections.Generic;
using System.Linq;

namespace EntyTea.JsonConverters
{
    /// <summary>
    /// Serializes an entity to a JSON object.
    /// </summary>
    /// <typeparam name="TEntity">the type of entity to serialize</typeparam>
    public interface IEntityJsonObjectSerializer<in TEntity>
    {
        /// <summary>
        /// Returns an object that represents the JSON data for this entity.
        /// This would normally be an anonymous object, where each property in the object is a property in the JSON document.
        /// </summary>
        /// <param name="entity">the entity to serialize or null</param>
        /// <returns>the JSON object representation of the entity or null</returns>
        object SerializeToJsonObject(TEntity entity);
    }
}