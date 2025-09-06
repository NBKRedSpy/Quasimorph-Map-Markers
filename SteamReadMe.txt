[h1]Quasimorph Map Markers[/h1]


Allows the user to add markers to the minimap.  For instance, mark the location of valuable loot to come back for later.

To use:

When in the Mini Map:
[list]
[*]Right click to add or remove a location under the cursor.
[*]Press F2 to add or remove a marker at the player's location
[/list]

When in inventory or when not in the minimap:
[list]
[*]Press F2 to add a marker at the player's location.  Note that this [i]only[/i] adds markers to prevent accidentally removing a marker.
[/list]

Hovering over a marker will show all the items at that location*.

The settings can be configured.

* The hover listing can be exploited to show anything on the map, but I leave that to the user's personal preferences on such matters :)

[h1]Configuration[/h1]

[h2]MCM[/h2]

This mod supports the Mod Configuration Menu. Some values can be set in the Mods menu, while others can only be changed in the config file.

[h2]Config File[/h2]

The configuration file will be created on the first game run and can be found at [i]%AppData%\..\LocalLow\Magnum Scriptum Ltd\Quasimorph_ModConfigs\MapMarkers\config.json[/i].
[table]
[tr]
[td]Name
[/td]
[td]Default
[/td]
[td]Description
[/td]
[/tr]
[tr]
[td]ClearLocationsKey
[/td]
[td]Delete
[/td]
[td]Deletes all of the map markers for the current level.
[/td]
[/tr]
[tr]
[td]AddLocationKey
[/td]
[td]Right Click
[/td]
[td]The key or mouse button to add or remove a marker.
[/td]
[/tr]
[tr]
[td]AddPlayerLocationKey
[/td]
[td]F2
[/td]
[td]Adds or removes a marker the the player's current location.
[/td]
[/tr]
[tr]
[td]AddPlayerLocationOnDungeonKey
[/td]
[td]F2
[/td]
[td]Adds a marker to the player's current location not in the minimap.  This [i]only[/i] adds
[/td]
[/tr]
[tr]
[td]MarkerColor
[/td]
[td]Red
[/td]
[td]The color of the markers.
[/td]
[/tr]
[tr]
[td]FontSize
[/td]
[td]5
[/td]
[td]The size of the text to use for the marker's content text.  Important!  See FontSize note below.
[/td]
[/tr]
[/table]

[h3]FontSize Note for MCM[/h3]

Only use the slider in the Mod Config Menu.  The number cannot be typed in currently.  Additionally, pressing backspace closes the mod settings UI, which causes the config UI to become confused.

The number does not have to be perfect, so 5.02 vs 5.0 makes no real difference.

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

[h2]1.2.1[/h2]
[list]
[*]Fixed the "not in minimap screen" mode did not place marker when storage was open.
[/list]

[h2]1.2.0[/h2]
[list]
[*]Added hotkey to add a marker when in inventory and not the mini map.
[*]Added game's "Click" sounds to the actions.
[*]Changed defaults of "Add Marker" to F2
[/list]

[h2]1.1.0[/h2]
[list]
[*]Added listing the items at a marker when hovered.
[/list]

[h1]Credits[/h1]
[list]
[*]Special thanks to Crynano for his excellent Mod Configuration Menu.
[*][url=https://www.flaticon.com/free-icons/treasure]Treasure icons created by Smashicons - Flaticon[/url]
[*][url=https://annotely.com/]Annotely[/url] for image annotation.
[/list]

[h1]Source Code[/h1]

Source code is available on GitHub at https://github.com/NBKRedSpy/Quasimorph-Map-Markers
