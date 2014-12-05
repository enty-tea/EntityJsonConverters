using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace EntyTea.JsonConverters
{
    /// <summary>
    /// Serializes an entity to a JSON string.
    /// </summary>
    /// <typeparam name="TEntity">the type of entity to serialize</typeparam>
    public interface IEntityJsonSerializer<in TEntity>
    {
        /// <summary>
        /// Returns a JSON string representation of the entity.
        /// </summary>
        /// <param name="entity">the entity to serialize or null</param>
        /// <returns>the JSON string representation of the entity or null</returns>
        string Serialize(TEntity entity);

        /// <summary>
        /// Returns a reader of a JSON string representation of the entity.
        /// </summary>
        /// <param name="entity">the entity to serialize or null</param>
        /// <returns>the a reader of a JSON string representation of the entity or null</returns>
        TextReader SerializeToReader(TEntity entity);
    }
}