using UnityEngine;
using Terrain;
using System.Collections.Generic;
using System;
using Random = UnityEngine.Random;
using Cabinet;
using PlayerGovernment;
using Unity.VisualScripting;
using Players;

namespace Character {
    public abstract class TraitBase {
        public abstract string Name { get;}
        public TraitBase(string description, int value)
        {
            this.description = description;
            this.value = value;
        }

        public string description {get; set;}
        public int value {get; set;}
        public List<string> banned_traits = new List<string>();


    }
}