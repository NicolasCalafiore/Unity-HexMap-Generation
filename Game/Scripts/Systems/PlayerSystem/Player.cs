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
using static Terrain.PlayerEnums;


namespace Players {
    public class Player {

        public string state_prefix { get; set; } = "";
        public string state_suffix { get; set; } = "";
        public string name  { get; set; }
        public int id  { get; set; }
        public float wealth { get; set; } = 1000;
        public int knowledge_points { get; set; } = 0;
        public int heritage_points { get; set; } = 0;
        public int belief_points { get; set; } = 0;
        public Color team_color  { get; set; }
        public GovernmentType government_type { get; set; }
        public Government government  { get; set; }
        public HexRegion home_region { get; set; }
        public PlayerPriority priority { get; set; }
        private List<City> cities = new List<City>();
        private List<List<float>> fog_of_war_map;




        public Player(string name, int id){
            this.name = name;
            this.team_color = new Color(Random.Range(0f, 1f),Random.Range(0f, 1f),Random.Range(0f, 1f));
            this.id = id;
            PlayerManager.player_id_to_player.Add(id, this);
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

    }
}