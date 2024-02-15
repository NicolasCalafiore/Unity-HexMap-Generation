using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Strategy.Assets.Game.Scripts.Terrain.Regions;
using UnityEngine;


namespace Terrain
{
    public class ResourceRandom : ResourceStrategy
    {
        /*
            ResourceRandom is used to generate resources randomly on the map, respective to regions.
        */

        int iron_chance = 5;
        int cattle_chance = 3;
        int gems_chance = 1;
        int stone_chance = 3;
        int bananas_chance = 2;
        int incense_chance = 1;
        int wheat_chance = 10;
        int pig_chance  = 2;
        int citrus_chance = 5;
        int foxes_chance = 2;
        int gold_chance = 1;
        int grapes_chance = 2;
        int rice_chance = 5;
        int salt_chance = 5;
        int horse_chance = 2;


        public override List<List<float>> GenerateResourceMap(Vector2 map_size, List<List<float>> ocean_map, List<List<float>> river_map, List<List<float>> regions_map, List<List<float>> features_map){

            List<List<float>> resource_map = TerrainUtils.GenerateMap();

            for(int i = 0; i < map_size.x; i++){
                for(int j = 0; j < map_size.y; j++){
                    if(regions_map[i][j] == (int) RegionsEnums.HexRegion.Tundra && features_map[i][j] == (int) FeaturesEnums.HexNaturalFeature.None){
                        if(Random.Range(0, 100) < iron_chance) resource_map[i][j] = (int) ResourceEnums.HexResource.Iron;
                        if(Random.Range(0, 100) < foxes_chance) resource_map[i][j] = (int) ResourceEnums.HexResource.Foxes;
                        if(Random.Range(0, 100) < horse_chance) resource_map[i][j] = (int) ResourceEnums.HexResource.Horses;
                    }
                    if(regions_map[i][j] == (int) RegionsEnums.HexRegion.Grassland && features_map[i][j] == (int) FeaturesEnums.HexNaturalFeature.None){
                        if(Random.Range(0, 100) < cattle_chance) resource_map[i][j] = (int) ResourceEnums.HexResource.Cattle;
                        if(Random.Range(0, 100) < pig_chance) resource_map[i][j] = (int) ResourceEnums.HexResource.Pigs;
                        if(Random.Range(0, 100) < citrus_chance) resource_map[i][j] = (int) ResourceEnums.HexResource.Citrus;
                        if(Random.Range(0, 100) < grapes_chance) resource_map[i][j] = (int) ResourceEnums.HexResource.Grapes;
                        if(Random.Range(0, 100) < horse_chance) resource_map[i][j] = (int) ResourceEnums.HexResource.Horses;
                    }
                    if(regions_map[i][j] == (int) RegionsEnums.HexRegion.Desert && features_map[i][j] == (int) FeaturesEnums.HexNaturalFeature.None){
                        if(Random.Range(0, 100) < gems_chance) resource_map[i][j] = (int) ResourceEnums.HexResource.Gems;
                        if(Random.Range(0, 100) < salt_chance) resource_map[i][j] = (int) ResourceEnums.HexResource.Salt;
                        if(Random.Range(0, 100) < gold_chance) resource_map[i][j] = (int) ResourceEnums.HexResource.Gold;
                        if(Random.Range(0, 100) < incense_chance){resource_map[i][j] = (int) ResourceEnums.HexResource.Spices;}
                    }
                    if(regions_map[i][j] == (int) RegionsEnums.HexRegion.Highland && features_map[i][j] == (int) FeaturesEnums.HexNaturalFeature.None){
                        if(Random.Range(0, 100) < stone_chance) resource_map[i][j] = (int) ResourceEnums.HexResource.Stone;
                        if(Random.Range(0, 100) < gold_chance) resource_map[i][j] = (int) ResourceEnums.HexResource.Gold;
                        if(Random.Range(0, 100) < salt_chance) resource_map[i][j] = (int) ResourceEnums.HexResource.Salt;
                        if(Random.Range(0, 100) < horse_chance) resource_map[i][j] = (int) ResourceEnums.HexResource.Horses;
                    }
                    if(regions_map[i][j] == (int) RegionsEnums.HexRegion.Jungle){
                        if(Random.Range(0, 100) < bananas_chance){resource_map[i][j] = (int) ResourceEnums.HexResource.Bananas; features_map[i][j] = (int) FeaturesEnums.HexNaturalFeature.None;}
                        if(Random.Range(0, 100) < rice_chance) resource_map[i][j] = (int) ResourceEnums.HexResource.Rice;
                        if(Random.Range(0, 100) < incense_chance) resource_map[i][j] = (int) ResourceEnums.HexResource.Spices;
                    }
                    if(regions_map[i][j] == (int) RegionsEnums.HexRegion.Plain){
                        if(Random.Range(0, 100) < wheat_chance){resource_map[i][j] = (int) ResourceEnums.HexResource.Wheat;}
                        if(Random.Range(0, 100) < horse_chance) resource_map[i][j] = (int) ResourceEnums.HexResource.Horses;
                    }
                    if(regions_map[i][j] == (int) RegionsEnums.HexRegion.Swamp){
                        if(Random.Range(0, 100) < rice_chance){resource_map[i][j] = (int) ResourceEnums.HexResource.Rice;}
                        if(Random.Range(0, 100) < incense_chance) resource_map[i][j] = (int) ResourceEnums.HexResource.Spices;
                    }


                }
            }

            return resource_map;

        }

    }
}