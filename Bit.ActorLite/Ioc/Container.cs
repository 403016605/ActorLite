using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Bit.ActorLite.Ioc
{
    public class Container : IContainer
    {
        private readonly Dictionary<Type, RegistrationModel> instanceRegistry = new Dictionary<Type, RegistrationModel>();

        public void RegisterInstanceType<I, C>()
            where I : class
            where C : class
        {
            RegisterType<I, C>(LifeStyle.Instance);
        }

        public void RegisterSingletonType<I, C>()
            where I : class
            where C : class
        {
            RegisterType<I, C>(LifeStyle.Singleton);
        }

        private void RegisterType<I, C>(LifeStyle type)
        {
            if (instanceRegistry.ContainsKey(typeof(I)))
            {
                instanceRegistry.Remove(typeof(I));
            }

            instanceRegistry.Add(
                typeof(I),
                new RegistrationModel
                {
                    RegType = type,
                    ObjectType = typeof(C)
                }
            );
        }

        public I Resolve<I>()
        {
            return (I)Resolve(typeof(I));
        }

        private object Resolve(Type t)
        {
            object obj;

            if (instanceRegistry.ContainsKey(t) != true) return null;

            var model = instanceRegistry[t];

            if (model == null) return null;

            var typeToCreate = model.ObjectType;

            var consInfo = typeToCreate.GetConstructors();

            var dependentCtor = consInfo.FirstOrDefault(item => item.CustomAttributes.FirstOrDefault(att => att.AttributeType == typeof(DependencyAttribute)) != null);

            if (dependentCtor == null)
            {
                // use the default constructor to create
                obj = CreateInstance(model);
            }
            else
            {
                // We found a constructor with dependency attribute
                ParameterInfo[] parameters = dependentCtor.GetParameters();
                obj = !parameters.Any() ? CreateInstance(model) : CreateInstance(model, parameters.Select(param => param.ParameterType).Select(this.Resolve).ToArray());
            }

            return obj;
        }

        private static object CreateInstance(RegistrationModel model, object[] arguments = null)
        {
            object returnedObj = null;
            var typeToCreate = model.ObjectType;

            if (model.RegType == LifeStyle.Instance)
            {
                returnedObj = InstanceCreationService.Instance.GetNewObject(typeToCreate, arguments);
            }
            else if (model.RegType == LifeStyle.Singleton)
            {
                returnedObj = SingletonCreationService.Instance.GetSingleton(typeToCreate, arguments);
            }

            return returnedObj;
        }
    }
}