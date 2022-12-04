using Autofac;
using BeatStore.API.Context;
using BeatStore.API.Services;
using BeatStore.API.Interfaces.Services;
using BeatStore.API.Interfaces.Repositories;
using BeatStore.API.Repositories;

namespace BeatStore.API.Modules
{
    public class ServiceBuilder : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<JwtService>().As<IJwtService>().SingleInstance();
            builder.RegisterType<MinioObjectStorage>().As<IObjectStorageService>().SingleInstance();
        }
    }
}
