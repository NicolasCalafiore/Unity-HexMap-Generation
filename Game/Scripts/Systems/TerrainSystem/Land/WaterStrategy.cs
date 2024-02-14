using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Terrain;
using UnityEngine;

namespace Strategy.Assets.Game.Scripts.Terrain.Water
{
    public abstract class WaterStrategy
    {
        /*
            WaterStrategy is used to generate water on the map - abstract class
        */
        public abstract List<List<float>> GenerateWaterMap(Vector2 map_size, RegionsEnums.HexRegion region_type);
        private int padding = 5;

        public void SetOceanBorder(List<List<float>> ocean_map){
            for(int i = 0; i < ocean_map.Count; i++){
                for(int j = 0; j < ocean_map[i].Count; j++){
                    if(i < padding || j > ocean_map[i].Count - padding || j < padding || i > ocean_map.Count - padding){
                        ocean_map[i][j] = (int) LandEnums.LandType.Water;
                    }
                }
            }
        }


    }
}