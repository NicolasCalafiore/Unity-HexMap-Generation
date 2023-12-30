using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;


namespace Terrain
{
    public class RegionSpecificRandom : FeaturesStrategy
    {
        int forest_chance = 50;
        int oasis_chance = 2;
        int wheat_field_chance = 35;
        int rock_chance = 15;
        int jungle_chance = 80;
        int swamp_chance = 75;
        public override List<List<float>> GenerateFeaturesMap(Vector2 map_size, List<List<float>> regions_map, List<List<float>> ocean_map)
        {
            List<List<float>> features_map = TerrainUtils.GenerateMap(map_size);

            for(int i = 0; i < map_size.x; i++){
                for(int j = 0; j < map_size.y; j++){
                    
                    if(regions_map[i][j] == (int) EnumHandler.HexRegion.Grassland){
                        if(Random.Range(0, 100) < forest_chance) features_map[i][j] = (int) EnumHandler.HexNaturalFeature.Forest;
                    }
                    else if(regions_map[i][j] == (int) EnumHandler.HexRegion.Desert){
                        if(Random.Range(0, 100) < oasis_chance) features_map[i][j] = (int) EnumHandler.HexNaturalFeature.Oasis;
                    }
                    else if(regions_map[i][j] == (int) EnumHandler.HexRegion.Plains){
                        if(Random.Range(0, 100) < wheat_field_chance) features_map[i][j] = (int) EnumHandler.HexNaturalFeature.WheatField;
                    }
                    else if(regions_map[i][j] == (int) EnumHandler.HexRegion.Highlands || regions_map[i][j] == (int) EnumHandler.HexRegion.Tundra){
                        if(Random.Range(0, 100) < rock_chance) features_map[i][j] = (int) EnumHandler.HexNaturalFeature.Rocks;
                    }
                    else if(regions_map[i][j] == (int) EnumHandler.HexRegion.Jungle){
                       if(Random.Range(0, 100) < jungle_chance) features_map[i][j] = (int) EnumHandler.HexNaturalFeature.Jungle;
                    }
                    else if(regions_map[i][j] == (int) EnumHandler.HexRegion.Swamp){
                       if(Random.Range(0, 100) < swamp_chance) features_map[i][j] = (int) EnumHandler.HexNaturalFeature.Swamp;
                    }
                }
            }
        
            return features_map;

        }
    }
}