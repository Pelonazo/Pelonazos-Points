# Pelonazo's Points Plugin

## Overview
Pelonazo's Points Plugin is a plugin for the game Unturned that adds a points system to reward players for various actions such as killing other players, destroying vehicles, and more. Players can earn points for these actions and see their total points displayed in-game.

## Features
- Allows players to earn points for killing other players, destroying vehicles, and headshots.
- Points are displayed in-game to players.
- Configuration options for point values and messages.
- Commands to check total points, set points for a player, and check points for another player.

## Commands
- `/points` (alias: `/pts`) - Displays the player's total points.
- `/setpoints` - Sets the points for a specific player. Requires `pelonazos.setpoints` permission.
  - Usage: `/setpoints <player> <points>`
- `/checkpoints` (alias: `/chkpts`) - Checks the points for another player. Requires `pelonazos.checkpoints` permission.
  - Usage: `/checkpoints <player>`

## Installation
1. Download the latest release from the [GitHub repository]([https://github.com/your/repository](https://github.com/Pelonazo/Pelonazos-Points)).
2. Extract the contents of the ZIP file into your Unturned server's `Rocket/Plugins` folder.
3. Restart your Unturned server.

## Configuration
The plugin's configuration file (`PelonazosPoints.configuration.xml`) allows you to customize various aspects of the plugin, including point values and messages.

## Usage
Once installed, the plugin will automatically start awarding points to players for the specified actions. Players can use the `/points` command to check their total points, and admins can use the `/setpoints` command to set a player's points or the `/checkpoints` command to check another player's points.

## Credits
- Plugin developed by Pelonazo.
