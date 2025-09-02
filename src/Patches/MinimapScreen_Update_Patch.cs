using HarmonyLib;
using MGSC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace MapMarkers.Patches;


/// <summary>
/// Handles adding and removing locations on the minimap screen
/// </summary>
[HarmonyPatch(typeof(MinimapScreen), nameof(MinimapScreen.Update))]
internal static class MinimapScreen_Update_Patch
{

    public static void Postfix(MinimapScreen __instance)
    {
        try
        {


            //Right click to add item.
            if(Input.GetKeyDown(Plugin.Config.AddLocationKey))
            {
                PoiLocations locations = Plugin.CurrentSavePoiStorage;

                if (locations == null)
                {
                    Plugin.Logger.LogError("POI locations are not loaded and cannot be updated");
                    return;
                }

                Vector2 normCursorPos = __instance._minimapCursorPosHandler.NormCursorPos;
                int x = Mathf.RoundToInt(normCursorPos.x * (float)__instance._mapGrid.MaxWidth);
                int y = Mathf.RoundToInt(normCursorPos.y * (float)__instance._mapGrid.MaxHeight);

                CellPosition pos = new CellPosition(x,y);

                //Out of bounds.
                if(x < 0 || y < 0 || x > __instance._mapGrid.MaxWidth || y > __instance._mapGrid.MaxHeight)
                {
                    //WARNING: Early exit
                    return;     
                }

                if (locations.CurrentDungeonLevelPois.Contains(pos))
                {
                    locations.CurrentDungeonLevelPois.Remove(pos);
                }
                else
                {
                    locations.CurrentDungeonLevelPois.Add(pos);
                }

                locations.Save();

                __instance._fogOfWar.RefreshMinimap(__instance._locationMetadata.ScanMonsters, __instance._locationMetadata.ScanItems, __instance._locationMetadata.ScanExit);
            }

            if(Input.GetKeyDown(Plugin.Config.ClearLocationsKey))
            {
                PoiLocations locations = Plugin.CurrentSavePoiStorage;
                locations.CurrentDungeonLevelPois.Clear();
                locations.Save();

                RefreshMap(__instance);
            }

            //Player's current location
            if (Input.GetKeyDown(Plugin.Config.AddPlayerLocationKey))
            {
                PoiLocations locations = Plugin.CurrentSavePoiStorage;

                if (locations == null)
                {
                    Plugin.Logger.LogError("POI locations are not loaded and cannot be updated");
                    return;
                }

                CellPosition pos = __instance._creatures.Player.CreatureData.Position;
                if (locations.CurrentDungeonLevelPois.Contains(pos))
                {
                    locations.CurrentDungeonLevelPois.Remove(pos);
                }
                else
                {
                    locations.CurrentDungeonLevelPois.Add(pos);
                }

                locations.Save();

                RefreshMap(__instance);
            }
        }
        catch (Exception ex)
        {
            Plugin.Logger.LogError(ex);
        }
    }

    private static void RefreshMap(MinimapScreen __instance)
    {
        __instance._fogOfWar.RefreshMinimap(__instance._locationMetadata.ScanMonsters, __instance._locationMetadata.ScanItems, __instance._locationMetadata.ScanExit);
    }
}

