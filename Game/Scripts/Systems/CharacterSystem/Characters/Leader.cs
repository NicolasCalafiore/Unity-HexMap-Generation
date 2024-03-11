using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Character;
using Players;
using Strategy.Assets.Game.Scripts.Terrain;
using Cities;
using Terrain;
using TMPro;
using UnityEngine;


namespace Cabinet{
    public class Leader : AbstractCharacter
    {
        public Leader(List<string> names, CharacterEnums.CharacterGender gender, Player player, List<string> titles)
        : base(names, gender, player, titles){}
        
    }
    
}