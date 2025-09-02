using HarmonyLib;
using MGSC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace MapMarkers.Patches
{

    /// <summary>
    /// Renders the Points Of Interest on the minimap.
    /// </summary>
    [HarmonyPatch(typeof(FogOfWar), nameof(FogOfWar.RefreshMinimapContainersAndCorpses))]
    public static class FogOfWar_RefreshMinimapContainersAndCorpses_Patch
    {

        public static void Postfix(FogOfWar __instance)
        {
            try
            {

                //Note - this could have been any of the updates in the function.
                //Just an easy place to inject the map changes.

                Color32 color = Plugin.Config.MarkerColor;

                List<CellPosition> locations = Plugin.CurrentSavePoiStorage?.CurrentDungeonLevelPois;

                if(locations == null)
                {
                    Plugin.Logger.LogError("The POI locations were not loaded");
                    return;
                }

                foreach (CellPosition cell in locations)
                {
                    TextureHelper.FillWithColorTo32(TextureHelper.FillMode.Rewrite, __instance._mapTexture, color,
                        new CellPosition(cell.X * 4, cell.Y * 4), 4, 4, applyTexture: false);
                }
            }
            catch (Exception ex)
            {
                Plugin.Logger.LogError(ex);
            }
        }
    }
}
