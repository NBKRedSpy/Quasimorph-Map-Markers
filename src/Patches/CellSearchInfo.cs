using MGSC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapMarkers.Patches
{
    /// <summary>
    /// A map for the search data for all cells.
    /// </summary>
    internal class CellSearchInfo
    {
        public Dictionary<Position, CellItemsState> CellStates = new();

        /// <summary>
        /// Sets the cell, using the rules for merging states.
        /// </summary>
        /// <param name="position">The position of the cell.</param>
        /// <param name="state">The cell's desired state.</param>
        public void SetCellState(CellPosition cellPosition, CellItemsState state)
        {

            Position position = new(cellPosition);

            CellItemsState currentState;

            if(!CellStates.TryGetValue(position, out currentState))
            {
                CellStates[position] = state;
                return; 
            }

            switch (currentState)
            {
                case CellItemsState.NotSearched:
                    //If anything was not searched on the tile, keep it as not searched.
                    return;
                //If anything was searched and not empty, then keep it as searched not empty.
                case CellItemsState.SearchedNotEmpty:
                    // keep as SearchedNotEmpty
                    return;
                case CellItemsState.Empty:
                default:
                    CellStates[position] = state;
                    return;
            }   
        }

        public CellItemsState GetCellState(Position position)
        {
            CellStates.TryGetValue(position, out CellItemsState state);
            return state;
        }
    }
}
