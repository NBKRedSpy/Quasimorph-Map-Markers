using MapMarkers.Utility;
using MapMarkers.Utility.Mcm;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.IO;
using UnityEngine;


namespace MapMarkers;
public class ModConfig : PersistentConfig<ModConfig>, IMcmConfigTarget
{

    /// <summary>
    /// Clears all locations for the current level.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public KeyCode ClearLocationsKey { get; set; } = KeyCode.Delete;

    /// <summary>
    /// Adds a marker under the cursor.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public KeyCode AddLocationKey { get; set; } = KeyCode.Mouse1;

    /// <summary>
    /// Adds a location under the player's current location
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public KeyCode AddPlayerLocationKey { get; set; } = KeyCode.L;

    /// <summary>
    /// The font size to use for the marker's content text.
    /// </summary>
    public float FontSize { get; set; } = 5f;

    /// <summary>
    /// The color of the minimap point of interest indicator.
    /// </summary>
    public Color32 MarkerColor { get; set; } = new Color32(255,0,0,255);

    
    /// <summary>
    /// The Color that is compatible with the MCM
    /// </summary>
    [JsonIgnore]
    public Color MarkerColorTransform
    {
        get => (Color)MarkerColor;
        set => MarkerColor = (Color32)value;
    }

    public ModConfig() 
    {
    }


    public ModConfig(string configPath, Utility.Logger logger) : base(configPath, logger)
    {
    }

}
