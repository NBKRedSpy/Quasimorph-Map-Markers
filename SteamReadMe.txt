[h1]Quasimorph Map Markers[/h1]


[h1]Overview[/h1]

Ever been in a mission and wanted to come back for something later?  But then forget where the heck it was?

Do you have the floor scanner and are tired of having to hunt down the ten storage items that aren't empty out of hundred on the level?

This mod adds the ability to add markers to the minimap, as well as show the search status of storage items.

Hold alt to show only the storage items that have not been searched yet.  Optionally, this mode can hide items such as barrels, sinks, and toilets.  Must be enabled in the settings.

The list of items to hide can be configured in the config file.  See the Hide Items List section.

Colors, hotkeys, and other options can be changed via the Mods button on the main menu.

[h1]Usage[/h1]

[h2]When In The Mini Map:[/h2]
[list]
[*]Right click to add or remove a location under the cursor.
[*]Press F2 to add or remove a marker at the player's location. *
[*]Hold the Alt key to show the indicator for unsearched loot containers and bodies.
[/list]

* Currently the mod supports different marker colors, but cannot be added using the mouse on the minimap. This may or may not be changed in the future.

[h2]When In Inventory Or When Not In The Minimap:[/h2]
[list]
[*]Press F2,F3, or F4 to add a marker at the player's location.  Each key is a different color.  Note that this [i]only[/i] adds markers to prevent accidentally removing a marker.
[*]Press Shift+F2 to remove the marker at the player's location.
[*]Hovering over a marker will show all the items at that location*.
[/list]

* The hover listing can be exploited to show anything on the map, but I leave that to the user's personal preferences on such matters :)

The colors and other settings can be configured using the Mods button on the main menu.

[h1]Configuration[/h1]

[h2]MCM[/h2]

This mod supports the Mod Configuration Menu and is the preferred method for changing settings.  Use the Mods button on the main menu.

[h2]Config File[/h2]

The configuration file will be created on the first game run and can be found at [i]%AppData%\..\LocalLow\Magnum Scriptum Ltd\Quasimorph_ModConfigs\MapMarkers\config.json[/i].

[h3]HideItemsList[/h3]

[i]HideItemsList[/i] is the list of obstacle ids that are hidden when [i]Hide Items in Alt Mode[/i] is enabled.  This list can only be changed by editing the config file directly; it cannot be edited from the MCM menu.

[h3]FontSize Note for MCM[/h3]

Note that there is a bug in the MCM where if the user drag highlights the number, the MCM can become confused and not save the change.
Either use the slider or click on the number box and delete and re-type the value.

When using the slider, the number does not have to be perfect; there is no real difference between using 5.02 or 5.0.

[h2]Key List[/h2]

The list of valid keyboard keys can be found  at the bottom of https://docs.unity3d.com/ScriptReference/KeyCode.html
Beware that numbers 0-9 are Alpha0 - Alpha9.  Most of the other keys are as expected such as X for X.
Use "None" to not bind the key.

[h1]Support[/h1]

If you enjoy my mods and want to buy me a coffee, check out my [url=https://ko-fi.com/nbkredspy71915]Ko-Fi[/url] page.

Thanks!

[h1]Issues[/h1]
[list]
[*]Placing a marker at the merc's location can be hard to see due to the merc icon being on top.
[/list]

[h1]Change Log[/h1]

See the CHANGELOG.md at https://github.com/NBKRedSpy/Quasimorph-Map-Markers/blob/main/CHANGELOG.md

[h1]Source Code[/h1]

Source code is available on GitHub at https://github.com/NBKRedSpy/Quasimorph-Map-Markers

[h1]Credits[/h1]
[list]
[*]Special thanks to Crynano for his excellent Mod Configuration Menu.
[*][url=https://www.flaticon.com/free-icons/treasure]Treasure icons created by Smashicons - Flaticon[/url]
[*][url=https://annotely.com/]Annotely[/url] for image annotation.
[/list]
