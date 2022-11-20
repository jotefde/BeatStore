using Autofac;
using BeatStore.API.UseCases.Tracks;

namespace BeatStore.API.Modules
{
    public class UseCaseBuilder : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ListAllTracksUseCase>().InstancePerLifetimeScope();
        }
    }
}
