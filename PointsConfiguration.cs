// Archivo llamado PointsConfiguration.cs
using Rocket.API;

namespace Pelonazos.Points
{
    public class PointsPluginConfiguration : IRocketPluginConfiguration
    {
        public int KillPoints { get; set; }
        public int HeadshotPoints { get; set; }
        public int DestroyVehiclePoints { get; set; }
        public string KillMessage { get; set; }
        public string HeadshotMessage { get; set; }
        public string DestroyVehicleMessage { get; set; }
        public int DeathPoints { get; set; }

        public void LoadDefaults()
        {
            KillPoints = 10;
            HeadshotPoints = 15;
            DestroyVehiclePoints = 20;
            DeathPoints = 5; // Puntos a restar cuando un jugador muere
        }
    }
}
