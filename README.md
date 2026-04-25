# CouinterStrikeAddons

Some addons for Counter Strike 2 using CounterStrikeSharp 
Used stuff:
<https://docs.cssharp.dev/>
<https://github.com/roflmuffin/CounterStrikeSharp>
<https://www.metamodsource.net/downloads.php/?branch=master>

# requirements:  
- **CounterStrike2** game server.
- **counterstrikesharp.api v1.0.362** or later.

# installation:  
Extract the folder to the `...\csgo\addons\counterstrikesharp\plugins\GameStatistic\` directory of the dedicated server.


# AdminMenu
Implementation of a AdminMenu plugin

This plugin allowes to the admins:
- ban (with time or permanent)
- kick
- kill
- slap
- respawn
- mute/unmuate (manually or automatic after death, configurable)
- rename any player (write the new name to the chat adfter select this menu item)
- set team for a player (and respawn if you need)
- respawn
- auto rename if player name already exist (Configurable)
- drop weapon
- change map (RockTheVote addon or its maplist.txt file needed)
- team shuffle (Configurable. GameStatistic addon stat file needed. See addon here: https://github.com/gaborszolner/GameStatistic)
- bot add, kick
- set admin with level
- weapon (un)restrict - current map or all maps
- welcome or ban message after connect (configurable)

There are 3 level for admins, lower can't use action on higher admins.

---
# installation:  
Extract the folder to the `...\csgo\addons\counterstrikesharp\plugins\AdminMenu\` directory of the dedicated server.
- Uses ..\csgo\addons\counterstrikesharp\configs\admins.json and ..\csgo\addons\counterstrikesharp\configs\banned.json files, these saves the admin and banned players. See example files in the solution.
- For changemap it uses the maplist.txt file, from the RockTheVote addon (https://github.com/abnerfs/cs2-rockthevote)

////////////////////////////////////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////////////////////////


# GameStatistic
Implementation of a GameStatistic plugin
  
This CS2 addon generates statistics for players and maps, but only if there are at least 4 players.

Player statistics:
  - It tracks kills, deaths, teamkills, selfkills, and assists.
  - It does not count events during warmup or after the round ends.
  - Based on these, it creates a ranking (which the adminMenu can use to mix teams fairly by dividing players into two teams).
  - Usable commands: !top and !mystat.

Map statistics:
  - It tracks how many times a map has started and how many times it was completed. From this, it can also calculate the RTV percentage.
  - It tracks how many times the CT and T sides have won on the given map.
  - Usable commands: !mapstat.

////////////////////////////////////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////////////////////////


# MenuHotKey
  
Implementation of a MenuHotKey plugin
  
This plugin allowes to choose menu items without type to console !1, !2, !3 ...
- you have to bind buttons: for example, if you want to bind menu option 3 to Numpad 3, type bind kp_3 "3" into the console.
- for official key mapping see: https://steamcommunity.com/sharedfiles/filedetails/?id=2498088800

////////////////////////////////////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////////////////////////


# QuickDefuse 
  
Implementation of a QuickDefuse Defuse plugin for CS2 using CounterStrikeSharp  
<https://docs.cssharp.dev/>  
  
This plugin allowes defuse the bomb by cut wires (1 - green, 2- yellow, 3 - red, 4 - blue, 5 - random)
- if cut right wire, the bomb will be defused immediately.
- if cut wrong wire, the bomb will explode
- until a wire is chosen after planting, the wire color will be random.
- you can choose the color in chat by typing, for example, !1.
- It is STRONGLY recommended to use the MenuHotkey plugin from my repository. With it, you can instantly select a wire in the pop-up menu. (and it works with every menu)

////////////////////////////////////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////////////////////////


# CounterStrike2 Map Switch Plugin

This plugin is designed for **CounterStrike2** to automatically switch to a specified map after the server starts. The map is selected from a `startMap.txt` file, which contains the map name and map ID. The plugin waits for 3 seconds after the server starts and then switches to the map specified in the `startMap.txt` file.

## Features

- **Automatic Map Switch**: After the server starts, the plugin reads the `startMap.txt` file and switches to the map defined within it.
- **Configurable File**: The `startMap.txt` file contains a list of map names and their corresponding map IDs, formatted as `mapname:mapid`.
- **Delay**: The plugin waits for 3 seconds after the server starts before switching the map.

## Requirements

- A `startMap.txt` file located in the appropriate directory (typically the root directory of your server).

## Installation

1. Download the plugin files.
2. Place the plugin in the server's plugin directory.
3. Ensure the `startMap.txt` file is in the correct location, containing the map name and ID pairs in the format `mapname:mapid`. For example: de_dolls:3501880673
4. Restart the server or load the plugin as per your server configuration.

## How It Works

1. When the server starts, the plugin waits for 3 seconds.
2. It then reads the `startMap.txt` file to retrieve the map name and ID.
3. The plugin switches to the specified map
