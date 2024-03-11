using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Terrain;
using Unity.VisualScripting;
using UnityEngine;

namespace Terrain
{
    public static class PathFinding
    {

        public static List<HexTile> AStarPathFinding(HexTile start_tile, HexTile goal_tile)
        {
            

            return null;

        }

        public static int GetManhattanDistance(Vector2 pos_one, Vector2 pos_two){

            int block = 0;
            while(pos_one != pos_two){
                if(pos_one.x > pos_two.x && pos_one.y < pos_two.y){
                    pos_one.x--;
                    pos_one.y++;
                    block++;
                }
                else if(pos_one.x < pos_two.x && pos_one.y > pos_two.y){
                    pos_one.x++;
                    pos_one.y--;
                    block++;
                }
                else if(pos_one.x < pos_two.x){
                    pos_one.x++;
                    block++;
                }
                else if(pos_one.x > pos_two.x){
                    pos_one.x--;
                    block++;
                }
                else if(pos_one.y < pos_two.y){
                    pos_one.y++;
                    block++;
                }
                else if(pos_one.y > pos_two.y){
                    pos_one.y--;
                    block++;
                }
            }


            return block;
        }
            
    }
}