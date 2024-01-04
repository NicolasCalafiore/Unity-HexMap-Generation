using UnityEngine;
using Terrain;

namespace Players {
    public class Player {

        private string state_prefix;
        private string name;
        private Color color;
        private int id;
        public Vector2 capital_position;

        public Player(string state_prefix, string name, Color color, int id){
            this.state_prefix = state_prefix;
            this.name = name;
            this.color = color;
            this.id = id;
            
        }

    }
}