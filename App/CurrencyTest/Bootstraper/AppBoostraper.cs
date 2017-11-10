using System.Reflection;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

namespace CurrencyTest.Bootstraper
{
    public class AppBoostraper
    {
        private IContainer _container;

        public AppBoostraper()
        {
        }

        public T Resolve<T>()
        {
            return _container.Resolve<T>();
        }

        public void Init(IServiceCollection services)
        {
            var containerBuilder = new ContainerBuilder();

            containerBuilder
                .RegisterAssemblyModules(Assembly.GetEntryAssembly());

            containerBuilder
                .Populate(services);
            
            _container = containerBuilder.Build();
        }
    }
}
