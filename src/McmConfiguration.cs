using MapMarkers.Utility;
using MapMarkers.Utility.Mcm;
using ModConfigMenu;
using ModConfigMenu.Objects;
using System.Collections.Generic;
using Logger = MapMarkers.Utility.Logger;

namespace MapMarkers
{
    internal class McmConfiguration : McmConfigurationBase
    {

        public McmConfiguration(ModConfig config, Logger logger) : base (config, logger) { }

        public override void Configure()
        {
            ModConfigMenuAPI.RegisterModConfig("Map Markers", new List<ConfigValue>()
            {
                CreateConfigProperty(nameof(ModConfig.MarkerColorTransform),
                    @"The color of point of interest point on the minimap","Marker Color"),
                CreateConfigProperty(nameof(ModConfig.FontSize),
                    @"The size of the text to use for the marker's content text.",1,20),
                CreateReadOnly(nameof(ModConfig.AddLocationKey)),
                CreateReadOnly(nameof(ModConfig.AddPlayerLocationKey)),
                CreateReadOnly(nameof(ModConfig.ClearLocationsKey)),

            }, OnSave);
        }
    }
}
