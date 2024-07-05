using UnityEngine;
using Terrain;
using Cities;
using System.Collections.Generic;
using System;
using Random = UnityEngine.Random;
using Cabinet;
using Character;
using static Terrain.GovernmentEnums;
using System.Linq;

namespace PlayerGovernment {
    public class Government {
        
        public Leader leader {get; set;}
        public GovernmentType government_type {get; set;}
        public Cabinet.Cabinet cabinet {get; set;}

        public Government(GovernmentType government_type){
            this.government_type = government_type;
            cabinet = new Cabinet.Cabinet();
        }

        // Adds a character to the government
        // The character is added to the appropriate list based on the type of character
        // Dictionary is used to store the character type and the action to add the character to the appropriate list
        public void AddCharacter(AbstractCharacter character){
            Dictionary<Type, Action<AbstractCharacter>> characterAddActions = new Dictionary<Type, Action<AbstractCharacter>>
            {
                { typeof(Leader), character => leader = (Leader) character },
                { typeof(Domestic), character => cabinet.domestic_advisor = (Domestic) character},
                { typeof(Foreign), character => cabinet.foreign_advisor = (Foreign) character},
            };

            characterAddActions[character.GetType()](character);
        }

        public List<AbstractCharacter> GetCharacterList() => new List<AbstractCharacter> {leader, cabinet.foreign_advisor, cabinet.domestic_advisor};
    }
}