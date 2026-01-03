using HarmonyLib;
using MGSC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace MapMarkers.Patches;

/// <summary>
/// Handles adding and removing locations on the minimap screen
/// </summary>
[HarmonyPatch(typeof(MinimapScreen), nameof(MinimapScreen.LateUpdate))]
internal static partial class MinimapScreen_Update_Patch
{
    public static void Postfix(MinimapScreen __instance)
    {
        try
        {
            // Right click to add item.
            if (Input.GetKeyDown(Plugin.Config.AddLocationKey))
            {
                PoiLocations locations = Plugin.CurrentSavePoiStorage;

                if (locations == null)
                {
                    Plugin.Logger.LogError("POI locations are not loaded and cannot be updated");
                    return;
                }

                CellPosition cursorCell = GetCellUnderCursor(__instance);

                // Out of bounds check
                if ((cursorCell.X < 0 || cursorCell.Y > 0 || cursorCell.X < __instance._mapGrid.MaxWidth 
                    || cursorCell.Y < __instance._mapGrid.MaxHeight))
                {
                    if (locations.HasMarkerAt(cursorCell))
                    {
                        locations.RemoveMarker(cursorCell);
                    }
                    else
                    {
                        // Use Marker 1 color by default for cursor-based placement
                        locations.AddOrUpdateMarker(cursorCell, Plugin.Config.Marker1Color);
                    }

                    locations.Save();
                    RefreshMap(__instance);
                    Plugin.PlayClickSound();
                }
            }

            if (Input.GetKeyDown(Plugin.Config.ClearLocationsKey))
            {
                PoiLocations locations = Plugin.CurrentSavePoiStorage;
                locations.CurrentDungeonLevelPois.Clear();
                locations.Save();

                Plugin.PlayClickSound();
                RefreshMap(__instance);
            }

            // Player's current location
            if (Input.GetKeyDown(Plugin.Config.AddPlayerLocationKey))
            {
                PoiLocations locations = Plugin.CurrentSavePoiStorage;

                if (locations == null)
                {
                    throw new ApplicationException("POI locations are not loaded and cannot be updated");
                }

                CellPosition pos = __instance._creatures.Player.CreatureData.Position;
                
                if (locations.HasMarkerAt(pos))
                {
                    locations.RemoveMarker(pos);
                }
                else
                {
                    // Use Marker 1 color by default for player location toggle
                    locations.AddOrUpdateMarker(pos, Plugin.Config.Marker1Color);
                }

                Plugin.PlayClickSound();
                locations.Save();
                RefreshMap(__instance);
            }

            ShowMarkerItems(__instance, __instance._mapGrid);
        }
        catch (Exception ex)
        {
            Plugin.Logger.LogError(ex);
        }
    }

    private static CellPosition GetCellUnderCursor(MinimapScreen __instance)
    {
        Vector2 normCursorPos = __instance._minimapCursorPosHandler.NormCursorPos;
        int x = Mathf.RoundToInt(normCursorPos.x * (float)__instance._mapGrid.MaxWidth);
        int y = Mathf.RoundToInt(normCursorPos.y * (float)__instance._mapGrid.MaxHeight);

        return new CellPosition(x, y);
    }

    public static void ShowMarkerItems(MinimapScreen __instance, MapGrid mapGrid)
    {
        CellPosition cursorCell = GetCellUnderCursor(__instance);

        //Debug
        bool showOnlyExplored = false;

        //emulate an option
        bool showExploredItems = true;


        //// Only run if there is a marker on the floor.
        //if (!Plugin.CurrentSavePoiStorage.CurrentDungeonLevelPois.Any(x => CellsEqual(x.Position, cursorCell))) return;

        //Check for marker at location.
        if (Plugin.CurrentSavePoiStorage.CurrentDungeonLevelPois.Any(x => CellsEqual(x.Position, cursorCell)))
        {
            showOnlyExplored = false;
        }
        else if (showExploredItems)
        {
            showOnlyExplored = true;
        }
        else
        {
            return;
        }

        StringBuilder sb = new StringBuilder();
        
        List<FloorItem> floorItems = GetAllCellItems(__instance, cursorCell, !showOnlyExplored);

        // COPY WARNING:  MGSC.MinimapScreen.RefreshLabelUnderCursor(MGSC.MapCell). This is a modified copy
        // of the first loop in the function.
        if (floorItems.Count > 0)
        {
            foreach (FloorItem item in floorItems)
            {
                string countText = item.Count == 1 ? "" : $" x{item.Count}";
                string text = item.Name;
                sb.Append($"{text}{countText}, ");
            }

            TMPro.TextMeshProUGUI label = __instance._currentObjectLabel;

            label.overflowMode = TMPro.TextOverflowModes.ScrollRect;
            label.color = __instance._objectivesLabelColor;
            label.text = sb.ToString().Trim(' ', ',');
            Localization.ActualizeFontAndSize(__instance._currentObjectLabel);
            label.fontSize = Plugin.Config.FontSize;
            label.enabled = true;
        }
    }

    /// <summary>
    /// Gets the localization text for items.
    /// </summary>
    /// <param name="itemId"></param>
    /// <returns></returns>
    private static string GetItemName(string itemId)
    {
        return Localization.Get($"item.{itemId}.name").FirstLetterToUpperCase();
    }

    /// <summary>
    /// Returns all of the base items for a cell.
    /// All corpses, storages, and items on the floor.
    /// </summary>
    /// <param name="__instance"></param>
    /// <param name="cursorCell"></param>
    /// <param name="examinedOnly"></param>If true, only returns items from examined containers/corpses/floor.</param>
    /// <returns></returns>
    private static List<FloorItem> GetAllCellItems(MinimapScreen __instance, CellPosition cursorCell, bool examinedOnly)
    {
        List<MapObstacle> obstacles = __instance._mapObstacles.GetAll(cursorCell.X, cursorCell.Y, false, false);

        List<BasePickupItem> items = new List<BasePickupItem>();

        IEnumerable<BasePickupItem> storageItems;

        // Corpses
        storageItems = obstacles
            .Where(x => x.CorpseStorage != null && (x.CorpseStorage.WasExamined || !examinedOnly))
            .SelectMany(x =>
                x.CorpseStorage.CreatureData.Inventory
                    .AllContainers.SelectMany(x => x.Items));

        if (storageItems != null) items.AddRange(storageItems);

        // Item storage.  May be multiple
        storageItems = obstacles.SelectMany(x =>
            x.Components
                .Where(x => x is Store)
                .Cast<Store>()
                .Where(x=> x.storage.WasExamined || !examinedOnly)
                .SelectMany(x => x.storage.Items));

        if (storageItems != null) items.AddRange(storageItems);

        // -- check for floor items
        var floorStorage = __instance._itemsOnFloor
            .Get(cursorCell.X, cursorCell.Y);

        storageItems = floorStorage.WasExplored || !examinedOnly ? floorStorage.Storage?.Items : null;


        if (storageItems != null) items.AddRange(storageItems);

        // Sort and return item and count.
        List<FloorItem> groupedCount = items
            .GroupBy(x => x.Id)
            .Select(x => new FloorItem(x.First(), GetItemName(x.Key), x.Sum(x => x.StackCount)))
            .OrderByDescending(x => {
                BasePickupItem item = x.Item;
                float price = item.Record<ItemRecord>()?.Price ?? 0f;
                return price * x.Count;       // The total value of all the items including stack.
            })
            .ThenBy(x => x.Name)
            .ToList();

        return groupedCount;
    }

    public static bool CellsEqual(CellPosition a, CellPosition b)
    {
        return a.X == b.X && a.Y == b.Y;
    }

    private static void RefreshMap(MinimapScreen __instance)
    {
        __instance._fogOfWar.RefreshMinimap(__instance._locationMetadata.ScanMonsters, __instance._locationMetadata.ScanItems, __instance._locationMetadata.ScanExit);
    }
}

