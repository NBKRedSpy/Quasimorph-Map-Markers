using MGSC;
using Newtonsoft.Json;
using MapMarkers.Utility;
using System;
using System.Collections.Generic;
using System.IO;

namespace MapMarkers
{

    /// <summary>
    /// The POI coordinates for a floor.
    /// </summary>
    internal class PoiLocations 
    {
        [JsonIgnore]
        private Logger Logger { get; set; }

        /// <summary>
        /// The game's slot number.  Used to create the file name for each save.
        /// </summary>
        public int SaveSlotNumber { get; set; }

        [JsonIgnore]
        /// <summary>
        /// The full path to the location to save to.
        /// </summary>
        private string FilePath { get; set; }

        /// <summary>
        /// The list of POI locations per dungeon level
        /// </summary>
        public Dictionary<string, List<CellPosition>> Locations { get; set; } = [];

        [JsonIgnore]
        /// <summary>
        /// The current dungeon level cache.  Each level has its own POI list.
        /// </summary>
        public string CurrentDungeonLevelId { get; set; } = "";


        /// <summary>
        /// Cache for the POI locations on the current dungeon level.
        /// </summary>
        [JsonIgnore]
        public List<CellPosition> CurrentDungeonLevelPois{ get; set; }

        private static readonly JsonSerializerSettings SerializerSettings = new JsonSerializerSettings()
        {
            Formatting = Formatting.Indented,
        };

        /// <summary>
        /// Used for deserialization.  Must call Init() after hydration.
        /// </summary>
        public PoiLocations()
        {
                
        }

        public PoiLocations(string persistencePath, int saveSlotNumber, Logger logger)
        {
            //Call the base with a blank value.  Init will set the full path to the config file.
            Init(persistencePath, logger, saveSlotNumber);
        }

        /// <summary>
        /// Sets the current Dungeon Level.  Creates or loads the POI locations for the level.
        /// </summary>
        /// <param name="locationUniqueId"></param>
        public void SetDungeonLevel(string locationUniqueId)
        {
            List<CellPosition> cellPositions = null;

            if (Locations.TryGetValue(locationUniqueId, out cellPositions))
            {
                CurrentDungeonLevelPois = cellPositions;
            }
            else
            {
                CurrentDungeonLevelPois = new List<CellPosition>();
                Locations[locationUniqueId] = CurrentDungeonLevelPois;
            }

            CurrentDungeonLevelId = locationUniqueId;
        }

        /// <summary>
        /// Must be called after JSON deserialization.
        /// </summary>
        /// <param name="persistenceDir">The path to the persistence directory.  Used to set the file name.</param>
        public void Init(string persistenceDir, Logger logger, int slotNumber)
        {
            Logger = logger;
            SaveSlotNumber = slotNumber;
            FilePath  = GetFilePath(persistenceDir, slotNumber);
        }

        private static string GetFilePath(string persistenceDir, int slotNumber)
        {
            return Path.Combine(persistenceDir, $"PoI_Slot_{slotNumber}.json");
        }


        public void Save()
        {
            if(string.IsNullOrEmpty(this.FilePath))
            {
                throw new ArgumentException("The File path to the POI Locations must be set");
            }

            try
            {
                string json = JsonConvert.SerializeObject(this, SerializerSettings);
                File.WriteAllText(FilePath, json);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
            }
        }


        /// <summary>
        /// Returns the existing save for the slot number, or will create and save a new one.
        /// </summary>
        /// <param name="persistencePath">The mod's persistence folder.</param>
        /// <param name="saveSlotNumber">The slot number for the current save.</param>
        /// <param name="logger"></param>
        /// <returns></returns>
        public static PoiLocations CreateOrLoad(string persistencePath, int saveSlotNumber, Logger logger)
        {
            PoiLocations location = new PoiLocations(persistencePath, saveSlotNumber, logger);

            if (!File.Exists(location.FilePath))
            {
                location.Save();
                return location;
            }

            string json = File.ReadAllText(location.FilePath);  
            location = JsonConvert.DeserializeObject<PoiLocations>(json, SerializerSettings);
            location.Init(persistencePath, logger, saveSlotNumber);

            return location;
        }

        internal void Delete() 
        {
            if (File.Exists(FilePath))
            {
                File.Delete(this.FilePath);
            }

            Locations = new Dictionary<string, List<CellPosition>>();
            CurrentDungeonLevelPois = null;
            CurrentDungeonLevelId = "";
        }

        internal static void Delete(string persistenceDir, int slotNumber)
        {
            string fileName = GetFilePath(persistenceDir, slotNumber);
            if (fileName != null)
            {
                File.Delete(fileName);
            }
        }


    }
}
