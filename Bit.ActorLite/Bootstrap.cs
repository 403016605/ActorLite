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
            //设置IOC容器 
            ObjectContainer.SetContainer(new AutofacObjectContainer());

            //注册事件总线
            ObjectContainer.Register<IBus, Bus>();

            //自动注册message,handler到容器中,同时在Bus总线中绑定对应的消息处理器
            ScanAssemblies();
        }

        private static void ScanAssemblies()
        {
            //获取事件总线
            var bus = ObjectContainer.Resolve<IBus>();

            //获取总线的注册函数(Register)
            var registerMethodInfo = bus.GetType()
                .GetMethods(BindingFlags.Instance | BindingFlags.Public)
                .Where(mi => mi.Name == "Register")
                .Where(mi => mi.IsGenericMethod)
                .Where(mi => mi.GetGenericArguments().Count() == 1)
                .Single(mi => mi.GetParameters().Count() == 1);

            #region 获取程序集

            var assemblies = AppDomain.CurrentDomain.GetAssemblies().Where(asm=>!asm.FullName.StartsWith("System.", StringComparison.OrdinalIgnoreCase)).ToList();

            #endregion

            #region 自动向Bus中注册事件处理器

            foreach (var asm in assemblies)
            {
                var HandlerMaps = asm.GetTypes()
                    .Select(type => new { HandlerType = type, Interfaces = type.GetImplementationType(typeof(IHandler<>)).ToList() })
                    .Where(type => type.Interfaces != null && type.Interfaces.Any()).ToList();

                foreach (var HandlerMap in HandlerMaps)
                {
                    //注册Handler
                    ObjectContainer.RegisterType(HandlerMap.HandlerType, null, LifeStyle.Transient);

                    //获取Handler.Handle方法
                    var proceed = new Action<dynamic>(x =>
                    {
                        dynamic handler = ObjectContainer.Resolve(HandlerMap.HandlerType);
                        handler.Handle(x);
                    });

                    //Bus总线注册消息处理器
                    foreach (var @interface in HandlerMap.Interfaces)
                    {
                        var commandType = @interface.GetGenericArguments()[0];

                        registerMethodInfo.MakeGenericMethod(commandType).Invoke(bus, new object[] { proceed });
                    }
                }
            }

            #endregion
        }
    }
}
