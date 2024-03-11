using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace Terrain
{
    public static class ForeignEnums
    { 
        private const int ALLY_MIN_VALUE = 10;
        private const int FRIENDLY_MIN_VALUE = 5;
        private const int NEUTRAL_MIN_VALUE = -5;
        private const int UNFRIENDLY_MIN_VALUE = -10;

        public enum RelationshipLevel{
            Ally,
            Friendly,
            Neutral,
            Unfriendly,
            Enemy
        }
        
        public static RelationshipLevel GetRelationshipLevel(float relationship_value)
        {
            if(relationship_value >= ALLY_MIN_VALUE) return RelationshipLevel.Ally;
            if(relationship_value >= FRIENDLY_MIN_VALUE) return RelationshipLevel.Friendly;
            if(relationship_value >= NEUTRAL_MIN_VALUE) return RelationshipLevel.Neutral;
            if(relationship_value >= UNFRIENDLY_MIN_VALUE) return RelationshipLevel.Unfriendly;
            else return RelationshipLevel.Enemy;
        }
    }
}