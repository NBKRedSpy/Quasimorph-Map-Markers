using HarmonyLib;
using MGSC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace MapMarkers.Patches;

/// <summary>
/// Handles adding a marker at the merc's location when not in the mini map.
/// This only adds, does not remove
/// </summary>
[HarmonyPatch(typeof(DungeonGameMode), nameof(DungeonGameMode.Update))]
internal static class DungeonGameMode_Update_Patch
{
    public static void Postfix(DungeonGameMode __instance)
    {
		try
		{
            //Player's current location
            //if (Input.GetKeyDown(Plugin.Config.AddPlayerLocationOnDungeonKey) 
            //    && !UI.IsAnyShowing(typeof(DungeonHudScreen), typeof(InventoryScreen)))
            if (Input.GetKeyDown(Plugin.Config.AddPlayerLocationOnDungeonKey))
            {
                if (UI.IsAnyShowing(typeof(DungeonHudScreen), typeof(InventoryScreen), typeof(CorpseInspectWindow))) return;

                PoiLocations locations = Plugin.CurrentSavePoiStorage;

                if (locations == null)
                {
                    throw new ApplicationException("POI locations are not loaded and cannot be updated");
                }

                CellPosition pos = __instance.Creatures.Player.CreatureData.Position;

                //Only add
                if (!locations.CurrentDungeonLevelPois.Contains(pos))
                {
                    locations.CurrentDungeonLevelPois.Add(pos);
                    locations.Save();
                }

                //Always make a sound to let the user know they pressed the correct key.
                Plugin.PlayClickSound();

            }
        }
        catch (Exception ex)
		{
			Plugin.Logger.LogError(ex);
		}
    }
}
