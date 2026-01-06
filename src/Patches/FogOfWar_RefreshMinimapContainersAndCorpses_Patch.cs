using HarmonyLib;
using MGSC;
using System;
using System.Collections.Generic;
using System.Drawing.Text;
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
                    AddSearchedAndEmptyIndicator(__instance, Plugin.Config.SearchedIndicatorColor, Plugin.Config.EmptyIndicatorColor);
                }
                
            }
            catch (Exception ex)
            {
                Plugin.Logger.LogError(ex);
            }
        }

        public record Position(int X, int Y)
        {

            public Position(CellPosition cellPosition) : this(cellPosition.X, cellPosition.Y)
            {

            }
        }




        /// <summary>
        /// Adds the searched and empty indicators to objects on the minimap.
        /// </summary>
        /// <param name="fogOfWar">Source of the map data and mini map screen</param>
        /// <param name="searchedColor">The color for containers that were searched and not empty.</param>
        /// <param name="emptyColor">The color for empty containers.  This overrides the searched indicator.</param>
        private static void AddSearchedAndEmptyIndicator(FogOfWar fogOfWar, Color searchedColor, Color emptyColor)
        {


            //Contains any item storage locations which have been searched.
            //  true = has items, false it is empty.
            Dictionary<Position, bool> searchedCells = new();


            //Debug cell

            Position pos = new Position(54, 70);
            Position pos2 = new Position(54, 70);

            foreach (ItemOnFloor floorItem in fogOfWar._itemsOnFloor.Values)
            {
                MapCell cell = fogOfWar._mapGrid.GetCell(floorItem.pos);

                //Not sure what the difference is between IsExplored and isSeen, but this check is from RefreshMinimap.
                if ((!cell.IsExplored && !cell.isSeen) || floorItem.Storage.Empty || !floorItem.Storage.WasExamined)
                {
                    //Don't show indicator for not seen tiles or unsearched items.
                    continue;
                }

                //TODO:  Fix bug: mixed states between items on the floor and corpses are messing up.
                //If a corpse is on the location and empty, but a floor item hasn't been examined, the indicator shows as empty from the corpse.

                //State:
                // If a tile has multiple items, choose no indicator if any are unexamined.
                // FloorItemNotExamined
                // Examined showing indicator.
                // CorpseEmpty
                // CorpseNotEmpty

                searchedCells[new Position(floorItem.pos)] = true;
            }
            
            // Iterate through all obstacles on the map to find searched or empty containers and corpses.
            foreach (MapObstacle obstacle in fogOfWar._mapObstacles.Obstacles)
            {
                MapCell cell = fogOfWar._mapGrid.GetCell(obstacle.pos);
                
                //Not sure what the difference is between the two, but this check is from RefreshMinimap.
                if (!cell.IsExplored && !cell.isSeen)
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
                    isEmpty = obstacle.CorpseStorage.CreatureData.Inventory.Empty;
                    wasSearched = obstacle.CorpseStorage.Looted;
                }

                if (!wasSearched && !isEmpty)
                {
                    continue;
                }

                //Translate the position
                Position positionKey = new Position(obstacle.Position);
                //Check if there is an existing state.  If not, default to false for "empty"
                searchedCells.TryGetValue(positionKey, out bool existingState);

                searchedCells[positionKey] = existingState || !isEmpty;
            }

            foreach (KeyValuePair<Position, bool> item in searchedCells)
            {
                Color indicatorColor = item.Value ? searchedColor : emptyColor;

                ////NOTE - Sinks and toilets will show the indicator offeset from the minimap location. 
                ////  The dungeon regular map does this too.  Not bothering to adjust as it is not a big deal.
                fogOfWar._mapTexture.SetPixel(item.Key.X * 4, item.Key.Y * 4 + 3, indicatorColor);

            }

        }
    }
}
