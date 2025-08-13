using Zenject;

namespace Utilities
{
    public static class ZenjectUtility
    {
        public static void BindAndQueueForInject<T>(DiContainer container, T instance) where T : class
        {
            container.BindInstance(instance);
            container.QueueForInject(instance);
        }
        
        public static void BindAllAndQueueForInject<T>(DiContainer container, T instance) where T : class
        {
            container.BindInterfacesAndSelfTo<T>().FromInstance(instance);
            container.QueueForInject(instance);
        }
        
        public static void BindAllAndQueueForInject<TBase, TConcrete>(DiContainer container, TConcrete instance)
            where TConcrete : class, TBase
            where TBase : class
        {
            container.Bind<TBase>().FromInstance(instance).AsSingle();
            container.BindInterfacesAndSelfTo<TConcrete>().FromInstance(instance).AsSingle();
            container.QueueForInject(instance);
        }
    }
}