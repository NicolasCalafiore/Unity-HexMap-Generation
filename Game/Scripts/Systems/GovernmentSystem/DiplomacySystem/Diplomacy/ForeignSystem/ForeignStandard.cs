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
        
        Vector2 owner_capital_coor = owner.GetCities()[0].GetColRow();
        Vector2 foreigner_capital_coor = foreigner.GetCities()[0].GetColRow();

        float distance = Vector2.Distance(owner_capital_coor, foreigner_capital_coor);

        HexTile owner_hex_tile = HexTile.col_row_to_hex[owner.GetCities()[0].GetColRow()];
        HexTile foreigner_hex_tile = HexTile.col_row_to_hex[foreigner.GetCities()[0].GetColRow()];

        bool similiar_region = owner_hex_tile.GetRegionType() == foreigner_hex_tile.GetRegionType();
        bool similiar_government = owner.GetGovernmentType() == foreigner.GetGovernmentType();

        if(distance < 2){
            capital_distance_level = 1;
        }

        else if(distance < 4){
            capital_distance_level = .8f;
        }

        else if(distance < 5){
            capital_distance_level = .6f;
        }
        else if(distance < 6){
            capital_distance_level = .4f;
        }
        else if(distance < 7){
            capital_distance_level = .2f;
        }
        else{
            capital_distance_level = 0f;
        }


        if(similiar_government){
            similiar_government_level = 1f;
        }
        else{
            similiar_government_level = 0f;
        }

        if(similiar_region){
            similiar_region_level = 1f;
        }
        else{
            similiar_region_level = 0f;
        }
        
        }
    }
}
