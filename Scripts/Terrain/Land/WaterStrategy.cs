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
        public abstract List<List<float>> GenerateWaterMap(Vector2 map_size, EnumHandler.HexRegion region_type, List<HexTile> hex_list);
        private int padding = 5;

        public void OceanBorder(List<List<float>> ocean_map){
            for(int i = 0; i < ocean_map.Count; i++){
                for(int j = 0; j < ocean_map[i].Count; j++){
                    if(i < padding || j > ocean_map[i].Count - padding || j < padding || i > ocean_map.Count - padding){
                        ocean_map[i][j] = (int) EnumHandler.LandType.Water;
                    }
                }
            }
        }


    }
}