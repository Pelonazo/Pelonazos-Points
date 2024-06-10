// Archivo llamado commands.cs
using Rocket.API;
using Rocket.Unturned.Chat;
using Rocket.Unturned.Player;
using System.Collections.Generic;

namespace Pelonazos.Points
{
    public class CommandPoints : IRocketCommand
    {
        public AllowedCaller AllowedCaller => AllowedCaller.Player;
        public string Name => "points";
        public string Help => "Revisa tus puntos";
        public string Syntax => "";
        public List<string> Aliases => new List<string> { "pts" };
        public List<string> Permissions => new List<string> { "pelonazos.points" };

        public void Execute(IRocketPlayer caller, string[] command)
        {
            UnturnedPlayer player = (UnturnedPlayer)caller;

            int points = PointsDataManager.GetPlayerPoints(player.CSteamID.ToString());
            UnturnedChat.Say(player, $"Tienes {points} puntos.");
        }
    }

    public class CommandSetPoints : IRocketCommand
    {
        public AllowedCaller AllowedCaller => AllowedCaller.Both;
        public string Name => "setpoints";
        public string Help => "Establece los puntos de un jugador";
        public string Syntax => "<jugador> <puntos>";
        public List<string> Aliases => new List<string>();
        public List<string> Permissions => new List<string> { "pelonazos.setpoints" };

        public void Execute(IRocketPlayer caller, string[] command)
        {
            if (command.Length < 2)
            {
                UnturnedChat.Say(caller, "Uso: /setpoints <jugador> <puntos>");
                return;
            }

            string targetName = command[0];
            UnturnedPlayer targetPlayer = UnturnedPlayer.FromName(targetName);
            if (targetPlayer == null)
            {
                UnturnedChat.Say(caller, "Jugador no encontrado.");
                return;
            }

            if (!int.TryParse(command[1], out int points))
            {
                UnturnedChat.Say(caller, "Por favor, introduce un número válido para los puntos.");
                return;
            }

            string playerId = targetPlayer.CSteamID.ToString();
            PointsDataManager.SetPlayerPoints(playerId, points);

            UnturnedChat.Say(caller, $"Se han establecido {points} puntos para {targetPlayer.DisplayName}.");
        }
    }
    public class CommandCheckPoints : IRocketCommand
    {
        public AllowedCaller AllowedCaller => AllowedCaller.Both;
        public string Name => "checkpoints";
        public string Help => "Revisa los puntos de otro jugador";
        public string Syntax => "<jugador>";
        public List<string> Aliases => new List<string> { "chkpts" };
        public List<string> Permissions => new List<string> { "pelonazos.checkpoints" };

        public void Execute(IRocketPlayer caller, string[] command)
        {
            if (command.Length < 1)
            {
                UnturnedChat.Say(caller, "Uso: /checkpoints <jugador>");
                return;
            }

            string targetName = command[0];
            UnturnedPlayer targetPlayer = UnturnedPlayer.FromName(targetName);
            if (targetPlayer == null)
            {
                UnturnedChat.Say(caller, "Jugador no encontrado.");
                return;
            }

            int points = PointsDataManager.GetPlayerPoints(targetPlayer.CSteamID.ToString());
            UnturnedChat.Say(caller, $"{targetPlayer.DisplayName} tiene {points} puntos.");
        }
    }


}
