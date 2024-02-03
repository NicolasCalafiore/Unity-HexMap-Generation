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
        private EnumHandler.GovernmentType government_type;
        public CabinetAdvisors cabinet;


        public Government(EnumHandler.GovernmentType government_type){
            this.government_type = government_type;
            cabinet = new CabinetAdvisors();
        }

        public void SetLeader(Leader leader){
            this.leader = leader;
        }

        public Leader GetLeader(){
            return leader;
        }

        public void AddDomestic(Domestic domestic){
            cabinet.AddDomestic(domestic);
        }

        public void AddForeign(Foreign foreign){
            cabinet.AddForeign(foreign);
        }

        public Foreign GetForeign(int index){
            return cabinet.GetForeign(index);
        }

        public Domestic GetDomestic(int index){
            return cabinet.GetDomestic(index);
        }

    }
}