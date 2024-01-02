using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Strategy.Assets.Game.Scripts.Terrain.Regions;
using UnityEngine;


namespace Terrain
{
    public class ResourceRandom : ResourceStrategy
    {
        int iron_chance = 5;
        int cattle_chance = 3;
        int gems_chance = 1;
        int stone_chance = 3;
        int bananas_chance = 2;
        int incense_chance = 1;


        public override List<List<float>> GenerateResourceMap(Vector2 map_size, List<List<float>> ocean_map, List<List<float>> river_map, List<List<float>> regions_map, List<List<float>> features_map){

            List<List<float>> resource_map = TerrainUtils.GenerateMap(map_size);

            for(int i = 0; i < map_size.x; i++){
                for(int j = 0; j < map_size.y; j++){
                    if(regions_map[i][j] == (int) EnumHandler.HexRegion.Tundra && features_map[i][j] == (int) EnumHandler.HexNaturalFeature.None){
                        if(Random.Range(0, 100) < iron_chance) resource_map[i][j] = (int) EnumHandler.HexResource.Iron;
                    }
                    if(regions_map[i][j] == (int) EnumHandler.HexRegion.Grassland && features_map[i][j] == (int) EnumHandler.HexNaturalFeature.None){
                        if(Random.Range(0, 100) < cattle_chance) resource_map[i][j] = (int) EnumHandler.HexResource.Cattle;
                    }
                    if(regions_map[i][j] == (int) EnumHandler.HexRegion.Desert && features_map[i][j] == (int) EnumHandler.HexNaturalFeature.None){
                        if(Random.Range(0, 100) < gems_chance) resource_map[i][j] = (int) EnumHandler.HexResource.Gems;
                    }
                    if(regions_map[i][j] == (int) EnumHandler.HexRegion.Highlands && features_map[i][j] == (int) EnumHandler.HexNaturalFeature.None){
                        if(Random.Range(0, 100) < stone_chance) resource_map[i][j] = (int) EnumHandler.HexResource.Stone;
                    }
                    if(regions_map[i][j] == (int) EnumHandler.HexRegion.Jungle && (features_map[i][j] == (int) EnumHandler.HexNaturalFeature.None || features_map[i][j] == (int) EnumHandler.HexNaturalFeature.Jungle)){
                        if(Random.Range(0, 100) < bananas_chance){resource_map[i][j] = (int) EnumHandler.HexResource.Bananas; features_map[i][j] = (int) EnumHandler.HexNaturalFeature.None;}
                    }
                    if(regions_map[i][j] == (int) EnumHandler.HexRegion.Desert && features_map[i][j] == (int) EnumHandler.HexNaturalFeature.None){
                        if(Random.Range(0, 100) < incense_chance){resource_map[i][j] = (int) EnumHandler.HexResource.Incense;}
                    }


                }
            }

            return resource_map;

        }

    }
}