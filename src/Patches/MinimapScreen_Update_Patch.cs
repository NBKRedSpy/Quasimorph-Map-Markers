using HarmonyLib;
using MGSC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

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

                CellPosition cursorCell = GetCellUnderCursor(__instance);

                //Out of bounds.
                if ((cursorCell.X < 0 || cursorCell.Y > 0 || cursorCell.X < __instance._mapGrid.MaxWidth 
                    || cursorCell.Y < __instance._mapGrid.MaxHeight))
                {
                    if (locations.CurrentDungeonLevelPois.Contains(cursorCell))
                    {
                        locations.CurrentDungeonLevelPois.Remove(cursorCell);
                    }
                    else
                    {
                        locations.CurrentDungeonLevelPois.Add(cursorCell);
                    }

                    locations.Save();

                    __instance._fogOfWar.RefreshMinimap(__instance._locationMetadata.ScanMonsters, __instance._locationMetadata.ScanItems, __instance._locationMetadata.ScanExit);
                }
            }

            if (Input.GetKeyDown(Plugin.Config.ClearLocationsKey))
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
                    throw new ApplicationException("POI locations are not loaded and cannot be updated");
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

        //Only run if there is a marker on the floor.
        if (!Plugin.CurrentSavePoiStorage.CurrentDungeonLevelPois.Any(x => CellsEqual(x, cursorCell))) return;

        StringBuilder sb = new StringBuilder();
        
        List<(BasePickupItem Item, int Count)> groupedCount = GetAllCellItems(__instance, cursorCell);

        //COPY WARNING:  MGSC.MinimapScreen.RefreshLabelUnderCursor(MGSC.MapCell). This is a modified copy a copy
        //  of the first loop in the  function.
        //Not doing an early return to keep the code similar to the source.
        //ItemOnFloor itemOnFloor = __instance._itemsOnFloor.Get(cursorCell.X, cursorCell.Y);
        if (groupedCount.Count > 0)
        {
            foreach (var item in groupedCount)
            {
                string countText = item.Count == 1 ? "" : $"{item.Count}x ";

                string text = Localization.Get($"item.{item.Item.Id}.name").FirstLetterToUpperCase();
                sb.Append($"{countText}{text}, ");
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
    /// Returns all of the base items for a cell.
    /// All corpses, storages, and items on the floor.
    /// </summary>
    /// <param name="__instance"></param>
    /// <param name="cursorCell"></param>
    /// <param name="obstacles"></param>
    /// <returns></returns>
    private static List<(BasePickupItem Item, int Count)> GetAllCellItems(MinimapScreen __instance, 
        CellPosition cursorCell)
    {

        List<MapObstacle> obstacles = __instance._mapObstacles.GetAll(cursorCell.X, cursorCell.Y, false, false);

        List<BasePickupItem> items;
        items = new List<BasePickupItem>();

        IEnumerable<BasePickupItem> storageItems;

        //Corpses
        storageItems = obstacles
            .Where(x => x.CorpseStorage != null)
            .SelectMany(x =>
                x.CorpseStorage.CreatureData.Inventory
                    .AllContainers.SelectMany(x => x.Items));

        if (storageItems != null) items.AddRange(storageItems);

        //Item storage.  May be multiple
        storageItems = obstacles.SelectMany(x =>
            x.Components
                .Where(x => x is Store)
                .Cast<Store>()
                .SelectMany(x => x.storage.Items));

        if (storageItems != null) items.AddRange(storageItems);

        //check for floor items
        storageItems = __instance._itemsOnFloor.Get(cursorCell.X, cursorCell.Y)
            ?.Storage.Items;

        if (storageItems != null) items.AddRange(storageItems);

        //Group by count
        List<(BasePickupItem Item, int Count)> groupedCount = items
            .GroupBy(x => x.Id)
            .OrderBy(x => x.Count())     //Order by count so common items are last.
            .ThenBy(x => x.Key)
            .Select(x => (Item: x.First(), Count: x.Count()))
            .ToList(); ;

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

