using UnityEngine;
using Terrain;
using Strategy.Assets.Scripts.Objects;
using System.Collections.Generic;
using System;
using Random = UnityEngine.Random;
using Cabinet;
using PlayerGovernment;
using Unity.VisualScripting;

namespace Players {
    public class Player {
        /*
            Player class is used to store information about the player
        */

        public string state_prefix;
        public string name;
        public Color team_color;
        public int id;
        public List<City> cities = new List<City>();
        public List<List<float>> fog_of_war_map;
        public EnumHandler.GovernmentType government_type;
        public Government government;
        public float wealth = 1000;
        public int knowledge_points = 0;
        public int heritage_points = 0;
        public int belief_points = 0;


        public Player(string state_prefix, string name, int id){
            this.state_prefix = state_prefix;
            this.name = name;


             this.team_color = new Color(Random.Range(0f, 1f),Random.Range(0f, 1f),Random.Range(0f, 1f));

            this.id = id;

            PlayerManager.player_id_to_player.Add(id, this);
        }

        public void GovernmentSimulation(MapGeneration map_generation){
            //List<List<float>> territory_map = TerrainUtils.GenerateMap(map_generation.GetMapSize(), 0);
            //DebugHandler.PrintMapDebug("Territory Map", territory_map);   //TO DO: REEVALUATE EFFECT

            List<List<float>> territory_map = map_generation.territory_map_handler.territory_map;
            government.cabinet.StartDomesticTurn(territory_map, id, fog_of_war_map);
            government.cabinet.StartForeignTurn(territory_map, id, fog_of_war_map);
        }

        public void AddCity(City city){
            cities.Add(city);
        }

        public void SetFogOfWarMap(List<List<float>> fog_of_war_map){
            this.fog_of_war_map = fog_of_war_map;
        }

        public void SetStatePrefix(string state_prefix){
            this.state_prefix = state_prefix;
        }

        public void SetStateName(string name){
            this.name = name;
        }

        public string GetOfficialName(){
            return state_prefix + " " + name;
        }

        internal void SetGovernmentType(EnumHandler.GovernmentType governmentType)
        {
            this.government_type = governmentType;
        }

        public void SetGovernment(Government government){
            this.government = government;
        }

        public Government GetGovernment(){
            return government;
        }

        public City GetCityByIndex(int index){
            return cities[index];
        }

    }
}