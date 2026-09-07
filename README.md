# Quasimorph Map Markers
![marker example](media/Marker%20Example.png)

# Overview
Ever been in a mission and wanted to come back for something later?  But then forget where the heck it was?  

Do you have the floor scanner and are tired of having to hunt down the ten storage items that aren't empty out of hundred on the level?

This mod adds the ability to add markers to the minimap, as well as show the search status of storage items.

Hold alt to show only the storage items that have not been searched yet.  Optionally, this mode can ignore barrels.  Must be enabled in the settings.

Colors, hotkeys, and other options can be changed via the Mods button on the main menu.

# Usage

## When In The Mini Map:
* Right click to add or remove a location under the cursor.
* Press F2 to add or remove a marker at the player's location. \* 
* Hold the Alt key to show the indicator for unsearched loot containers and bodies.

\* Currently the mod supports different marker colors, but cannot be added using the mouse on the minimap. This may or may not be changed in the future.

## When In Inventory Or When Not In The Minimap:
* Press F2,F3, or F4 to add a marker at the player's location.  Each key is a different color.  Note that this *only* adds markers to prevent accidentally removing a marker.
* Press Shift+F2 to remove the marker at the player's location.
* Hovering over a marker will show all the items at that location\*.

\* The hover listing can be exploited to show anything on the map, but I leave that to the user's personal preferences on such matters :)  

The colors and other settings can be configured using the Mods button on the main menu.


# Configuration
## MCM
This mod supports the Mod Configuration Menu and is the preferred method for changing settings.  Use the Mods button on the main menu.

## Config File
The configuration file will be created on the first game run and can be found at `%AppData%\..\LocalLow\Magnum Scriptum Ltd\Quasimorph_ModConfigs\MapMarkers\config.json`.

### FontSize Note for MCM
Note that there is a bug in the MCM where if the user drag highlights the number, the MCM can become confused and not save the change.
Either use the slider or click on the number box and delete and re-type the value.

When using the slider, the number does not have to be perfect; there is no real difference between using 5.02 or 5.0.

## Key List
The list of valid keyboard keys can be found  at the bottom of https://docs.unity3d.com/ScriptReference/KeyCode.html
Beware that numbers 0-9 are Alpha0 - Alpha9.  Most of the other keys are as expected such as X for X.
Use "None" to not bind the key.

# Support
If you enjoy my mods and want to buy me a coffee, check out my [Ko-Fi](https://ko-fi.com/nbkredspy71915) page.

Thanks!  

# Issues
* Placing a marker at the merc's location can be hard to see due to the merc icon being on top.

# Change Log
See the CHANGELOG.md at https://github.com/NBKRedSpy/Quasimorph-Map-Markers/blob/main/CHANGELOG.md

# Source Code
Source code is available on GitHub at https://github.com/NBKRedSpy/Quasimorph-Map-Markers

# Credits
* Special thanks to Crynano for his excellent Mod Configuration Menu. 
* [Treasure icons created by Smashicons - Flaticon](https://www.flaticon.com/free-icons/treasure)
* [Annotely](https://annotely.com/) for image annotation.

