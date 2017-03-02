using System;
using System.Collections.Generic;

namespace Bit.ActorLite.Ioc
{
    internal class SingletonCreationService
    {
        private static readonly Dictionary<string, object> objectPool = new Dictionary<string, object>();

        static SingletonCreationService()
        {

        }

        private SingletonCreationService()
        { }

        public static SingletonCreationService Instance { get; } = new SingletonCreationService();


        public object GetSingleton(Type t, object[] arguments = null)
        {
            object obj = null;

            try
            {
                if (objectPool.ContainsKey(t.Name) == false)
                {
                    obj = InstanceCreationService.Instance.GetNewObject(t, arguments);
                    objectPool.Add(t.Name, obj);
                }
                else
                {
                    obj = objectPool[t.Name];
                }
            }
            catch
            {
                // log it maybe
            }

            return obj;
        }
    }
}