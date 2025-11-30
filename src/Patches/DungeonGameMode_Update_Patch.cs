using HarmonyLib;
using MGSC;
using System;
using UnityEngine;

namespace MapMarkers.Patches;

/// <summary>
/// Handles adding a marker at the merc's location when not in the mini map.
/// </summary>
[HarmonyPatch(typeof(DungeonGameMode), nameof(DungeonGameMode.Update))]
internal static class DungeonGameMode_Update_Patch
{
    public static void Postfix(DungeonGameMode __instance)
    {
        try
        {
            if (UI.IsAnyShowing(typeof(DungeonHudScreen), typeof(InventoryScreen), typeof(CorpseInspectWindow))) return;

            PoiLocations locations = Plugin.CurrentSavePoiStorage;

            if (locations == null)
            {
                throw new ApplicationException("POI locations are not loaded and cannot be updated");
            }

            CellPosition pos = __instance.Creatures.Player.CreatureData.Position;
            bool isRemoving = Input.GetKey(Plugin.Config.RemovePlayerLocationOnDungeonModifierKey);

            // Check for Marker 1
            if (Input.GetKeyDown(Plugin.Config.Marker1Key))
            {
                HandleMarkerToggle(locations, pos, Plugin.Config.Marker1Color, isRemoving);
            }
            // Check for Marker 2
            else if (Input.GetKeyDown(Plugin.Config.Marker2Key))
            {
                HandleMarkerToggle(locations, pos, Plugin.Config.Marker2Color, isRemoving);
            }
            // Check for Marker 3
            else if (Input.GetKeyDown(Plugin.Config.Marker3Key))
            {
                HandleMarkerToggle(locations, pos, Plugin.Config.Marker3Color, isRemoving);
            }
        }
        catch (Exception ex)
        {
            Plugin.Logger.LogError(ex);
        }
    }

    private static void HandleMarkerToggle(PoiLocations locations, CellPosition pos, Color32 color, bool isRemoving)
    {
        if (isRemoving)
        {
            if (locations.RemoveMarker(pos))
            {
                locations.Save();
                Plugin.PlayClickSound();
            }
        }
        else
        {
            locations.AddOrUpdateMarker(pos, color);
            locations.Save();
            Plugin.PlayClickSound();
        }
    }
}
