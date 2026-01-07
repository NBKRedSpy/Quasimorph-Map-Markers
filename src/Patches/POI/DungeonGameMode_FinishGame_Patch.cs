using HarmonyLib;
using MGSC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapMarkers.Patches.POI;

/// <summary>
/// Handles saving the POI state on level change
/// Misleading name.  FinishGame includes changing to different levels 
/// as well merc dying or winning.
/// </summary>
[HarmonyPatch(typeof(DungeonGameMode), nameof(DungeonGameMode.FinishGame))]
internal static class DungeonGameMode_FinishGame_Patch
{
    public static void Postfix(DungeonGameMode __instance, DungeonFinishedData dungeonFinishedData)
    {
        string location = dungeonFinishedData.To?.LocationUniqueId;

        if (string.IsNullOrEmpty(location)) return; //This was a dungeon exit.  Death, evac, etc.

        Plugin.CurrentSavePoiStorage.SetDungeonLevel(location);
    }
}
