using MGSC;

namespace MapMarkers.Patches;


internal static partial class MinimapScreen_Update_Patch
{
    /// <summary>
    /// A group of items on the floor with the same ID.
    /// </summary>
    /// <param name="Item">The grouped BasePickupItem</param>
    /// <param name="Name">The localized name of the item.</param>
    /// <param name="Count">The total count of the Item, using the stack count</param>
    internal record struct FloorItem(BasePickupItem Item, string Name, int Count);
}

