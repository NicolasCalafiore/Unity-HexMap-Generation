using UnityEngine;
using Terrain;
using System.Collections.Generic;
using System;
using Random = UnityEngine.Random;
using Cabinet;
using PlayerGovernment;
using Unity.VisualScripting;
using Character;
using static Terrain.GovernmentEnums;
using Cities;
using System.Linq;
using static Terrain.RegionsEnums;
using static Terrain.PriorityEnums;


namespace Players {
    public class Player {

        public string state_prefix { get; set; } = "";
        public string state_suffix { get; set; } = "";
        public string name  { get; set; }
        public int id  { get; set; }
        public float wealth { get; set; }
        public int knowledge_level { get; set; }
        public int heritage_points { get; set; }
        public int belief_level { get; set; }
        public Color team_color  { get; set; }
        public GovernmentType government_type { get; set; }
        public Government government  { get; set; }
        public HexRegion home_region { get; set; }
        public PlayerPriority priority { get; set; }
        private List<City> cities = new List<City>();
        private List<List<float>> fog_of_war_map;
        public int stability_critical_point = 40;       // TO DO: CHANGE DEPENDING ON TRAITS/GOVERNMENT ETC.
        public int nourishment_critical_point = 25;     // TO DO: CHANGE DEPENDING ON TRAITS/GOVERNMENT ETC.
        public int production_critical_point = 20;      // TO DO: CHANGE DEPENDING ON TRAITS/GOVERNMENT ETC.




        public Player(string name, int id){
            this.name = name;
            this.team_color = new Color(Random.Range(0f, 1f),Random.Range(0f, 1f),Random.Range(0f, 1f));
            this.id = id;
            PlayerManager.player_id_to_player.Add(id, this);

            this.wealth = UnityEngine.Random.Range(0, 2000);
            this.knowledge_level = UnityEngine.Random.Range(0, 5);
            this.heritage_points = UnityEngine.Random.Range(0, 5);
            this.belief_level = UnityEngine.Random.Range(0, 5);
        }

        public void SimulateGovernment(){
            government.cabinet.StartDomesticTurn(this, fog_of_war_map);
            government.cabinet.StartForeignTurn();
        }


        public void AddCity(City city) => cities.Add(city);
        public City GetCapital() => cities[0];
        public string GetOfficialName() => $"{state_prefix}{name}{state_suffix}";
        public City GetCityByIndex(int index) => cities[index];
        public Vector2 GetCapitalCoordinate()=> cities[0].col_row;
        public List<City> GetCities() => cities;
        public List<List<float>> GetFogOfWarMap() => fog_of_war_map;
        public void SetFogOfWarMap(List<List<float>> fog_of_war_map) => this.fog_of_war_map = fog_of_war_map;
        internal Player GetRandomKnownPlayerNullable() => government.cabinet.foreign_advisor.GetRandomKnownPlayerNullable();
        public List<Player> GetKnownPlayers() => government.cabinet.foreign_advisor.known_players;
        public int GetStability(){
            int stability = 0;
            foreach(City city in cities){
                stability += (int)city.stability;
            }
            return stability/cities.Count;
        }

        public int GetNutrition(){
            int nutrition = 0;
            foreach(City city in cities){
                nutrition += (int)city.nourishment;
            }
            return nutrition/cities.Count;
        }

        public int GetProduction(){
            int production = 0;
            foreach(City city in cities){
                production += (int)city.construction;
            }
            return production/cities.Count;
        }

        internal void CalculatePriorities()
        {
            
        }
    }
}