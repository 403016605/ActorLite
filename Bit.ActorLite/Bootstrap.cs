using System;
using System.Linq;
using System.Reflection;
using Bit.ActorLite.Components;
using Bit.ActorLite.Extensions;

namespace Bit.ActorLite
{
    public static class Bootstrap
    {
        public static void Start()
        {
            ObjectContainer.SetContainer(new AutofacObjectContainer());
            ObjectContainer.Register<IBus, Bus>();


            var bus = ObjectContainer.Resolve<IBus>();

            var registerMethodInfo = bus.GetType()
                .GetMethods(BindingFlags.Instance | BindingFlags.Public)
                .Where(mi => mi.Name == "Register")
                .Where(mi => mi.IsGenericMethod)
                .Where(mi => mi.GetGenericArguments().Count() == 1)
                .Single(mi => mi.GetParameters().Count() == 1);

            #region 自动向Bus中注册事件处理器
            //获取程序集
            var assemblies = Assembly.GetEntryAssembly().GetReferencedAssemblies().ToList();
            assemblies.Add(Assembly.GetEntryAssembly().GetName());

            foreach (var asm in assemblies)
            {
                var HandlerMaps = Assembly.Load(asm).GetTypes()
                    .Select(type => new {HandlerType = type, Interfaces = type.GetImplementationType(typeof(IHandler<>)).ToList()})
                    .Where(type => type.Interfaces!=null && type.Interfaces.Any()).ToList();

                foreach (var HandlerMap in HandlerMaps)
                {
                    ObjectContainer.RegisterType(HandlerMap.HandlerType, null, LifeStyle.Transient);

                    foreach (var @interface in HandlerMap.Interfaces)
                    {
                        var commandType = @interface.GetGenericArguments()[0];

                        var proceed = new Action<dynamic>(x =>
                        {
                            dynamic handler = ObjectContainer.Resolve(HandlerMap.HandlerType);
                            handler.Handle(x);
                        });

                        registerMethodInfo.MakeGenericMethod(commandType).Invoke(bus, new object[] { proceed });
                    }
                }
            }
            #endregion

        }
    }
}
