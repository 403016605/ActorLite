using System;

namespace Bit.ActorLite.Ioc
{
    internal class InstanceCreationService
    {
        static InstanceCreationService()
        {

        }

        private InstanceCreationService()
        { }

        public static InstanceCreationService Instance { get; } = new InstanceCreationService();

        public object GetNewObject(Type t, object[] arguments = null)
        {
            object obj = null;

            try
            {
                obj = Activator.CreateInstance(t, arguments);
            }
            catch
            {
                // log it maybe
            }

            return obj;
        }
    }
}