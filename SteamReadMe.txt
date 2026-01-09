[h1]Quasimorph Map Markers[/h1]

[img]https://raw.githubusercontent.com/NBKRedSpy/Quasimorph-Map-Markers/main/media/Marker%20Example.webp[/img]

[h1]Description[/h1]

Allows the user to add markers to the minimap.  For instance, mark the location of valuable loot to come back for later.
Support multiple marker colors to prioritize locations. **

[h1]Functionality:[/h1]

Map Markers:
[list]
[*]Toggle add and remove markers to the minimap.
[*]Supports three different marker colors.
[*]For marked locations, shows a list of the items at that location when hovered in the minimap.
[/list]

Optional Item Display:
[list]
[*]Can optionally add indicators if a container is empty, and the search status of corpse and floor items.
[*]Can optionally always show the list of containers if it has been searched before.
[/list]

[h1]Minimap markers[/h1]

When in the Mini Map:
[list]
[*]Right click to add or remove a location under the cursor.
[*]Press F2 to add or remove a marker at the player's location.
[*]Hover a marker to list the items at that location.
[/list]

When in inventory or when not in the minimap:
[list]
[*]Press F2,F3, or F4 to add a marker at the player's location.  Each key is a different color.  Note that this [i]only[/i] adds markers to prevent accidentally removing a marker.
[*]Press Shift+F2 to remove the marker at the player's location.
[/list]

[h1]Location Search Status[/h1]

[img]https://raw.githubusercontent.com/NBKRedSpy/Quasimorph-Map-Markers/main/media/SearchIndicatorExample.webp[/img] [img]https://raw.githubusercontent.com/NBKRedSpy/Quasimorph-Map-Markers/main/media/Search%20Indicator.webp[/img]

Disabled by default. Adds a dot to the upper left locations of locations on the mini map.
This is great for quickly checking if an item has been searched or is already empty.  It provides the same information available in the base game.

The "search" indicator is a single dot at for each item location.  By default, no dot means the item's contents are unknown, grey means it was searched or known to be empty, yellow indicates it has been searched or known and/or to have items.

As long as a container is visible in the FoW, the empty/full status will be shown since the game shows different images based on the contents.  For instance, an empty bookshelf has no books, while one with items does.

[h3]Notes[/h3]

If a tab shows a + (new), the location will continue to be "not searched" until that tab is examined.
Toilets and sinks will have their markers one spot lower due to how the game visualizes these specific containers.

[h1]Configuration[/h1]

Common configuration options in the Mods Menu:
[list]
[*]Change colors.
[*]Change Hotkeys.
[*]Enable and disable the optional functionality.
[/list]

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

[h1]Change Log[/h1]

[h2]1.7.0[/h2]
[list]
[*]Added status indicator.
[*]Added Show Explored Items option.
[/list]

[h2]1.6.1[/h2]
[list]
[*]0.9.8.2 compatibility.
[/list]

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
