namespace Bit.ActorLite.Components
{
    /// <summary>描述组件的生命周期
    /// </summary>
    public enum LifeStyle
    {
        /// <summary>
        /// 表示一个临时组件.
        /// </summary>
        Transient,
        /// <summary>
        /// 表示一个单例组件.
        /// </summary>
        Singleton
    }
}