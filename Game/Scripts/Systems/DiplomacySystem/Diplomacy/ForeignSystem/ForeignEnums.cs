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
            Welcoming = 75,
            Friendly = 50,
            Neutral = 0,
            Unfriendly = -Friendly,
            Hostile = -Welcoming
        }
        
        public static RelationshipLevel GetRelationshipLevel(float relationship_value)
        {
            if(relationship_value >= (float)RelationshipLevel.Welcoming) return RelationshipLevel.Welcoming;
            if(relationship_value >= (float)RelationshipLevel.Friendly) return RelationshipLevel.Friendly;
            if(relationship_value >= (float)RelationshipLevel.Neutral) return RelationshipLevel.Neutral;
            if(relationship_value >= (float)RelationshipLevel.Unfriendly) return RelationshipLevel.Unfriendly;
            return RelationshipLevel.Hostile;
        }
    }
}