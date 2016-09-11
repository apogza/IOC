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
        private Dictionary<Type, Type> _typeDict;

        /// <summary>
        /// A disctionary to keep track of instances of types, if requested upon registration
        /// </summary>
        private Dictionary<Type, object> _instanceDict;

        /// <summary>
        /// Constructor -> zero parameters
        /// </summary>
        public Container()
        {
            _typeDict = new Dictionary<Type, Type>();
            _instanceDict = new Dictionary<Type, object>();
        }

        /// <summary>
        /// A private method to check whether we have a registration for a type
        /// Throws an exception of the type is not found
        /// </summary>
        /// <param name="type"></param>
        private void CheckTypeRegistration(Type type)
        {
            if (_typeDict.ContainsKey(type))
            {
                throw new InvalidOperationException(string.Format("There is already a registered type {0}", type.FullName));
            }
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
        /// Resolve a type and all of its dependencies recursively
        /// We instantiate the type using the constructor that has 
        /// the largest number of parameters
        /// </summary>
        /// <param name="type"></param>
        /// <returns>an instance of the type</returns>
        private object Resolve(Type type)
        {
            CheckTypeResolution(type);

            Type resolvedType = _typeDict[type];

            ConstructorInfo constructor = GetConstructorMaxParams(type);
            ParameterInfo[] constructorParams = null;

            if (constructor != null)
                constructorParams = constructor.GetParameters();

            //if we do not have parameters or constructors, instantiation is easy
            if(constructorParams == null || constructorParams.Count() == 0)
            {
                return Activator.CreateInstance(resolvedType);
            }
            else
            {
                //go through each parameter type and resolve it
                object[] paramInstances = new object[constructorParams.Length];
                for(int i = 0; i < constructorParams.Length; i++)
                {
                    paramInstances[i] = Resolve(constructorParams[i].GetType());
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
            Type keyType = typeof(T);
            if (_instanceDict.ContainsKey(keyType))
            {
                if (_instanceDict[keyType] == null)
                    _instanceDict[keyType] = Resolve(keyType);

                return (T)_instanceDict[keyType];
            }
            else
            {
                return (T)Resolve(keyType);
            }
        }

        /// <summary>
        /// Add a type to the dictionary
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public void RegisterType<T>(bool isSingleton = false)
        {
            CheckTypeRegistration(typeof(T));
            _typeDict.Add(typeof(T), typeof(T));

            //add the type to the instance dictionary as a key without a value
            //instantiate the value upon resolution
            if(isSingleton)
                _instanceDict.Add(typeof(T), null);
        }

        /// <summary>
        /// Register a pair, usually and interface and an implementation
        /// </summary>
        /// <typeparam name="I">Interface Type</typeparam>
        /// <typeparam name="T">Implementation Type</typeparam>
        public void RegisterType<I, T>(bool isSingleton = false)
        {
            CheckTypeRegistration(typeof(I));
            _typeDict.Add(typeof(I), typeof(T));

            if (isSingleton)
                _instanceDict.Add(typeof(I), null);
        }
    }
}
