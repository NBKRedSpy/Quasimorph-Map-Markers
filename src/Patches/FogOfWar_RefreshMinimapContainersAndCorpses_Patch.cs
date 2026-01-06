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
    public static partial class FogOfWar_RefreshMinimapContainersAndCorpses_Patch
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


        /// <summary>
        /// Adds the searched and empty indicators to objects on the minimap.
        /// </summary>
        /// <param name="fogOfWar">Source of the map data and mini map screen</param>
        /// <param name="searchedColor">The color for containers that were searched and not empty.</param>
        /// <param name="emptyColor">The color for empty containers.  This overrides the searched indicator.</param>
        private static void AddSearchedAndEmptyIndicator(FogOfWar fogOfWar, Color searchedColor, Color emptyColor)
        {

            CellSearchInfo cellItemsState = new();


            //Debug: test position to make conditional breakpoints easier.
            //Remove when done testing.
            Position pos = new Position(54, 70);
            Position pos2 = new Position(54, 70);


            //These are the "floor" tab for cell.  Can be a single item or a stack of items.
            //  The tile can have bodies and a floor stack.
            foreach (ItemOnFloor floorItem in fogOfWar._itemsOnFloor.Values)
            {
                MapCell cell = fogOfWar._mapGrid.GetCell(floorItem.pos);

                //Not sure what the difference is between IsExplored and isSeen, but this check is from RefreshMinimap.
                if (!cell.IsExplored && !cell.isSeen)
                {
                    //Don't show indicator for not seen tiles or unsearched items.
                    continue;
                }

                CellItemsState newState;

                if(floorItem.Storage.WasExamined)
                {
                    newState = floorItem.Storage.Empty ? CellItemsState.Empty : CellItemsState.SearchedNotEmpty;
                }
                else
                {
                    newState = CellItemsState.NotSearched;  

                }

                cellItemsState.SetCellState(cell.Position, newState);
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

                //Obstacle.  For instance a barrel.  Cannot have any other types of storage such as bodies.
                if (obstacle.Store != null && obstacle.ObstacleHealth.Health > 0)
                {
                    //empty, searched, not searched...

                    CellItemsState newState = CellItemsState.Invalid;


                    //Obstalces are different as their empty state is visible as long as it is not in the FOW.
                    if (obstacle.Store.storage.Empty)
                    {
                        newState = CellItemsState.Empty; 
                    }
                    else if (obstacle.Store.Looted)
                    {
                        newState = CellItemsState.SearchedNotEmpty;
                    }
                    else
                    {
                        newState = CellItemsState.NotSearched;
                    }

                    cellItemsState.SetCellState(cell.Position, newState);
                }
                else if (obstacle.CorpseStorage != null && obstacle.ObstacleHealth.Health > 0)
                {
                    CellItemsState newState = CellItemsState.Invalid;

                    if(obstacle.CorpseStorage.Looted)
                    {
                        newState = obstacle.CorpseStorage.CreatureData.Inventory.Empty ? CellItemsState.Empty : CellItemsState.SearchedNotEmpty;
                    }
                    else
                    {
                        newState = CellItemsState.NotSearched;
                    }

                    cellItemsState.SetCellState(cell.Position, newState);
                }
            }

            foreach (var cellItem in cellItemsState.CellStates)
            {

                CellItemsState state = cellItem.Value;
                Color indicatorColor;

                switch (state)
                {
                    case CellItemsState.NotSearched:
                        continue;
                    case CellItemsState.SearchedNotEmpty:
                        indicatorColor = searchedColor;
                        break;
                    case CellItemsState.Empty:
                        indicatorColor = emptyColor;    
                        break;
                    default:
                        throw new ApplicationException("Unexpected cell state found when adding searched/empty indicators to minimap. " +
                            $"Value: '{state}'");
                }
                

                ////NOTE - Sinks and toilets will show the indicator offeset from the minimap location. 
                ////  The dungeon regular map does this too.  Not bothering to adjust as it is not a big deal.
                fogOfWar._mapTexture.SetPixel(cellItem.Key.X * 4, cellItem.Key.Y * 4 + 3, indicatorColor);

            }

        }
    }
}
