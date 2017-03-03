using System;

namespace Bit.ActorLite.Components
{
    /// <summary>
    /// 标记类的组件特性.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class ComponentAttribute : Attribute
    {
        /// <summary>
        /// 组件的生命周期.
        /// </summary>
        public LifeStyle LifeStyle { get; private set; }

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public ComponentAttribute() : this(LifeStyle.Singleton) { }

        /// <summary>带参数的构造函数
        /// </summary>
        /// <param name="lifeStyle"></param>
        public ComponentAttribute(LifeStyle lifeStyle)
        {
            LifeStyle = lifeStyle;
        }
    }
}
