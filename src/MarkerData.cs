using MGSC;
using UnityEngine;

namespace MapMarkers
{
    /// <summary>
    /// Represents a marker with its position and color.
    /// </summary>
    public class MarkerData
    {
        public CellPosition Position { get; set; }
        public Color32 Color { get; set; }

        public MarkerData()
        {
        }

        public MarkerData(CellPosition position, Color32 color)
        {
            Position = position;
            Color = color;
        }
    }
}