using Autofac;
using BeatStore.API.Context;
using BeatStore.API.Interfaces.Repositories;
using BeatStore.API.Repositories;

namespace BeatStore.API.Modules
{
    public class RepositoryBuilder : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<TrackRepository>().As<ITrackRepository>().InstancePerLifetimeScope();
            builder.RegisterType<StockRepository>().As<IStockRepository>().InstancePerLifetimeScope();
            builder.RegisterType<OrderRepository>().As<IOrderRepository>().InstancePerLifetimeScope();
        }
    }
}
