using MapMarkers.Mcm;
using MapMarkers.Utility;
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
    public Color32 Marker1Color { get; set; } = Color.red;

    /// <summary>
    /// Marker 2 Key - Adds/removes a marker with Color 2 at the player's location.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public KeyCode Marker2Key { get; set; } = KeyCode.F3;

    /// <summary>
    /// The color for Marker 2.
    /// </summary>
    public Color32 Marker2Color { get; set; } = Color.green;

    /// <summary>
    /// Marker 3 Key - Adds/removes a marker with Color 3 at the player's location.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public KeyCode Marker3Key { get; set; } = KeyCode.F4;


    /// <summary>
    /// The color for Marker 3.
    /// </summary>
    public Color32 Marker3Color { get; set; } = Color.yellow;

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

    public ModConfig()
    {
    }

    public ModConfig(string configPath, Utility.Logger logger) : base(configPath, logger)
    {
    }
}
