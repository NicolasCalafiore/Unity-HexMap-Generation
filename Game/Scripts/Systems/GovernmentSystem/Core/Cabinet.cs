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

        private List<Domestic> domestic_advisor_list = new List<Domestic>();
        private List<Foreign> foreign_advisor_list = new List<Foreign>();
        public Cabinet(){}
            
        // Starts the domestic turn for each advisor
        // This is where the advisors will identify construction opportunities and expansion opportunities
        public void StartDomesticTurn(Player player, List<List<float>> fog_of_war){
            foreach(Domestic i in domestic_advisor_list){
                i.IdentifyConstructionOpportunities(player);
                i.IdentifyExpansionOpportuntiies(fog_of_war, player);
            }
        }

        // Starts the foreign turn for each advisor
        // This is where the advisors will scan for new players and generate starting relationships
        // with the new players
        public void StartForeignTurn(){
            foreach(Foreign i in foreign_advisor_list){
                i.GenerateStartingRelationship(i.known_players);
            }
        }

        public Domestic GetDomestic(int index) => domestic_advisor_list[index];
        public Foreign GetForeign(int index) => foreign_advisor_list[index];
        public List<Domestic> GetDomesticList() => domestic_advisor_list;
        public List<Foreign> GetForeignList() => foreign_advisor_list;
        
    }
}