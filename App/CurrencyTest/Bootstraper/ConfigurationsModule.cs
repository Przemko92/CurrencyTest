using Autofac;
using CurrencyTest.Configurations;
using CurrencyTest.Infrastructure.Interfaces;

namespace CurrencyTest.Bootstraper
{
    public class ConfigurationsModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterType<CurrenciesConfiguration>()
                .As<ICurrenciesConfiguration>()
                .SingleInstance();

            base.Load(builder);
        }
    }
}
