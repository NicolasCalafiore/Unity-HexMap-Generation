using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cabinet;
using Players;
using Terrain;
using UnityEngine;


namespace Diplomacy
{
    public abstract class ForeignStrategy
    {
        public abstract float GenerateStartingRelationship(Player known_player, Player player);
        public abstract List<string> CalculationValues(Player known_player, Player player);

        public enum RelationshipLevel{
            Ally = 10,
            Friendly = 5,
            Neutral = 0,
            Unfriendly = -5,
            Enemy = -10
        }
    }
}

