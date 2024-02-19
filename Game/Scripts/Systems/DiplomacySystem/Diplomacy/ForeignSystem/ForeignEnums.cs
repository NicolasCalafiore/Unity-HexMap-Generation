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
            Ally,
            Friendly,
            Neutral,
            Unfriendly,
            Enemy
        }
        

        public static RelationshipLevel GetRelationshipLevel(float relationship_value)
        {
            if(relationship_value >= 10) return RelationshipLevel.Ally;
            if(relationship_value >= 5) return RelationshipLevel.Friendly;
            if(relationship_value >= -5) return RelationshipLevel.Neutral;
            if(relationship_value >= -10) return RelationshipLevel.Unfriendly;
            else return RelationshipLevel.Enemy;
        }


    }
}