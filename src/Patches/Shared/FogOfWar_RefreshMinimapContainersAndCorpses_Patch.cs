using HarmonyLib;
using MapMarkers.Patches.CellSearchState;
using MapMarkers.Patches.POI;
using MGSC;
using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Linq;
using UnityEngine;

namespace MapMarkers.Patches.Shared
{

    /// <summary>
    /// Adds the search indicators to the minimap objects.
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
                    AddSearchedAndEmptyIndicator(__instance, MinimapScreen_Update_Patch.UnsearchedDisplayMode, 
                        Plugin.Config.UnsearchedIndicatorColor, Plugin.Config.SearchedIndicatorColor, Plugin.Config.EmptyIndicatorColor);
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
        private static void AddSearchedAndEmptyIndicator(FogOfWar fogOfWar, bool showUnsearchedMode, Color unsearchedColor, 
            Color searchedColor, Color emptyColor)
        {

            CellSearchInfo cellItemsState = new();

            SetFloorStates(fogOfWar, cellItemsState);
            SetObstacleStates(fogOfWar, cellItemsState);
            AddUiIndicators(fogOfWar, showUnsearchedMode, unsearchedColor, searchedColor, emptyColor, cellItemsState);
        }

        /// <summary>
        /// Adds the pips to the minimap's storage and corpse icons to indicate if they have been searched or are empty.
        /// </summary>
        /// <param name="fogOfWar"></param>
        /// <param name="showUnsearchedMode"></param>
        /// <param name="unsearchedColor"></param>
        /// <param name="searchedColor"></param>
        /// <param name="emptyColor"></param>
        /// <param name="cellItemsState"></param>
        /// <exception cref="ApplicationException"></exception>
        private static void AddUiIndicators(FogOfWar fogOfWar, bool showUnsearchedMode, Color unsearchedColor, Color searchedColor, Color emptyColor, CellSearchInfo cellItemsState)
        {
            foreach (var cellItem in cellItemsState.CellStates)
            {

                CellItemsState state = cellItem.Value;
                Color indicatorColor;

                if (showUnsearchedMode)
                {
                    if (state != CellItemsState.NotSearched) continue;

                    indicatorColor = unsearchedColor;
                }
                else
                {
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
                }

                ////NOTE - Sinks and toilets will show the indicator offeset from the minimap location. 
                ////  The dungeon regular map does this too.  Not bothering to adjust as it is not a big deal.
                fogOfWar._mapTexture.SetPixel(cellItem.Key.X * 4, cellItem.Key.Y * 4 + 3, indicatorColor);

            }
        }

        /// <summary>
        /// Sets the tile states for floor items.
        /// </summary>
        /// <param name="fogOfWar"></param>
        /// <param name="cellItemsState"></param>
        private static void SetFloorStates(FogOfWar fogOfWar, CellSearchInfo cellItemsState)
        {
            //These are the "floor" tab for cell.  Can be a single item or a stack of items.
            //  The tile can also have bodies.
            foreach (ItemOnFloor floorItem in fogOfWar._itemsOnFloor.Values)
            {
                MapCell cell = fogOfWar._mapGrid.GetCell(floorItem.pos);


                //Not sure what the difference is between IsExplored and isSeen, but this check is from RefreshMinimap.
                if ((!cell.IsExplored && !cell.isSeen) || floorItem.Storage.Empty)
                {
                    //Don't show indicator for not seen tiles or unsearched items.
                    continue;
                }

                //Don't process corpses.  They show in _itemsOnFloor list, but are handled differently.
                //Side note:  The game appears to paint the obstacles and then when corpses are processed, any obstacles are painted over by the corpse icon
                if (fogOfWar._mapObstacles._cellToObstacles.TryGetValue(cell.Position, out var obstacles)
                    && obstacles.Any(o => o.CorpseStorage != null)
                    )
                {
                    continue;
                }

                CellItemsState newState;
                newState = floorItem.Storage.WasExamined ? CellItemsState.SearchedNotEmpty : CellItemsState.NotSearched;

                cellItemsState.SetCellState(cell.Position, newState);
            }
        }

        /// <summary>
        /// Sets the tile state for all obstacles on the map.
        /// They are containers and corpses.
        /// </summary>
        /// <param name="fogOfWar"></param>
        /// <param name="cellItemsState"></param>
        private static void SetObstacleStates(FogOfWar fogOfWar, CellSearchInfo cellItemsState)
        {
            // Iterate through all obstacles on the map to find searched or empty containers and corpses.
            foreach (MapObstacle obstacle in fogOfWar._mapObstacles.Obstacles)
            {
                MapCell cell = fogOfWar._mapGrid.GetCell(obstacle.pos);
                
                //Check if has been seen, has items, valid, etc.
                if ((obstacle.Store == null && obstacle.CorpseStorage == null) 
                    || obstacle.OccupiedCells.Count == 0 || obstacle.ObstacleHealth.Health <= 0 
                    || !obstacle.WasExplored || (!cell.IsExplored && !cell.isSeen))
                {

                    //Not sure what the difference is between cell.IsExplored and cell.isSeen, but this check is from RefreshMinimap.

                    continue;
                }
                //Corpse
                else if (obstacle.CorpseStorage != null)
                {
                    CellItemsState newState = CellItemsState.Invalid;

                    if (obstacle.CorpseStorage.Looted)
                    {
                        newState = obstacle.CorpseStorage.CreatureData.Inventory.Empty ? CellItemsState.Empty : CellItemsState.SearchedNotEmpty;
                    }
                    else
                    {
                        newState = CellItemsState.NotSearched;
                    }

                    cellItemsState.SetCellState(cell.Position, newState);
                }
                //Otherwise this is an obstacle. For instance a barrel.  
                else
                {
                    CellItemsState newState = CellItemsState.Invalid;


                    //Obstacles are different as their empty state is visible as long as it is not in the FOW.
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

            }
        }
    }
}
