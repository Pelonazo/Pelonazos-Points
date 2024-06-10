// Archivo llamado PointsModels.cs
namespace Pelonazos.Points.Models
{
    public class PointsCommand
    {
        public string CommandName { get; set; }
        public string Message { get; set; }
        public string Image { get; set; }
        public string ChatMode { get; set; }
        public bool IsAnonymous { get; set; }
    }

    public class PlayerData
    {
        public string SteamId { get; set; }
        public int Points { get; set; }
    }
}

