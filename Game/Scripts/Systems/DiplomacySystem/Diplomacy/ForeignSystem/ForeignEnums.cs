using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace Terrain
{
    public static class ForeignEnums
    { 
        public enum RelationshipLevel{
            Ally = 30,
            Friendly = 15,
            Neutral = 0,
            Unfriendly = -Friendly,
            Enemy = -Ally
        }
        
        public static RelationshipLevel GetRelationshipLevel(float relationship_value)
        {
            if(relationship_value > (float) RelationshipLevel.Friendly) return RelationshipLevel.Ally;
            if(relationship_value > (float) RelationshipLevel.Neutral) return RelationshipLevel.Friendly;
            if(relationship_value > (float) RelationshipLevel.Unfriendly) return RelationshipLevel.Neutral;
            if(relationship_value > (float) RelationshipLevel.Enemy) return RelationshipLevel.Unfriendly;
            else return RelationshipLevel.Enemy;
        }
    }
}