using System.Linq;
using AEShip.Service.Models;

namespace AEShip.Service
{
    public static class DbInitializer
    {
        public static void Initialize(RepositoryContext context)
        {
            context.Database.EnsureCreated();

            if(context.Ports.Any())
            {
                return;
            }

            var ports = new []
            {
                new Port {Id="P01", Name= "Port_A", Longitude = 83.09908, Latitude = 26.87640 },
                new Port {Id="P02", Name= "Port_B", Longitude = -99.17508, Latitude = 22.34455 },
                new Port {Id="P03", Name= "Port_C", Longitude = -54.88544, Latitude = -49.75010 },
                new Port {Id="P04", Name= "Port_D", Longitude = 0.07392, Latitude = -48.79440 },
                new Port {Id="P05", Name= "Port_E", Longitude = -108.22851, Latitude = 13.12538 },
                new Port {Id="P06", Name= "Port_F", Longitude = -141.04711, Latitude = 2.29585 },
                new Port {Id="P07", Name= "Port_G", Longitude = 116.28070, Latitude = -12.74879 },
                new Port {Id="P08", Name= "Port_H", Longitude = 146.61130, Latitude = 67.04796 },
                new Port {Id="P09", Name= "Port_I", Longitude = 179.21967, Latitude = 18.73603 },
                new Port {Id="P010", Name= "Port_J", Longitude = -121.91904, Latitude = 50.61675 }
            };

            foreach (var p in ports)
            {
                context.Ports.Add(p);
            }
            context.SaveChanges();
        }
    }
}
