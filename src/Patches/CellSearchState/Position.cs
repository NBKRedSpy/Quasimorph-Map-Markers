using MGSC;

namespace MapMarkers.Patches.CellSearchState
{
        /// <summary>
        /// A cell position.  Used as a key for a location lookup since CellPosition does not 
        /// provide a compare.
        /// </summary>
        /// <param name="X"></param>
        /// <param name="Y"></param>
        internal record Position(int X, int Y)
        {
            public Position(CellPosition cellPosition) : this(cellPosition.X, cellPosition.Y) {}
        }
}
