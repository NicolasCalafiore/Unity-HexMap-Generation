using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Character;
using Players;
using Strategy.Assets.Game.Scripts.Terrain;
using Strategy.Assets.Scripts.Objects;
using Terrain;
using TMPro;
using UnityEngine;


namespace Cabinet{
    public class Leader : ICharacter
    {
        public Leader(List<string> names, CharacterEnums.CharacterGender gender, Player player, List<string> titles){
            this.first_name = names[0];
            this.last_name = names[1];
            this.gender = gender;
            this.character_type = CharacterEnums.CharacterType.Leader;
            this.owner_player = player;         
            this.title = titles[UnityEngine.Random.Range(0, titles.Count)];
        }

    }
    
}