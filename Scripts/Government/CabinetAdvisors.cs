using UnityEngine;
using Terrain;
using Strategy.Assets.Scripts.Objects;
using System.Collections.Generic;
using System;
using Random = UnityEngine.Random;
using Cabinet;
using Players;

namespace Cabinet {
    public class CabinetAdvisors {

        public List<Domestic> domestic_advisor_list = new List<Domestic>();
        public List<Foreign> foreign_advisor_list = new List<Foreign>();
        


        public CabinetAdvisors(){
        }

        public void AddDomestic(Domestic domestic){
            domestic_advisor_list.Add(domestic);
        }

        public Domestic GetDomestic(int index){
            return domestic_advisor_list[index];
        }

        public void AddForeign(Foreign foreign){
            foreign_advisor_list.Add(foreign);
        }

        public Foreign GetForeign(int index){
            return foreign_advisor_list[index];
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

    }
}