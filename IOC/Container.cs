using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace IOC
{
    public class Container
    {
        /// <summary>
        /// A dictionary that allows us to keep track of
        /// registered types
        /// </summary>
        private Dictionary<Type, List<Type>> _typeDict;

        /// <summary>
        /// A disctionary to keep track of instances of types, if requested upon registration
        /// </summary>
        private Dictionary<Type, List<object>> _instanceDict;

        /// <summary>
        /// Constructor -> zero parameters
        /// </summary>
        public Container()
        {
            _typeDict = new Dictionary<Type, List<Type>>();
            _instanceDict = new Dictionary<Type, List<object>>();
        }

        /// <summary>
        /// Check whether we could resolve a given type
        /// Throws an exception if we cannot
        /// </summary>
        /// <param name="type"></param>
        private void CheckTypeResolution(Type type)
        {
            if(!_typeDict.ContainsKey(type))
            {
                throw new InvalidOperationException(string.Format("There is no registered type {0}", type.FullName));
            }
        }

        /// <summary>
        /// Register a type
        /// </summary>
        /// <typeparam name="T">T - the generic type used as a key (usually an interface)</typeparam>
        /// <param name="type">the type to be used as a value - return upon resolution</param>
        private void AddType<T>(Type type)
        {
            Type keyType = typeof(T);
            if (_typeDict.ContainsKey(keyType))
            {
                if(!_typeDict[keyType].Contains(type))
                    _typeDict[keyType].Add(type);
            }
            else
            {
                _typeDict.Add(keyType, new List<Type>() { type });
            }
        }

        /// <summary>
        /// Add an instance 
        /// </summary>
        /// <typeparam name="T">The generic type used as a key</typeparam>
        /// <param name="instance">instance of the type</param>
        private void AddInstance<T>(object instance)
        {
            Type keyType = typeof(T);
            if (_typeDict.ContainsKey(keyType))
            {
                if (_instanceDict[keyType] == null)
                    _instanceDict[keyType] = new List<object>();

                if(!_instanceDict[keyType].Contains(instance))
                    _instanceDict[keyType].Add(instance);
            }
            else
            {
                
                _instanceDict.Add(keyType, new List<object>() { instance });
            }
        }

        /// <summary>
        /// Resolve a type and all of its dependencies recursively
        /// We instantiate the type using the constructor that has 
        /// the largest number of parameters
        /// </summary>
        /// <param name="type"></param>
        /// <returns>an instance of the type</returns>
        private object Resolve(Type type)
        {
            CheckTypeResolution(type);

            List<Type> resolvedType = _typeDict[type];
            return ResolveType(resolvedType.First());
        }

        /// <summary>
        /// Returned all the types registered against an interface
        /// </summary>
        /// <param name="type">The type used as a key</param>
        /// <returns></returns>
        private List<object> ResolveList(Type type)
        {
            List<Type> resolvedTypes = _typeDict[type];

            List<object> listInstances = new List<object>();
            foreach(Type t in resolvedTypes)
            {
                listInstances.Add(ResolveType(t));
            }

            return listInstances;
        }

        /// <summary>
        /// Resolving a type -> creating an instance by
        /// resolving all dependencies
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private object ResolveType(Type type)
        {
            ConstructorInfo constructor = GetConstructorMaxParams(type);
            ParameterInfo[] constructorParams = null;

            if (constructor != null)
                constructorParams = constructor.GetParameters();

            //if we do not have parameters or constructors, instantiation is easy
            if (constructorParams == null || constructorParams.Count() == 0)
            {
                return Activator.CreateInstance(type);
            }
            else
            {
                //go through each parameter type and resolve it
                object[] paramInstances = new object[constructorParams.Length];
                Type parameterType = null;
                for (int i = 0; i < constructorParams.Length; i++)
                {
                    parameterType = constructorParams[i].ParameterType;
                    paramInstances[i] = Resolve(parameterType.IsByRef ? parameterType.GetElementType() : parameterType);
                }

                return constructor.Invoke(paramInstances);
            }
        }

        /// <summary>
        /// Find the constructor with the greatest number of parameters for a given type
        /// </summary>
        /// <param name="type"></param>
        /// <returns>array of ConstructorInfo</returns>
        private ConstructorInfo GetConstructorMaxParams(Type type)
        {
            int maxNumParams = 0;
            int numParams = 0;
            int indexConstructor = 0;

            ConstructorInfo[] constructors = type.GetConstructors();

            if (constructors.Length == 0)
                return null;

            for (int i = 0; i < constructors.Length; i++)
            {
                numParams = constructors[i].GetParameters().Length;
                if (numParams > maxNumParams)
                {
                    maxNumParams = numParams;
                    indexConstructor = i;
                }
            }

            return constructors[indexConstructor];
        }

        /// <summary>
        /// Wrapper public method for resolve taking a generic parameter
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>instance of the type T</returns>
        public T Resolve<T>()
        {
            CheckTypeResolution(typeof(T));

            Type keyType = typeof(T);
            if (_instanceDict.ContainsKey(keyType))
            {
                AddInstance<T>(Resolve(keyType));
                return (T)_instanceDict[keyType].First();
            }
            else
            {
                return (T)Resolve(keyType);
            }
        }

        /// <summary>
        /// Resolve the list of types registered against an interface
        /// </summary>
        /// <typeparam name="T">the interface type</typeparam>
        /// <returns>a list of registered types</returns>
        public List<T> ResolveList<T>()
        {
            CheckTypeResolution(typeof(T));

            Type keyType = typeof(T);
            if(_instanceDict.ContainsKey(keyType))
            {
                return _instanceDict[keyType] as List<T>;
            }
            else
            {
                return ResolveList(typeof(T)).Cast<T>().ToList();
            }
        }

        /// <summary>
        /// Add a type to the dictionary
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public void RegisterType<T>(bool isSingleton = false)
        {
            AddType<T>(typeof(T));

            //add the type to the instance dictionary as a key without a value
            //instantiate the value upon resolution
            if (isSingleton)
            {
                AddInstance<T>(null);
            }
        }

        /// <summary>
        /// Register a pair, usually and interface and an implementation
        /// </summary>
        /// <typeparam name="I">Interface Type</typeparam>
        /// <typeparam name="T">Implementation Type</typeparam>
        public void RegisterType<I, T>(bool isSingleton = false)
        {
            AddType<I>(typeof(T));

            if (isSingleton)
            {
                AddInstance<T>(null);
            }
        }

        public void RegisterInstance<T>(object instance)
        {
            AddInstance<T>(instance);
        }
    }
}
