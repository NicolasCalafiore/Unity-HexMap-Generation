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
        public TraitBase(string name, string description, int value)
        {
            this.name = name;
            this.description = description;
            this.value = value;
        }

        public string name {get; set;}
        public string description {get; set;}
        public int value {get; set;}
    }
}