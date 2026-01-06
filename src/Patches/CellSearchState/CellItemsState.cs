namespace MapMarkers.Patches.CellSearchState
{
    /// <summary>
    /// The state of a cell's items for the searched/empty indicator.
    /// </summary>
    internal enum CellItemsState
    {
        Invalid = 0,

        /// <summary>
        /// Not searched yet.
        /// </summary>
        NotSearched,

        /// <summary>
        /// The item has been searched, and has items.
        /// </summary>
        SearchedNotEmpty,

        /// <summary>
        /// The storage item has no items.
        /// 
        /// This is either because an Obstical is visible and empty, or bodies or floor items have been searched 
        /// and found to be empty.
        /// </summary>
        Empty
    }
}
