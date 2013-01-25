using System;
using System.Collections;
using System.Collections.Generic;

namespace Billboard.Infrastructure
{
    public interface IInjection
    {
        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        T Get<T>();

        /// <summary>
        /// Gets the specified type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>System.Object.</returns>
        object Get(Type type);

        /// <summary>
        /// Gets the instances.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>IList.</returns>
        IList GetInstances(Type type);

        /// <summary>
        /// Gets the instances.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>IList{``0}.</returns>
        IList<T> GetInstances<T>();

        /// <summary>
        /// Adds the manifest.
        /// </summary>
        /// <param name="manifest">The manifest.</param>
        void AddManifest(object manifest);

    }
}
