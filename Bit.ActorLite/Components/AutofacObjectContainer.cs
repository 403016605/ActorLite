﻿using System;
using Autofac;

namespace Bit.ActorLite.Components
{
    /// <summary>Autofac implementation of IObjectContainer.
    /// </summary>
    public class AutofacObjectContainer : IObjectContainer
    {
        /// <summary>默认构造函数
        /// </summary>
        public AutofacObjectContainer()
        {
            Container = new ContainerBuilder().Build();
        }
        /// <summary>带参数的构造函数
        /// </summary>
        /// <param name="containerBuilder"></param>
        public AutofacObjectContainer(ContainerBuilder containerBuilder)
        {
            Container = containerBuilder.Build();
        }

        /// <summary>Represents the inner autofac container.
        /// </summary>
        public IContainer Container { get; }

        /// <summary>Register a implementation type.
        /// </summary>
        /// <param name="implementationType">The implementation type.</param>
        /// <param name="serviceName">The service name.</param>
        /// <param name="life">The life cycle of the implementer type.</param>
        public void RegisterType(Type implementationType, string serviceName = null, LifeStyle life = LifeStyle.Singleton)
        {
            var builder = new ContainerBuilder();
            var registrationBuilder = builder.RegisterType(implementationType);
            if (serviceName != null)
            {
                registrationBuilder.Named(serviceName, implementationType);
            }
            if (life == LifeStyle.Singleton)
            {
                registrationBuilder.SingleInstance();
            }
            builder.Update(Container);
        }
        /// <summary>Register a implementer type as a service implementation.
        /// </summary>
        /// <param name="serviceType">The service type.</param>
        /// <param name="implementationType">The implementation type.</param>
        /// <param name="serviceName">The service name.</param>
        /// <param name="life">The life cycle of the implementer type.</param>
        public void RegisterType(Type serviceType, Type implementationType, string serviceName = null, LifeStyle life = LifeStyle.Singleton)
        {
            var builder = new ContainerBuilder();
            var registrationBuilder = builder.RegisterType(implementationType).As(serviceType);
            if (serviceName != null)
            {
                registrationBuilder.Named(serviceName, serviceType);
            }
            if (life == LifeStyle.Singleton)
            {
                registrationBuilder.SingleInstance();
            }
            builder.Update(Container);
        }
        /// <summary>Register a implementer type as a service implementation.
        /// </summary>
        /// <typeparam name="TService">The service type.</typeparam>
        /// <typeparam name="TImplementer">The implementer type.</typeparam>
        /// <param name="serviceName">The service name.</param>
        /// <param name="life">The life cycle of the implementer type.</param>
        public void Register<TService, TImplementer>(string serviceName = null, LifeStyle life = LifeStyle.Singleton)
            where TService : class
            where TImplementer : class, TService
        {
            var builder = new ContainerBuilder();
            var registrationBuilder = builder.RegisterType<TImplementer>().As<TService>();
            if (serviceName != null)
            {
                registrationBuilder.Named<TService>(serviceName);
            }
            if (life == LifeStyle.Singleton)
            {
                registrationBuilder.SingleInstance();
            }
            builder.Update(Container);
        }
        /// <summary>Register a implementer type instance as a service implementation.
        /// </summary>
        /// <typeparam name="TService">The service type.</typeparam>
        /// <typeparam name="TImplementer">The implementer type.</typeparam>
        /// <param name="instance">The implementer type instance.</param>
        /// <param name="serviceName">The service name.</param>
        public void RegisterInstance<TService, TImplementer>(TImplementer instance, string serviceName = null)
            where TService : class
            where TImplementer : class, TService
        {
            var builder = new ContainerBuilder();
            var registrationBuilder = builder.RegisterInstance(instance).As<TService>().SingleInstance();
            if (serviceName != null)
            {
                registrationBuilder.Named<TService>(serviceName);
            }
            builder.Update(Container);
        }
        /// <summary>Resolve a service.
        /// </summary>
        /// <typeparam name="TService">The service type.</typeparam>
        /// <returns>The component instance that provides the service.</returns>
        public TService Resolve<TService>() where TService : class
        {
            return Container.Resolve<TService>();
        }
        /// <summary>Resolve a service.
        /// </summary>
        /// <param name="serviceType">The service type.</param>
        /// <returns>The component instance that provides the service.</returns>
        public object Resolve(Type serviceType)
        {
            return Container.Resolve(serviceType);
        }
        /// <summary>Try to retrieve a service from the container.
        /// </summary>
        /// <typeparam name="TService">The service type to resolve.</typeparam>
        /// <param name="instance">The resulting component instance providing the service, or default(TService).</param>
        /// <returns>True if a component providing the service is available.</returns>
        public bool TryResolve<TService>(out TService instance) where TService : class
        {
            return Container.TryResolve<TService>(out instance);
        }
        /// <summary>Try to retrieve a service from the container.
        /// </summary>
        /// <param name="serviceType">The service type to resolve.</param>
        /// <param name="instance">The resulting component instance providing the service, or null.</param>
        /// <returns>True if a component providing the service is available.</returns>
        public bool TryResolve(Type serviceType, out object instance)
        {
            return Container.TryResolve(serviceType, out instance);
        }
        /// <summary>Resolve a service.
        /// </summary>
        /// <typeparam name="TService">The service type.</typeparam>
        /// <param name="serviceName">The service name.</param>
        /// <returns>The component instance that provides the service.</returns>
        public TService ResolveNamed<TService>(string serviceName) where TService : class
        {
            return Container.ResolveNamed<TService>(serviceName);
        }
        /// <summary>Resolve a service.
        /// </summary>
        /// <param name="serviceName">The service name.</param>
        /// <param name="serviceType">The service type.</param>
        /// <returns>The component instance that provides the service.</returns>
        public object ResolveNamed(string serviceName, Type serviceType)
        {
            return Container.ResolveNamed(serviceName, serviceType);
        }
        /// <summary>Try to retrieve a service from the container.
        /// </summary>
        /// <param name="serviceName">The name of the service to resolve.</param>
        /// <param name="serviceType">The type of the service to resolve.</param>
        /// <param name="instance">The resulting component instance providing the service, or null.</param>
        /// <returns>True if a component providing the service is available.</returns>
        public bool TryResolveNamed(string serviceName, Type serviceType, out object instance)
        {
            return Container.TryResolveNamed(serviceName, serviceType, out instance);
        }
    }
}

