using UnityEngine;
using Terrain;
using Strategy.Assets.Scripts.Objects;
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
        public Cabinet(){
        }

        public void StartDomesticTurn(List<List<float>> territory_map, int id, List<List<float>> fog_of_war){
            foreach(Domestic i in domestic_advisor_list){
                i.IdentifyConstructionOppruntities(territory_map, id);
                i.IdentifyExpansionOpportuntiies(fog_of_war, id, territory_map);
            }
        }

        public void StartForeignTurn(List<List<float>> territory_map, int id, List<List<float>> fog_of_war){
            foreach(Foreign i in foreign_advisor_list){
                i.ScanForNewPlayers(territory_map, fog_of_war, id);
                i.GenerateStartingRelationship(id);
      
            }
        }

        public void AddCharacter(ICharacter character){
            if(character is Domestic){
                domestic_advisor_list.Add((Domestic) character);
            }
            else if(character is Foreign){
                foreign_advisor_list.Add((Foreign) character);
            }
        }

        public Domestic GetDomestic(int index){
            return domestic_advisor_list[index];
        }

        public Foreign GetForeign(int index){
            return foreign_advisor_list[index];
        }

        public List<Domestic> GetDomesticList(){
            return domestic_advisor_list;
        }
        public List<Foreign> GetForeignList(){
            return foreign_advisor_list;
        }

    }
}