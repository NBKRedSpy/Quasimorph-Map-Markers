# Quasimorph Map Markers
![marker example](media/Marker%20Example.png)


# Beta Note
If using the opt in beta, use the [beta docs](https://github.com/NBKRedSpy/Quasimorph-Map-Markers/blob/beta/README.md) instead.

# Description
Allows the user to add markers to the minimap.  For instance, mark the location of valuable loot to come back for later.

To use: 

When in the Mini Map:
* Right click to add or remove a location under the cursor.
* Press F2 to add or remove a marker at the player's location

When in inventory or when not in the minimap:
* Press F2 to add a marker at the player's location.  Note that this *only* adds markers to prevent accidentally removing a marker.

Hovering over a marker will show all the items at that location\*.

The settings can be configured.

\* The hover listing can be exploited to show anything on the map, but I leave that to the user's personal preferences on such matters :)

# Configuration

## MCM
This mod supports the Mod Configuration Menu. Some values can be set in the Mods menu, while others can only be changed in the config file.

## Config File
The configuration file will be created on the first game run and can be found at `%AppData%\..\LocalLow\Magnum Scriptum Ltd\Quasimorph_ModConfigs\MapMarkers\config.json`.

|Name|Default|Description|
|--|--|--|
|ClearLocationsKey|Delete|Deletes all of the map markers for the current level.|
|AddLocationKey|Right Click|The key or mouse button to add or remove a marker.|
|AddPlayerLocationKey|F2|Adds or removes a marker the the player's current location.|
|AddPlayerLocationOnDungeonKey|F2|Adds a marker to the player's current location not in the minimap.  This *only* adds|
|MarkerColor|Red|The color of the markers.|
|FontSize|5|The size of the text to use for the marker's content text.  Important!  See FontSize note below.|

### FontSize Note for MCM
Only use the slider in the Mod Config Menu.  The number cannot be typed in currently.  Additionally, pressing backspace closes the mod settings UI, which causes the config UI to become confused.

The number does not have to be perfect, so 5.02 vs 5.0 makes no real difference.

## Key List
The list of valid keyboard keys can be found  at the bottom of https://docs.unity3d.com/ScriptReference/KeyCode.html
Beware that numbers 0-9 are Alpha0 - Alpha9.  Most of the other keys are as expected such as X for X.
Use "None" to not bind the key.

# Support My Mods
If you enjoy my mods and want to buy me a coffee, check out my [Ko-Fi](https://ko-fi.com/nbkredspy71915) page.
It helps motivate me to maintain as well as create mods.

Thanks!  

# Issues
* Placing a marker at the merc's location can be hard to see due to the merc icon being on top.

# Change Log

## 1.5.0
* For game version 0.9.7+ only.
* Can now remove markers when not in the minimap.  Defaults to Shift+F2.
* All keybinds can be set in the Mod Config menu.  The user no longer needs to edit the config.json directly.
* Internal: Updated Json to MCM framework to latest iteration.

## 1.4.0
* For game version 0.9.6+ only.
* 0.9.6 compatibility

## 1.3.0
* Primary sort is now price.
* The count per item includes the stack count.  Ex: '9mm x120' instead of '9mm x2'

## 1.2.1
* Fixed the "not in minimap screen" mode did not place marker when storage was open.

## 1.2.0
* Added hotkey to add a marker when in inventory and not the mini map.
* Added game's "Click" sounds to the actions.
* Changed defaults of "Add Marker" to F2

## 1.1.0
* Added listing the items at a marker when hovered.

# Credits
* Special thanks to Crynano for his excellent Mod Configuration Menu. 
* [Treasure icons created by Smashicons - Flaticon](https://www.flaticon.com/free-icons/treasure)
* [Annotely](https://annotely.com/) for image annotation.

# Source Code
Source code is available on GitHub at https://github.com/NBKRedSpy/Quasimorph-Map-Markers
