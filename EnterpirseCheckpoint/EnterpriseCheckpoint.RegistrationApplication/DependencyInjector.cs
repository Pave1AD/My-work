using Autofac;

namespace EnterpriseCheckpoint.RegistrationApplication
{
    public static class DependencyInjector
    {
        public static void Load(ContainerBuilder serviceCollection)
        {
            Services.DependencyInjector.Load(serviceCollection);
            DataAccess.DependencyInjector.Load(serviceCollection);
            EnterpirseCheckpoint.Utilities.DependencyInjector.Load(serviceCollection);
        }
    }
}
