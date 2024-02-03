using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Players;
using Strategy.Assets.Game.Scripts.Terrain;
using Strategy.Assets.Scripts.Objects;
using Terrain;
using TMPro;
using UnityEngine;


namespace Cabinet{
    public class Leader : ICharacter
    {
        public Leader(List<string> names, EnumHandler.CharacterGender gender){
            this.first_name = names[0];
            this.last_name = names[1];
            this.gender = gender;
            this.character_type = EnumHandler.CharacterType.Leader;

        }

    }
    
}