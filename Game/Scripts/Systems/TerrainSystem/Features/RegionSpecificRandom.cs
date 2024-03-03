using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;


namespace Terrain
{
    public class RegionSpecificRandom : FeaturesStrategy
    {
        /*
            RegionSpecificRandom is used to generate features on the map - concrete class
            Features are things like Forests/Oasis's - Natural Features
            This class generates features based on the region type
        */

        int forest_chance = 50;
        int oasis_chance = 2;
        int heavy_vegetation_chance = 35;
        int rock_chance = 15;
        int jungle_chance = 80;
        int swamp_chance = 75;
        public override List<List<float>> GenerateFeaturesMap(Vector2 map_size, List<List<float>> regions_map, List<List<float>> ocean_map) //Called from MapGeneration.cs
        {
            List<List<float>> features_map = MapUtils.GenerateMap();

            for(int i = 0; i < map_size.x; i++){
                for(int j = 0; j < map_size.y; j++){
                    
                    if(regions_map[i][j] == (int) RegionsEnums.HexRegion.Grassland){
                        if(Random.Range(0, 100) < forest_chance) features_map[i][j] = (int) FeaturesEnums.HexNaturalFeature.Forest;
                    }
                    else if(regions_map[i][j] == (int) RegionsEnums.HexRegion.Desert){
                        if(Random.Range(0, 100) < oasis_chance) features_map[i][j] = (int) FeaturesEnums.HexNaturalFeature.Oasis;
                    }
                    else if(regions_map[i][j] == (int) RegionsEnums.HexRegion.Plain){
                        if(Random.Range(0, 100) < heavy_vegetation_chance) features_map[i][j] = (int) FeaturesEnums.HexNaturalFeature.Heavy_Vegetation;
                    }
                    else if(regions_map[i][j] == (int) RegionsEnums.HexRegion.Highland || regions_map[i][j] == (int) RegionsEnums.HexRegion.Tundra){
                        if(Random.Range(0, 100) < rock_chance) features_map[i][j] = (int) FeaturesEnums.HexNaturalFeature.Rocks;
                    }
                    else if(regions_map[i][j] == (int) RegionsEnums.HexRegion.Jungle){
                       if(Random.Range(0, 100) < jungle_chance) features_map[i][j] = (int) FeaturesEnums.HexNaturalFeature.Jungle;
                    }
                    else if(regions_map[i][j] == (int) RegionsEnums.HexRegion.Swamp){
                       if(Random.Range(0, 100) < swamp_chance) features_map[i][j] = (int) FeaturesEnums.HexNaturalFeature.Swamp;
                    }
                }
            }
        
            return features_map;

        }
    }
}