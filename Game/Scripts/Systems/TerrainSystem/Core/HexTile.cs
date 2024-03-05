using UnityEngine;
using Terrain;
using Strategy.Assets.Scripts.Objects;
using System;
using Players;
using System.Collections.Generic;
using Character;

namespace Terrain {
    public class HexTile {

        /*
            Used to store all HexTile properties
            Is wrap around any HexTile decorators
        */

        public static Dictionary<Vector2, HexTile> col_row_to_hex = new Dictionary<Vector2, HexTile>();
        private static List<HexTile> hex_list = new List<HexTile>();   // All HexTile objects
        private static DecoratorHandler hex_decorator = new DecoratorHandler();

        //***
        static readonly float WIDTH_MULTIPLIER = Mathf.Sqrt(3) / 2; // Used to calculate HexTile position
        public float nourishment { get; set; }

        public float construction { get; set; }

        public readonly int column;  //Column
        public readonly int row;  //Row
        private readonly int S;  // -(column + row)
        public float elevation; //Elevation


        private ElevationEnums.HexElevation elevation_type { get; set;}
        private RegionsEnums.HexRegion region_type;  //Region
        private LandEnums.LandType land_type; //Land or Water
        private FeaturesEnums.HexNaturalFeature feature_type; //Natural Feature
        private ResourceEnums.HexResource resource_type;  //Resource
        private StructureEnums.StructureType structure_type;   //Structures
        private Player owner_player;   
        private City owner_city;
        private bool is_coast = false;
        public virtual float MovementCost { get; set; } = 1.0f; // Default movement costs

        public HexTile(int column, int row)
        {
            this.column = column;
            this.row = row;
            this.S = -(column + row);
        }
        public HexTile SetElevation(ElevationEnums.HexElevation elevation_type)
        {
            this.elevation = (float) elevation_type / 100;
            this.elevation_type = elevation_type;
            return this;
        }

        public HexTile SetStructureType(StructureEnums.StructureType structure_type){
            this.structure_type = structure_type;
            return this;
        }

        public HexTile SetFeatureType(FeaturesEnums.HexNaturalFeature feature_type){
            this.feature_type = feature_type;
            return this;
        }

        public HexTile SetLandType(LandEnums.LandType land_type){
            this.land_type = land_type;
            return this;
        }
        public HexTile SetRegionType(RegionsEnums.HexRegion region_type){
            this.region_type = region_type;
            return this;
        }

        public HexTile SetResourceType(ResourceEnums.HexResource resource_type){
            this.resource_type = resource_type;
            return this;
        }

        public HexTile SetOwnerPlayer(Player owner_player){
            this.owner_player = owner_player;
            return this;
        }
        public HexTile SetOwnerCity(City owner_city){
            this.owner_city = owner_city;
            return this;
        }

        public Vector2 GetColRow()
        {
            return new Vector2(this.column, this.row);
        }

        public Vector3 GetPosition()
        {  
            float radius = 1f;
            float height = radius * 2;
            float width = WIDTH_MULTIPLIER * height;

            float vert = height * 0.75f;
            float horiz = width;

            return new Vector3(
                horiz * (this.column + this.row/2f),
                elevation,
                vert * this.row
            );
        }

        public void SetCoast(){
            is_coast = true;
            elevation = elevation < 0 ? 0 : elevation;
        }

        public bool IsCoast(){
            return is_coast;
        }

        public ElevationEnums.HexElevation GetElevationType(){
            return elevation_type;
        }

        public RegionsEnums.HexRegion GetRegionType(){
            return region_type;
        }

        public LandEnums.LandType GetLandType(){
            return land_type;
        }

        public FeaturesEnums.HexNaturalFeature GetFeatureType(){
            return feature_type;
        }

        public ResourceEnums.HexResource GetResourceType(){
            return resource_type;
        }

        public StructureEnums.StructureType GetStructureType(){
            return structure_type;
        }

        public Player GetOwnerPlayer(){
            return owner_player;
        }

        public City GetOwnerCity(){
            return owner_city;
        }



        public static void GenerateHexList(){
            List<HexTile> hex_list = new List<HexTile>();

            for(int column = 0; column < MapManager.GetMapSize().x; column++){
                for(int row = 0; row < MapManager.GetMapSize().y; row++){
                    
                    HexTile hex = HexTile.GenerateHex(
                        MapManager.terrain_map_handler.GetElevationMap()[column][row], 
                        MapManager.city_map_handler.structure_map[column][row], 
                        MapManager.terrain_map_handler.GetFeaturesMap()[column][row], 
                        MapManager.terrain_map_handler.GetWaterMap()[column][row],
                        MapManager.terrain_map_handler.GetRegionsMap()[column][row], 
                        MapManager.terrain_map_handler.GetResourceMap()[column][row], 
                        column, row);

                    hex_list.Add(hex);
                }
                
            }


            HexTile.hex_list = hex_list;
            
        }

        public static HexTile GenerateHex(float elevation_type, float structure_type, float feature_type, float land_type, float region_type, float resource_type, /* float owner_id, */ float col, float row){ 
            HexTile hex = new((int) col, (int) row);
            
            col_row_to_hex.Add(new Vector2(col, row), hex);
            ElevationEnums.HexElevation elevation_type_enum = LandEnums.GetLandType(land_type) == LandEnums.LandType.Water ? ElevationEnums.HexElevation.Flatland : ElevationEnums.GetElevationType(elevation_type);
            
            hex.SetElevation(elevation_type_enum)
                .SetStructureType(StructureEnums.GetStructureType(structure_type))
                .SetFeatureType(FeaturesEnums.GetNaturalFeatureType(feature_type))
                .SetLandType(LandEnums.GetLandType(land_type))
                .SetRegionType(RegionsEnums.GetRegionType(region_type))
                .SetResourceType(ResourceEnums.GetResourceType(resource_type));

            return hex;
        }


        public static void AddHexTileToCityTerritory(City city, Vector2 map_size) 
        {
            Vector2 city_col_row = city.GetColRow();

            List<HexTile> hex_list_to_add = HexTileUtils.CircularRetrieval((int) city_col_row.x, (int) city_col_row.y, map_size);
                
            foreach(HexTile hex in hex_list_to_add){
                city.GetHexTerritoryList().Add(hex);
                hex.SetOwnerCity(city);
            }
        }

        public static void AddHexTileToPlayerTerritory(List<HexTile> hex_list, Player player)   
        {
            foreach(HexTile hex in hex_list){
                if(hex.owner_player == null){
                    hex.SetOwnerPlayer(player);
                }
            }
        }

        public static List<HexTile> GetHexList(){
            return hex_list;
        }

        internal static void SetHexDecorators()
        {
            hex_decorator.SetHexDecorators(hex_list);
        }

    }
}