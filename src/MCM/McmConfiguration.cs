using MapMarkers.Mcm;
using MapMarkers.Utility;
using ModConfigMenu;
using ModConfigMenu.Contracts;
using ModConfigMenu.Objects;
using System.Collections.Generic;
using UnityEngine;
using Logger = MapMarkers.Utility.Logger;

namespace MapMarkers.MCM
{
    internal class McmConfiguration : McmConfigurationBase
    {

        public McmConfiguration(ModConfig config) : base (config) { }

        public override void Configure()
        {
            ModConfigMenuAPI.RegisterModConfig("Map Markers", new List<IConfigValue>()
            {
                CreateConfigProperty(nameof(ModConfig.MarkerColorTransform),
                    @"The color of point of interest point on the minimap","Marker Color"),
                CreateConfigProperty(nameof(ModConfig.FontSize),
                    @"The size of the text to use for the marker's content text.",1f, 20f),

                CreateEnumDropdown<KeyCode>(nameof(ModConfig.ClearLocationsKey),
                    @"The key to press to clear all markers for the current level.", "Clear All Locations", "MiniMap"),
                CreateEnumDropdown<KeyCode>(nameof(ModConfig.AddLocationKey),
                    @"Adds a marker under the cursor.", "Add Location", "MiniMap"),
                CreateEnumDropdown<KeyCode>(nameof(ModConfig.AddPlayerLocationKey),
                    @"Adds a marker under the player's current location", "Toggle Player's Location", "MiniMap"),

                CreateEnumDropdown<KeyCode>(nameof(ModConfig.AddPlayerLocationOnDungeonKey),
                    @"Adds a marker at the current location. Note: This only adds", "Add Player Location.", "Mission"),
                CreateEnumDropdown<KeyCode>(nameof(ModConfig.RemovePlayerLocationOnDungeonModifierKey),
                    @"Hold down this key and the 'Add Player Location' key to remove a marker at the player's location.\nSet to None to disable. Ex: Shift + F2","Remove Marker Modifier Key" , "Mission")
            }, OnSave);
        }

    }
}
