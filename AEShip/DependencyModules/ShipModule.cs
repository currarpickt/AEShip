using AEShip.Service.Interfaces;
using AEShip.Service.Services;
using Autofac;

namespace AEShip.DependencyModules
{
    public class ShipModule: Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<RepositoryService>()
                .As<IRepositoryService>()
                .InstancePerLifetimeScope();

            builder.RegisterType<ShipService>()
                .As<IShipService>()
                .InstancePerLifetimeScope();
        }
    }
}
