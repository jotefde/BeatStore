using Autofac;
using BeatStore.API.Context;
using BeatStore.API.Factories;
using BeatStore.API.Interfaces.Factories;
using BeatStore.API.Interfaces.Repositories;
using BeatStore.API.Repositories;

namespace BeatStore.API.Modules
{
    public class ServiceBuilder : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<JwtFactory>().As<IJwtFactory>().SingleInstance();
            builder.RegisterType<MinioObjectStorageFactory>().As<IObjectStorageFactory>().SingleInstance();
        }
    }
}
