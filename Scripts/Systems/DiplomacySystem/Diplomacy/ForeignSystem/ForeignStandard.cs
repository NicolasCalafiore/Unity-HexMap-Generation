using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Players;
using Terrain;
using Unity.VisualScripting;
using UnityEngine;


namespace Diplomacy
{
    public class ForeignStandard : ForeignStrategy
    {
        public ForeignStandard(){
            SetStrategyWeights();
        }

        public override void SetStrategyValues(Player owner, Player foreigner){
        
        Vector2 owner_capital_coor = owner.cities[0].GetColRow();
        Vector2 foreigner_capital_coor = foreigner.cities[0].GetColRow();
        
        // NEED TO FIND DISTANCE OF CITY BY HEX AMOUNT (FLOOD FILL ALGOROTIHM) TO SET CAPITAL DISTANCE LEVEL. Continue with rest of attributes
        //public static bool FloodFillDistance( owner_capital_coor.x, int j, List<List<float>> map, int target, int depth = 0)
        
        
        }
    }
}
