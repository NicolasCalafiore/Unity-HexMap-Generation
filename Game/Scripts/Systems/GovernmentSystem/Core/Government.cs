using UnityEngine;
using Terrain;
using Strategy.Assets.Scripts.Objects;
using System.Collections.Generic;
using System;
using Random = UnityEngine.Random;
using Cabinet;
using Character;

namespace PlayerGovernment {
    public class Government {
        
        Leader leader;
        private GovernmentEnums.GovernmentType government_type;
        public Cabinet.Cabinet cabinet;

        public Government(GovernmentEnums.GovernmentType government_type){
            this.government_type = government_type;
            cabinet = new Cabinet.Cabinet();
        }

        public void AddCharacter(ICharacter character){


            switch(character){
                case Leader leader:
                    this.leader = leader;
                    break;
                case Domestic domestic:
                    cabinet.GetDomesticList().Add(domestic);
                    break;
                case Foreign foreign:
                    cabinet.GetForeignList().Add(foreign);
                    break;
            }
        }

        public Leader GetLeader(){
            return leader;
        }

        public Foreign GetForeign(int index){
            return cabinet.GetForeign(index);
        }

        public Domestic GetDomestic(int index){
            return cabinet.GetDomestic(index);
        }

        public GovernmentEnums.GovernmentType GetGovernment(){
            return government_type;
        }

    }
}