[h1]Quasimorph Map Markers[/h1]


[h1]Description[/h1]

Allows the user to add markers to the minimap.  For instance, mark the location of valuable loot to come back for later.
Support multiple marker colors to prioritize locations. **

To use:

When in the Mini Map:
[list]
[*]Right click to add or remove a location under the cursor.
[*]Press F2 to add or remove a marker at the player's location. **
[/list]

When in inventory or when not in the minimap:
[list]
[*]Press F2,F3, or F4 to add a marker at the player's location.  Each key is a different color.  Note that this [i]only[/i] adds markers to prevent accidentally removing a marker.
[*]Press Shift+F2 to remove the marker at the player's location.
[/list]

Hovering over a marker will show all the items at that location*.

The settings can be configured.

* The hover listing can be exploited to show anything on the map, but I leave that to the user's personal preferences on such matters :)
** Currently the mod supports different marker colors, but cannot be added using the mouse on the minimap. This may or may not be changed in the future.

[h1]Configuration[/h1]

[h2]MCM[/h2]

This mod supports the Mod Configuration Menu and is the preferred method for changing settings.  Use the Mods button on the main menu.

[h2]Config File[/h2]

The configuration file will be created on the first game run and can be found at [i]%AppData%\..\LocalLow\Magnum Scriptum Ltd\Quasimorph_ModConfigs\MapMarkers\config.json[/i].

[h3]FontSize Note for MCM[/h3]

Note that there is a bug in the MCM where if the user drag highlights the number, the MCM can become confused and not save the change.
Either use the slider or click on the number box and delete and re-type the value.

When using the slider, the number does not have to be perfect; there is no real difference between using 5.02 or 5.0.

[h2]Key List[/h2]

The list of valid keyboard keys can be found  at the bottom of https://docs.unity3d.com/ScriptReference/KeyCode.html
Beware that numbers 0-9 are Alpha0 - Alpha9.  Most of the other keys are as expected such as X for X.
Use "None" to not bind the key.

[h1]Support My Mods[/h1]

If you enjoy my mods and want to buy me a coffee, check out my [url=https://ko-fi.com/nbkredspy71915]Ko-Fi[/url] page.
It helps motivate me to maintain as well as create mods.

Thanks!

[h1]Issues[/h1]
[list]
[*]Placing a marker at the merc's location can be hard to see due to the merc icon being on top.
[/list]

[h1]Change Log[/h1]

[h2]1.6.0[/h2]
[list]
[*]Added additional colors
[/list]

[h2]1.5.0[/h2]
[list]
[*]For game version 0.9.7+ only.
[*]Can now remove markers when not in the minimap.  Defaults to Shift+F2.
[*]All keybinds can be set in the Mod Config menu.  The user no longer needs to edit the config.json directly.
[*]Internal: Updated Json to MCM framework to latest iteration.
[/list]

[h2]1.4.0[/h2]
[list]
[*]For game version 0.9.6+ only.
[*]0.9.6 compatibility
[/list]

[h2]More Changes[/h2]
[list]
[*]See the github link for older changes.
[/list]

[h1]Credits[/h1]
[list]
[*]Special thanks to Crynano for his excellent Mod Configuration Menu.
[*][url=https://www.flaticon.com/free-icons/treasure]Treasure icons created by Smashicons - Flaticon[/url]
[*][url=https://annotely.com/]Annotely[/url] for image annotation.
[/list]

[h1]Source Code[/h1]

Source code is available on GitHub at https://github.com/NBKRedSpy/Quasimorph-Map-Markers
