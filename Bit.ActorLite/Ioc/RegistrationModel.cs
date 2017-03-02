using System;

namespace Bit.ActorLite.Ioc
{
    internal class RegistrationModel
    {
        internal Type ObjectType { get; set; }
        internal LifeStyle RegType { get; set; }
    }
}