# Quasimorph Map Markers
![marker example](media/Marker%20Example.png)

Allows the user to add markers to the minimap.  For instance, mark the location of valuable loot to come back for later.

To use: Open the mini map and right click a location.  Right click again to toggle the location off.
Press the delete key to clear all markers for the current floor.

The settings can be configured.

# Configuration

## MCM
This mod supports the Mod Configuration Menu. Some values can be set in the Mods menu, while others can only be changed in the config file.

## Config File
The configuration file will be created on the first game run and can be found at `%AppData%\..\LocalLow\Magnum Scriptum Ltd\Quasimorph_ModConfigs\MapMarkers\config.json`.

|Name|Default|Description|
|--|--|--|
|ClearLocationsKey|Delete|Deletes all of the map markers for the current level.|
|AddLocationKey|Right Click|The key or mouse button to add or remove a marker.|
|AddPlayerLocationKey|L|Adds a marker the the player's current location.|
|MarkerColor|Red|The color of the markers.|

## Key List
The list of valid keyboard keys can be found  at the bottom of https://docs.unity3d.com/ScriptReference/KeyCode.html
Beware that numbers 0-9 are Alpha0 - Alpha9.  Most of the other keys are as expected such as X for X.
Use "None" to not bind the key.

# Support My Mods
If you enjoy my mods and want to buy me a coffee, check out my [Ko-Fi](https://ko-fi.com/nbkredspy71915) page.
It helps motivate me to maintain as well as create mods.

Thanks!  

# Issues
* Placing a marker at the merc's location can be hard to see due to the marc icon being on top.
* No ability to add notes.  Recommend using an alternative such as Steam's overlay notes.
* No displayed cell number.  Does not show the cell number under the cursor, which could be helpful with creating notes.

# Credits
* Special thanks to Crynano for his excellent Mod Configuration Menu. 
* [Treasure icons created by Smashicons - Flaticon](https://www.flaticon.com/free-icons/treasure)
* [Annotely](https://annotely.com/) for image annotation.

# Source Code
Source code is available on GitHub at https://github.com/NBKRedSpy/MapMarkers
