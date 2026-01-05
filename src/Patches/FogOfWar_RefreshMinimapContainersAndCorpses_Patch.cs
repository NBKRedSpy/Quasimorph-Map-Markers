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

                if(Plugin.Config.ShowSearchedIndicator)
                {
                    AddSearchedIndicator(__instance, Plugin.Config.SearchedIndicatorColor);
                }
                
            }
            catch (Exception ex)
            {
                Plugin.Logger.LogError(ex);
            }
        }

        private static void AddSearchedIndicator(FogOfWar fogOfWar, Color color)
        {
            foreach (MapObstacle obstacle in fogOfWar._mapObstacles.Obstacles)
            {
                bool wasSearched = false;

                if (obstacle.Store != null && obstacle.ObstacleHealth.Health > 0 && obstacle.Store.Looted)
                {
                    wasSearched = true;
                }
                else if (obstacle.CorpseStorage != null && obstacle.CorpseStorage.Looted && obstacle.ObstacleHealth.Health > 0)
                {
                    wasSearched = true;
                }

                //NOTE - Sinks and toilets are weird as they are actually offset from where they really are on the game's internal map.
                //  Not bothering to adjust.
                if (wasSearched)
                {
                    fogOfWar._mapTexture.SetPixel(obstacle.Position.X * 4, obstacle.Position.Y * 4 + 3 , color);
                }
            }
        }
    }
}
