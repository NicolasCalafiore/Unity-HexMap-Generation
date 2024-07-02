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
            Welcoming = 60,
            Friendly = 27,
            Neutral = 0,
            Unfriendly = -Friendly,
            Hostile = -Welcoming
        }
        
        public static RelationshipLevel GetRelationshipLevel(float relationship_value)
        {
            if(relationship_value >= (float)RelationshipLevel.Welcoming) return RelationshipLevel.Welcoming;
            else if(relationship_value >= (float)RelationshipLevel.Friendly) return RelationshipLevel.Friendly;
            else if(relationship_value >= (float)RelationshipLevel.Unfriendly) return RelationshipLevel.Neutral;
            else if(relationship_value >= (float)RelationshipLevel.Hostile) return RelationshipLevel.Unfriendly;
            else return RelationshipLevel.Hostile;
        }
    }
}