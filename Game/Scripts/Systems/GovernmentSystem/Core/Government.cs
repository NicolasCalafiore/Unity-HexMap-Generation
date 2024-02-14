using UnityEngine;
using Terrain;
using Strategy.Assets.Scripts.Objects;
using System.Collections.Generic;
using System;
using Random = UnityEngine.Random;
using Cabinet;

namespace PlayerGovernment {
    public class Government {
        
        Leader leader;
        private GovernmentEnums.GovernmentType government_type;
        public Cabinet.Cabinet cabinet;

        public Government(GovernmentEnums.GovernmentType government_type){
            this.government_type = government_type;
            cabinet = new Cabinet.Cabinet();
        }

        public void SetLeader(Leader leader){
            this.leader = leader;
        }

        public void AddDomestic(Domestic domestic){
            cabinet.AddCharacter(domestic);
        }

        public void AddForeign(Foreign foreign){
            cabinet.AddCharacter(foreign);
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