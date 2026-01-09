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
        public McmConfiguration(ModConfig config) : base(config) { }

        public override void Configure()
        {
            ModConfigMenuAPI.RegisterModConfig("Map Markers", new List<IConfigValue>()
            {
                CreateConfigProperty(nameof(ModConfig.FontSize),
                    @"The size of the text to use for the marker's content text.", 1f, 20f),

                CreateConfigProperty(nameof(ModConfig.ShowExploredItems),
                    @"Always show items from explored containers, corpses, and floor tiles when hovering over a cell. Even without a map marker.", 
                    "Always Show Explored Items"),

                #region Search Indicator
                CreateConfigProperty(nameof(ModConfig.ShowSearchedIndicator),
                    @"On the minimap, add an indicator for any items that have been searched",
                    "Show Searched Indicator", "Search Indicator"),

                CreateConfigProperty(nameof(ModConfig.SearchedIndicatorColorTransform), 
                    @"The color of the searched indicator", "Searched Indicator Color", "Search Indicator"),

                                CreateConfigProperty(nameof(ModConfig.EmptyIndicatorColorTransform),
                    @"The indicator color for containers that are empty", "Empty Indicator Color", "Search Indicator"),

                #endregion

                #region Points of interest
                CreateEnumDropdown<KeyCode>(nameof(ModConfig.ClearLocationsKey),
                    @"The key to press to clear all markers for the current level.", "Clear All Locations", "MiniMap"),
                CreateEnumDropdown<KeyCode>(nameof(ModConfig.AddLocationKey),
                    @"Adds a marker under the cursor.", "Add Location", "MiniMap"),
                CreateEnumDropdown<KeyCode>(nameof(ModConfig.AddPlayerLocationKey),
                    @"Adds a marker under the player's current location", "Toggle Player's Location", "MiniMap"),

                CreateEnumDropdown<KeyCode>(nameof(ModConfig.Marker1Key),
                    @"Key to add/remove a Marker 1 at the player's location", "Marker 1 Key", "Markers"),
                CreateConfigProperty(nameof(ModConfig.Marker1ColorTransform),
                    @"The color of Marker 1", "Marker 1 Color", "Markers"),

                CreateEnumDropdown<KeyCode>(nameof(ModConfig.Marker2Key),
                    @"Key to add/remove a Marker 2 at the player's location", "Marker 2 Key", "Markers"),
                CreateConfigProperty(nameof(ModConfig.Marker2ColorTransform),
                    @"The color of Marker 2", "Marker 2 Color", "Markers"),

                CreateEnumDropdown<KeyCode>(nameof(ModConfig.Marker3Key),
                    @"Key to add/remove a Marker 3 at the player's location", "Marker 3 Key", "Markers"),
                CreateConfigProperty(nameof(ModConfig.Marker3ColorTransform),
                    @"The color of Marker 3", "Marker 3 Color", "Markers"),

                CreateEnumDropdown<KeyCode>(nameof(ModConfig.RemovePlayerLocationOnDungeonModifierKey),
                    @"Hold down this key and any marker key to remove a marker at the player's location.\nSet to None to disable. Ex: Shift + F1", "Remove Marker Modifier Key", "Markers")
                #endregion  
            }, OnSave);
        }
    }
}
