using Autofac;
using CurrencyTest.Infrastructure.Interfaces;
using CurrencyTest.Services;

namespace CurrencyTest.Bootstraper
{
    public class ServicesModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterType<CurrenciesService>()
                .As<ICurrenciesService>()
                .InstancePerDependency();
            
            base.Load(builder);
        }
    }
}
