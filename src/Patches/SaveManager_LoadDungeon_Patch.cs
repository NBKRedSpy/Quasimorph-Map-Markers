using HarmonyLib;
using MGSC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapMarkers.Patches
{
    /// <summary>
    /// Called when a dungeon is loaded.
    /// </summary>
    [HarmonyPatch(typeof(SaveManager), nameof(SaveManager.LoadDungeon))]
    internal static class SaveManager_LoadDungeon_Patch
    {
        public static void Postfix(string locationId)
        {
            Plugin.CreateOrLoadCurrentPoi(locationId);
        }
    }
}
