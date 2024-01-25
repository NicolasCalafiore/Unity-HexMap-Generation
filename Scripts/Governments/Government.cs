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

        public Government(EnumHandler.GovernmentType government_type){
            this.government_type = government_type;
        }

        public void SetLeader(Leader leader){
            this.leader = leader;
        }

        public Leader GetLeader(){
            return leader;
        }





    }
}