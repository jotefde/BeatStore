using Autofac;
using BeatStore.API.UseCases.Accounts;
using BeatStore.API.UseCases.Auth;
using BeatStore.API.UseCases.Orders;
using BeatStore.API.UseCases.Stock;
using BeatStore.API.UseCases.Tracks;
using BeatStore.API.UseCases.TrackStorage;

namespace BeatStore.API.Modules
{
    public class UseCaseBuilder : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // Auth use cases
            builder.RegisterType<CreateTokenUseCase>().InstancePerLifetimeScope();

            // Account use cases
            builder.RegisterType<CreateUserUseCase>().InstancePerLifetimeScope();

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
            builder.RegisterType<UpdateNotificationUseCase>().InstancePerLifetimeScope();
            builder.RegisterType<SendOwnedProductsLinkUseCase>().InstancePerLifetimeScope();

            // Track storage use cases
            builder.RegisterType<CreateTrackObjectsUseCase>().InstancePerLifetimeScope();
        }
    }
}
