using HarmonyLib;
using MapMarkers.Utility;
using MapMarkers_Bootstrap;
using MGSC;
using System;
using System.IO;
using UnityEngine;

namespace MapMarkers
{
    public class Plugin : BootstrapMod
    {


        /// <summary>
        /// The Point of Interest entries for the currently loaded save.
        /// </summary>
        internal static PoiLocations CurrentSavePoiStorage { get; set; }

        /// <summary>
        /// The slot number for the save that is currently loaded.
        /// </summary>
        internal static int LoadedSlotNumber { get; set; }  

        public static ConfigDirectories ConfigDirectories = new ConfigDirectories();

        public static ModConfig Config { get; private set; }

        public static Utility.Logger Logger = new();

        public static State State { get; private set; }

        private static McmConfiguration McmConfiguration;

        public Plugin(HookEvents hookEvents, bool isBeta) : base(hookEvents, isBeta)
        {
            HookEvents.AfterConfigsLoaded += AfterConfig;

        }


        //[Hook(ModHookType.AfterConfigsLoaded)]
        public static void AfterConfig(IModContext context)
        {
            Directory.CreateDirectory(ConfigDirectories.ModPersistenceFolder);
            State = context.State;

            Config = ModConfig.LoadConfig(ConfigDirectories.ConfigPath, Logger);

            McmConfiguration = new McmConfiguration(Config, Logger);
            McmConfiguration.TryConfigure();


            new Harmony("NBKRedSpy_" + ConfigDirectories.ModAssemblyName).PatchAll();
        }

        /// <summary>
        /// Creates or loads the Point of Interest data for the currently loaded save.
        /// </summary>
        public static void CreateOrLoadCurrentPoi(string locationId)
        {
            try
            {
                int slot = State.Get<SavedGameMetadata>().Slot;

                PoiLocations location = PoiLocations.CreateOrLoad(ConfigDirectories.ModPersistenceFolder, slot, Logger);
                CurrentSavePoiStorage = location;
                location.SetDungeonLevel(locationId);
                location.Save();
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Error creating or loading the POI data");
                CurrentSavePoiStorage = null;
            }

        }

        public static void PlayClickSound()
        {
            SingletonMonoBehaviour<SoundController>.Instance.PlayUiSound(SingletonMonoBehaviour<SoundsStorage>.Instance.ButtonClick);
        }
    }
}
