using System.Collections.Generic;
using Character;
using Diplomacy;
using Players;
using Terrain;
using Unity.VisualScripting;
using UnityEngine;
using static Terrain.ForeignEnums;


namespace Cabinet
{
    public class Foreign : AbstractCharacter
    {

        public List<Player> known_players = new List<Player>();
        public Dictionary<Player, float> relations = new Dictionary<Player, float>();


        public Foreign(List<string> names, CharacterEnums.CharacterGender gender, Player player, List<string> titles): base(names, gender, player, titles){}


        // Scans the territory map and fog of war for new players
        // If a new player is found, it is added to the known players list
        public void ScanForNewPlayers(List<List<float>> fog_of_war, Player player){
            List<List<float>> territory_map = MapManager.territory_map_handler.territory_map;

            for(int i = 0; i < territory_map.Count; i++)
            {
                ScanRowForNewPlayers(territory_map[i], fog_of_war[i], player.id);
            }
        }

        // Scans a row of the territory map and fog of war for new players
        // If a new player is found, it is added to the known players list
        // A player is valid if the fog of war is discovered, the territory map is not owned by the player, the territory map is not -1, the new player is not null, and the new player is not already known
        private void ScanRowForNewPlayers(List<float> territory_row, List<float> fog_row, int player_id){
            for(int j = 0; j < territory_row.Count; j++)
            {
                if(territory_row[j] == -1) continue;

                Player new_player = PlayerManager.player_id_to_player[(int) territory_row[j]];
                if(IfIsValidPlayer(fog_row[j], territory_row[j], player_id, new_player))
                {
                    known_players.Add(new_player);
                }
            }
        }

        // Determines if a player is valid based on the fog of war, territory map, player id and new player
        // A player is valid if the fog of war is discovered, the territory map is not owned by the player, the territory map is not -1, the new player is not null, and the new player is not already known
        public bool IfIsValidPlayer(float fog_map, float territory_map, int player_id, Player new_player){

            return fog_map == (float)FogEnums.FogType.Discovered &&
                    territory_map != player_id &&
                    territory_map != -1 &&
                    new_player != null &&
                    !known_players.Contains(new_player);
        }
        

        public List<Player> GetKnownPlayers() => known_players;

        public float GetRelationshipFloat(Player player) => relations[player];
        public RelationshipLevel GetRelationshipLevel(Player player) => ForeignEnums.GetRelationshipLevel(GetRelationshipFloat(player));
        
        // Returns a random known player or null if there are no known players
        public Player GetRandomKnownPlayerNullable() => known_players.Count == 0 ? null : known_players[Random.Range(0, known_players.Count)];




    }
}