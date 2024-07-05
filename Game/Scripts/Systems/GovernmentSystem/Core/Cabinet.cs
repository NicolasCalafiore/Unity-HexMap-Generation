using UnityEngine;
using Terrain;
using System.Collections.Generic;
using System;
using Random = UnityEngine.Random;
using Cabinet;
using Players;
using Character;

namespace Cabinet {
    public class Cabinet {

        public Domestic domestic_advisor;
        public Foreign foreign_advisor;
        public Cabinet(){}
            
        // Starts the domestic turn for each advisor
        // This is where the advisors will identify construction opportunities and expansion opportunities
        public void StartDomesticTurn(Player player, List<List<float>> fog_of_war){
            domestic_advisor.IdentifyConstructionOpportunities(player);
            domestic_advisor.IdentifyExpansionOpportuntiies(fog_of_war, player);
            
        }

        // Starts the foreign turn for each advisor
        // This is where the advisors will scan for new players and generate starting relationships
        // with the new players
        public void StartForeignTurn(){

        }

    }
}