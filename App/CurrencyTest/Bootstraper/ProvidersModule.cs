using Autofac;
using CurrencyTest.Infrastructure.Interfaces;
using CurrencyTest.Providers;

namespace CurrencyTest.Bootstraper
{
    public class ProvidersModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterType<FixerApiProvider>()
                .As<ICurrencyApi>()
                .OnActivated(x => x.Instance.Initilize())
                .InstancePerDependency();

            base.Load(builder);
        }
    }
}
