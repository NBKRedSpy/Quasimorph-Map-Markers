using HarmonyLib;
using MGSC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapMarkers.Patches.POI
{
    /// <summary>
    /// Handles every dungeon exit and new dungeon creation.
    /// Includes when a save is deleted, etc.
    /// </summary>
    [HarmonyPatch(typeof(SaveManager), nameof(SaveManager.RemoveAllDungeonSaves))]
    internal static class SaveManager_RemoveAllDungeonSaves_Patch
    {
        public static void Prefix(int slot)
        {

            try
            {
                //This is technically unnecessary since every new dungeon generation creates a new POI, but leaving it.
                PoiLocations.Delete(Plugin.ConfigDirectories.ModPersistenceFolder, slot);
                Plugin.CurrentSavePoiStorage = null;

            }
            catch (Exception ex)
            {
                Plugin.Logger.LogError(ex);
            }        
        }
    }
}
