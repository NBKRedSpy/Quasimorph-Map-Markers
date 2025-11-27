using HarmonyLib;
using MGSC;
using System;
using System.Collections.Generic;
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
                List<MarkerData> markers = Plugin.CurrentSavePoiStorage?.CurrentDungeonLevelPois;

                if (markers == null)
                {
                    Plugin.Logger.LogError("The POI locations were not loaded");
                    return;
                }

                foreach (MarkerData marker in markers)
                {
                    TextureHelper.FillWithColorTo32(TextureHelper.FillMode.Rewrite, __instance._mapTexture, marker.Color,
                        new CellPosition(marker.Position.X * 4, marker.Position.Y * 4), 4, 4, applyTexture: false);
                }
            }
            catch (Exception ex)
            {
                Plugin.Logger.LogError(ex);
            }
        }
    }
}
