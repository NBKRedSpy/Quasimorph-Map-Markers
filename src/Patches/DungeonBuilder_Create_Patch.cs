using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using MGSC;

namespace MapMarkers.Patches
{
    /// <summary>
    /// Called when a dungeon is created (new) or loaded (resumed)
    /// Loads the POI data for the current save.
    /// </summary>
    [HarmonyPatch(typeof(DungeonBuilder), nameof(DungeonBuilder.Create), 
        new Type[] {typeof(InputMapData)})]
    internal static class DungeonBuilder_Create_Patch
    {
        public static void Postfix(InputMapData inputMapData)
        {
            Plugin.CreateOrLoadCurrentPoi(inputMapData.locationId);
        }
    }
}
