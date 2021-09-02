using System.Linq;
using AEShip.Service.Models;
using NetTopologySuite;
using NetTopologySuite.Geometries;

namespace AEShip.Service
{
    public static class DBInitializer
    {
        public static void Initialize(RepositoryContext context)
        {
            context.Database.EnsureCreated();

            if(context.Ports.Any())
            {
                return;
            }

            var geometryFactory = NtsGeometryServices.Instance.CreateGeometryFactory(srid: 4326);

            var ports = new []
            {
                new Port {Id="P01", Name= "Port_A", Location = geometryFactory.CreatePoint(new Coordinate(26.87640, 83.09908)) },
                new Port {Id="P02", Name= "Port_B", Location = geometryFactory.CreatePoint(new Coordinate(22.34455, -99.17508)) },
                new Port {Id="P03", Name= "Port_C", Location = geometryFactory.CreatePoint(new Coordinate(-49.75010, -54.88544)) },
                new Port {Id="P04", Name= "Port_D", Location = geometryFactory.CreatePoint(new Coordinate(-48.79440, 0.07392)) },
                new Port {Id="P05", Name= "Port_E", Location = geometryFactory.CreatePoint(new Coordinate(13.12538, -108.22851)) },
                new Port {Id="P06", Name= "Port_F", Location = geometryFactory.CreatePoint(new Coordinate(2.29585, -141.04711)) },
                new Port {Id="P07", Name= "Port_G", Location = geometryFactory.CreatePoint(new Coordinate(-12.74879, 116.28070)) },
                new Port {Id="P08", Name= "Port_H", Location = geometryFactory.CreatePoint(new Coordinate(67.04796, 146.61130)) },
                new Port {Id="P09", Name= "Port_I", Location = geometryFactory.CreatePoint(new Coordinate(18.73603, 179.21967)) },
                new Port {Id="P010", Name= "Port_J", Location = geometryFactory.CreatePoint(new Coordinate(50.61675, -121.91904)) }
            };

            foreach (var p in ports)
            {
                context.Ports.Add(p);
            }
            context.SaveChanges();
        }
    }
}
