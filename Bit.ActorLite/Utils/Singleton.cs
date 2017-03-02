namespace Bit.ActorLite.Utils
{
    /// <summary>
    /// 泛型单例模式
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class Singleton<T> where T : new()
    {
        public static T Instance => SingletonCreator.instance;

        private class SingletonCreator
        {
            internal static readonly T instance = new T();
        }
    }
}