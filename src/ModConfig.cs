using MapMarkers.Mcm;
using MapMarkers.Utility;
using MGSC;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using UnityEngine;

namespace MapMarkers;

public class ModConfig : PersistentConfig<ModConfig>, ISave
{
    /// <summary>
    /// Clears all locations for the current level.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public KeyCode ClearLocationsKey { get; set; } = KeyCode.Delete;

    /// <summary>
    /// MiniMap: Adds a marker under the cursor.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public KeyCode AddLocationKey { get; set; } = KeyCode.Mouse1;

    /// <summary>
    /// MiniMap: Adds a location under the player's current location
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public KeyCode AddPlayerLocationKey { get; set; } = KeyCode.F2;

    /// <summary>
    /// Marker 1 Key - Adds/removes a marker with Color 1 at the player's location.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public KeyCode Marker1Key { get; set; } = KeyCode.F2;

    /// <summary>
    /// The color for Marker 1.
    /// </summary>
    public Color32 Marker1Color { get; set; } = Color.green;

    /// <summary>
    /// Marker 2 Key - Adds/removes a marker with Color 2 at the player's location.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public KeyCode Marker2Key { get; set; } = KeyCode.F3;

    /// <summary>
    /// The color for Marker 2.
    /// </summary>
    public Color32 Marker2Color { get; set; } = Color.yellow;

    /// <summary>
    /// Marker 3 Key - Adds/removes a marker with Color 3 at the player's location.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public KeyCode Marker3Key { get; set; } = KeyCode.F4;


    /// <summary>
    /// The color for Marker 3.
    /// </summary>
    public Color32 Marker3Color { get; set; } = Color.red;

    /// <summary>
    /// Dungeon: Hold down this key with any marker key to remove that marker.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public KeyCode RemovePlayerLocationOnDungeonModifierKey { get; set; } = KeyCode.LeftShift;

    /// <summary>
    /// The font size to use for the marker's content text.
    /// </summary>
    public float FontSize { get; set; } = 5f;

    /// <summary>
    /// The Color that is compatible with the MCM for Marker 1
    /// </summary>
    [JsonIgnore]
    public Color Marker1ColorTransform
    {
        get => (Color)Marker1Color;
        set => Marker1Color = (Color32)value;
    }

    /// <summary>
    /// The Color that is compatible with the MCM for Marker 2
    /// </summary>
    [JsonIgnore]
    public Color Marker2ColorTransform
    {
        get => (Color)Marker2Color;
        set => Marker2Color = (Color32)value;
    }

    /// <summary>
    /// The Color that is compatible with the MCM for Marker 3
    /// </summary>
    [JsonIgnore]
    public Color Marker3ColorTransform
    {
        get => (Color)Marker3Color;
        set => Marker3Color = (Color32)value;
    }

    /// <summary>
    /// If true, will show the contents of items that have been searched (containers, corpses, floor items) when hovering over a cell, 
    /// even if there is no map marker there.
    /// </summary>
    public bool ShowExploredItems { get; set; } = false;

    /// <summary>
    /// On the mini map, objects have been searched will have an indicator added to it.
    /// </summary>
    public bool ShowSearchedIndicator { get; set; } = false;

    /// <summary>
    /// The color of the dot used to indicate a searched container/corpse on the mini map.
    /// </summary>
    public Color32 SearchedIndicatorColor { get; set; } = new Color(251/255f, 227/255f, 67/255f);      //This is Colors.Yellow, but the game's Colors object isn't inited at this point.

    /// <summary>
    /// The Color that is compatible with the MCM for the Searched Indicator
    /// </summary>
    [JsonIgnore]
    public Color SearchedIndicatorColorTransform
    {
        get => (Color)SearchedIndicatorColor;
        set => SearchedIndicatorColor  = (Color32)value;
    }

    /// <summary>
    /// The color of the dot used to indicate an empty container/corpse that has been searched on the mini map.
    /// </summary>
    public Color32 EmptyIndicatorColor { get; set; } = new Color32(90,89, 89, 255);      //Light grey

    /// <summary>
    /// The Color that is compatible with the MCM for the Searched Empty Indicator
    /// </summary>
    [JsonIgnore]
    public Color EmptyIndicatorColorTransform
    {
        get => (Color)EmptyIndicatorColor;
        set => EmptyIndicatorColor  = (Color32)value;
    }



    public ModConfig()
    {
    }

    public ModConfig(string configPath, Utility.Logger logger) : base(configPath, logger)
    {
    }
}
