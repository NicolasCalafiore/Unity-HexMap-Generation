using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;


namespace Terrain
{
    public class RegionSpecificRandom : FeaturesStrategy
    {
        int forest_chance = 50;
        int oasis_chance = 10;
        public override List<List<float>> GenerateFeaturesMap(Vector2 map_size, List<List<float>> regions_map, List<List<float>> ocean_map)
        {
            List<List<float>> features_map = TerrainUtils.GenerateMap(map_size);

            for(int i = 0; i < map_size.x; i++){
                for(int j = 0; j < map_size.y; j++){

                    
                    if(regions_map[i][j] == (int) EnumHandler.HexRegion.Grassland){
                        if(Random.Range(0, 100) < forest_chance) features_map[i][j] = (int) EnumHandler.HexFeatures.Forest;
                    }
                    if(regions_map[i][j] == (int) EnumHandler.HexRegion.Desert){
                        if(Random.Range(0, 100) < oasis_chance) features_map[i][j] = (int) EnumHandler.HexFeatures.Oasis;
                    }

                }
            }
        
            return features_map;

        }
    }
}