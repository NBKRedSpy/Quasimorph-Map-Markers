using MGSC;

namespace MapMarkers.Patches.POI;


/// <summary>
/// A group of items on the floor with the same ID.
/// Used to display the items and their count on the minimap hover.
/// </summary>
/// <param name="Item">The grouped BasePickupItem</param>
/// <param name="Name">The localized name of the item.</param>
/// <param name="Count">The total count of the Item, using the stack count</param>
internal record struct FloorItem(BasePickupItem Item, string Name, int Count);


