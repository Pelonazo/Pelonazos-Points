// Archivo llamado PointsDataManager.cs
using Newtonsoft.Json;
using SDG.Unturned;
using System.Collections.Generic;
using System.IO;

namespace Pelonazos.Points
{
    public static class PointsDataManager
    {
        private static readonly string dataFolderPath = Path.Combine(Rocket.Core.Environment.PluginsDirectory, "PelonazosPoints");
        private static readonly string dataFilePath = Path.Combine(dataFolderPath, "PelonazosPoints.json");

        public static void Initialize()
        {
            if (!Directory.Exists(dataFolderPath))
            {
                Directory.CreateDirectory(dataFolderPath);
            }

            if (File.Exists(dataFilePath))
            {
                // Cargar datos de puntos
                var playerPoints = LoadPlayerPoints();
                foreach (var player in Provider.clients)
                {
                    var playerId = player.playerID.steamID.ToString();
                    if (!playerPoints.ContainsKey(playerId))
                    {
                        playerPoints[playerId] = 0; // Asignar 0 puntos a los nuevos jugadores
                    }
                }
                SavePlayerPoints(playerPoints);
            }
            else
            {
                // Crear nuevo archivo de puntos
                Dictionary<string, int> playerPoints = new Dictionary<string, int>();
                foreach (var player in Provider.clients)
                {
                    playerPoints[player.playerID.steamID.ToString()] = 0; // Asignar 0 puntos a los nuevos jugadores
                }
                SavePlayerPoints(playerPoints);
            }
        }

        public static void SavePlayerPoints(Dictionary<string, int> playerPoints)
        {
            File.WriteAllText(dataFilePath, JsonConvert.SerializeObject(playerPoints));
        }

        public static Dictionary<string, int> LoadPlayerPoints()
        {
            if (File.Exists(dataFilePath))
            {
                return JsonConvert.DeserializeObject<Dictionary<string, int>>(File.ReadAllText(dataFilePath));
            }
            return new Dictionary<string, int>();
        }

        public static void SetPlayerPoints(string playerId, int points)
        {
            var playerPoints = LoadPlayerPoints();
            playerPoints[playerId] = points;
            SavePlayerPoints(playerPoints);
        }

        public static int GetPlayerPoints(string playerId)
        {
            var playerPoints = LoadPlayerPoints();
            return playerPoints.ContainsKey(playerId) ? playerPoints[playerId] : 0;
        }

        public static void AddPoints(string playerId, int points)
        {
            var currentPoints = GetPlayerPoints(playerId);
            SetPlayerPoints(playerId, currentPoints + points);
        }
    }
}
