using Autofac;
using BeatStore.API.UseCases.Orders;
using BeatStore.API.UseCases.Stock;
using BeatStore.API.UseCases.Tracks;

namespace BeatStore.API.Modules
{
    public class UseCaseBuilder : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // Track use cases
            builder.RegisterType<ListAllTrackUseCase>().InstancePerLifetimeScope();
            builder.RegisterType<GetTrackUseCase>().InstancePerLifetimeScope();
            builder.RegisterType<CreateTrackUseCase>().InstancePerLifetimeScope();

            // Stock use cases
            builder.RegisterType<ListAllStockUseCase>().InstancePerLifetimeScope();
            builder.RegisterType<GetStockUseCase>().InstancePerLifetimeScope();
            builder.RegisterType<CreateStockUseCase>().InstancePerLifetimeScope();
            builder.RegisterType<UpdateStockUseCase>().InstancePerLifetimeScope();

            // Order use cases
            builder.RegisterType<CreateOrderUseCase>().InstancePerLifetimeScope();
        }
    }
}
