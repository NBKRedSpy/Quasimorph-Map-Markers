using HarmonyLib;
using MGSC;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace MapMarkers.Patches
{
    /// <summary>
    /// Handles marking up the minimap with POI locations and searched indicators.
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
                    AddSearchedIndicator(__instance, Plugin.Config.SearchedIndicatorColor, Plugin.Config.EmptyIndicatorColor);
                }
                
            }
            catch (Exception ex)
            {
                Plugin.Logger.LogError(ex);
            }
        }

        private static void AddSearchedIndicator(FogOfWar fogOfWar, Color searchedColor, Color emptyColor)
        {
            // Iterate through all obstacles on the map to find searched or empty containers and corpses.
            foreach (MapObstacle obstacle in fogOfWar._mapObstacles.Obstacles)
            {
                MapCell cell = fogOfWar._mapGrid.GetCell(obstacle.pos);

                //Not sure what the difference is between the two, but this check is from RefreshMinimap.
                if(!cell.IsExplored && !cell.isSeen)
                {
                    //Don't show indicators for seen/visible cells.
                    continue;
                }

                bool wasSearched = false;
                bool isEmpty = false;

                if (obstacle.Store != null && obstacle.ObstacleHealth.Health > 0)
                {
                    isEmpty = obstacle.Store.storage.Empty;
                    wasSearched = obstacle.Store.Looted;
                }
                else if (obstacle.CorpseStorage != null && obstacle.ObstacleHealth.Health > 0)
                {
                    wasSearched = obstacle.CorpseStorage.Looted;
                    isEmpty = obstacle.CorpseStorage.CreatureData.Inventory.Empty;
                }

                if(!wasSearched && !isEmpty)
                {
                    continue;
                }

                Color indicatorColor = isEmpty ? emptyColor : searchedColor;

                //NOTE - Sinks and toilets are weird as they are actually offset from where they really are on the game's internal map.
                //  Not bothering to adjust.
                fogOfWar._mapTexture.SetPixel(obstacle.Position.X * 4, obstacle.Position.Y * 4 + 3, indicatorColor);
            }
        }
    }
}
