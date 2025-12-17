# Quasimorph Map Markers
![marker example](media/Marker%20Example.png)

# Description
Allows the user to add markers to the minimap.  For instance, mark the location of valuable loot to come back for later.
Support multiple marker colors to prioritize locations. \*\*

To use: 

When in the Mini Map:
* Right click to add or remove a location under the cursor.
* Press F2 to add or remove a marker at the player's location. \*\*


When in inventory or when not in the minimap:
* Press F2,F3, or F4 to add a marker at the player's location.  Each key is a different color.  Note that this *only* adds markers to prevent accidentally removing a marker.
* Press Shift+F2 to remove the marker at the player's location.

Hovering over a marker will show all the items at that location\*.

The settings can be configured.

\* The hover listing can be exploited to show anything on the map, but I leave that to the user's personal preferences on such matters :)  
\*\* Currently the mod supports different marker colors, but cannot be added using the mouse on the minimap. This may or may not be changed in the future.

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

# Support My Mods
If you enjoy my mods and want to buy me a coffee, check out my [Ko-Fi](https://ko-fi.com/nbkredspy71915) page.
It helps motivate me to maintain as well as create mods.

Thanks!  

# Issues
* Placing a marker at the merc's location can be hard to see due to the merc icon being on top.

# Change Log
## 1.6.1 
* 0.9.8.2 compatibility. 

## 1.6.0
* Added additional colors

## 1.5.0
* For game version 0.9.7+ only.
* Can now remove markers when not in the minimap.  Defaults to Shift+F2.
* All keybinds can be set in the Mod Config menu.  The user no longer needs to edit the config.json directly.
* Internal: Updated Json to MCM framework to latest iteration.

## 1.4.0
* For game version 0.9.6+ only.
* 0.9.6 compatibility

## More Changes
* See the github link for older changes.

# Credits
* Special thanks to Crynano for his excellent Mod Configuration Menu. 
* [Treasure icons created by Smashicons - Flaticon](https://www.flaticon.com/free-icons/treasure)
* [Annotely](https://annotely.com/) for image annotation.

# Source Code
Source code is available on GitHub at https://github.com/NBKRedSpy/Quasimorph-Map-Markers
