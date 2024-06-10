// Archivo llamado PointsPlugin.cs
using Rocket.API.Collections;
using Rocket.Core.Plugins;
using Rocket.Unturned.Events;
using Rocket.Unturned.Player;
using SDG.Unturned;
using Steamworks;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Pelonazos.Points
{
    public class PelonazoPoints : RocketPlugin<PointsPluginConfiguration>
    {
        public static PelonazoPoints Instance;

        protected override void Load()
        {
            Instance = this;
            PointsDataManager.Initialize();
            Rocket.Core.Logging.Logger.Log("=================================");
            Rocket.Core.Logging.Logger.Log($"{Name} {System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString(3)} has been loaded!");
            Rocket.Core.Logging.Logger.Log("Plugin desarrollado por Pelonazo");
            Rocket.Core.Logging.Logger.Log("=================================");
            UnturnedPlayerEvents.OnPlayerDeath += OnPlayerDeath;
            DamageTool.damagePlayerRequested += OnPlayerDamaged;
            VehicleManager.onDamageVehicleRequested += OnVehicleDamaged;
        }

        protected override void Unload()
        {
            Instance = null;
            Rocket.Core.Logging.Logger.Log($"{Name} has been unloaded!");
            UnturnedPlayerEvents.OnPlayerDeath -= OnPlayerDeath;
            DamageTool.damagePlayerRequested -= OnPlayerDamaged;
            VehicleManager.onDamageVehicleRequested -= OnVehicleDamaged;
        }

        private void DeductPoints(UnturnedPlayer player, int points, string message)
        {
            string playerId = player.CSteamID.ToString();
            int currentPoints = PointsDataManager.GetPlayerPoints(playerId);
            if (currentPoints >= points)
            {
                PointsDataManager.AddPoints(playerId, -points); // Restar puntos en lugar de sumarlos
                currentPoints -= points;
            }
            else
            {
                PointsDataManager.SetPlayerPoints(playerId, 0);
                currentPoints = 0;
            }

            message = string.Format(message, points, currentPoints);
            message += $" Total: {currentPoints} Pts.";
            ChatManager.serverSendMessage(
                message,
                Color.red,
                null,
                player.SteamPlayer(),
                EChatMode.GLOBAL,
                null,
                true
            );
        }

        private void OnPlayerDeath(UnturnedPlayer player, EDeathCause cause, ELimb limb, CSteamID instigator)
        {
            if (cause == EDeathCause.KILL)
            {
                var killer = UnturnedPlayer.FromCSteamID(instigator);
                if (killer != null && killer != player)
                {
                    int points = Configuration.Instance.KillPoints;
                    AddPoints(killer, points, $"Has ganado {points} puntos por matar a un jugador.");
                }
            }

            // Restar puntos cuando un jugador muere
            DeductPoints(player, Configuration.Instance.DeathPoints, $"Has perdido {Configuration.Instance.DeathPoints} puntos por morir.");
        }


        private void OnPlayerDamaged(ref DamagePlayerParameters parameters, ref bool shouldAllow)
        {
            var attacker = UnturnedPlayer.FromCSteamID(parameters.killer);
            var victim = UnturnedPlayer.FromPlayer(parameters.player);

            // Permitir el daño al jugador
            shouldAllow = true;

            if (attacker != null && victim != null && attacker != victim)
            {
                if (parameters.cause == EDeathCause.GUN && parameters.limb == ELimb.SKULL)
                {
                    AddPoints(attacker, Configuration.Instance.HeadshotPoints, $"Has ganado {Configuration.Instance.HeadshotPoints} puntos por un disparo a la cabeza.");
                }
                else if (parameters.damage >= victim.Health)
                {
                    AddPoints(attacker, Configuration.Instance.KillPoints, $"Has ganado {Configuration.Instance.KillPoints} puntos por matar a un jugador.");
                }
            }
        }




        private void OnVehicleDamaged(CSteamID instigatorSteamID, InteractableVehicle vehicle, ref ushort pendingTotalDamage, ref bool canRepair, ref bool shouldAllow, EDamageOrigin damageOrigin)
        {
            var player = UnturnedPlayer.FromCSteamID(instigatorSteamID);
            if (player != null)
            {
                if (pendingTotalDamage >= vehicle.health)
                {
                    AddPoints(player, Configuration.Instance.DestroyVehiclePoints, $"Has ganado {Configuration.Instance.DestroyVehiclePoints} puntos por destruir un vehículo.");
                }

                // Permitir el daño al vehículo
                shouldAllow = true;
            }
        }

        private void AddPoints(UnturnedPlayer player, int points, string message)
        {
            if (player == null)
            {
                Rocket.Core.Logging.Logger.LogWarning("Se intentó agregar puntos a un jugador nulo.");
                return;
            }

            string playerId = player.CSteamID.ToString();
            PointsDataManager.AddPoints(playerId, points);
            int totalPoints = PointsDataManager.GetPlayerPoints(playerId);
            message = $"{message} Total: {totalPoints} Pts.";
            ChatManager.serverSendMessage(
                message,
                Color.green,
                null,
                player.SteamPlayer(),
                EChatMode.GLOBAL,
                null,
                true
            );
        }


    }
}
