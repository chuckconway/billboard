using System;
using System.Collections;
using System.Collections.Generic;
using StructureMap;
using StructureMap.Configuration.DSL;

namespace Billboard.Infrastructure
{
    public class StructureMapInjection : IInjection
    {

        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T Get<T>()
        {
            return ObjectFactory.GetInstance<T>();
        }
        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public object Get(Type type)
        {
            return ObjectFactory.GetInstance(type);
        }

        /// <summary>
        /// Gets the instances.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>IList{``0}.</returns>
        public IList<T> GetInstances<T>()
        {
            return ObjectFactory.GetAllInstances<T>();
        }

        /// <summary>
        /// Gets the instances.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>IList.</returns>
        public IList GetInstances(Type type)
        {
            return ObjectFactory.GetAllInstances(type);
        }

        /// <summary>
        /// Adds the manifest.
        /// </summary>
        /// <param name="manifest">The manifest.</param>
        public void AddManifest(object manifest)
        {
            ObjectFactory.Configure(c => c.AddRegistry((Registry)manifest));
        }
    }
}