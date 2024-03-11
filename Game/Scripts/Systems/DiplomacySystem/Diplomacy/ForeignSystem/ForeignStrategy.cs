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

    }
}

