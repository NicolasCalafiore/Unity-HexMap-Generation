using System.Collections.Generic;
using Character;
using Diplomacy;
using Players;
using Terrain;
using Unity.VisualScripting;
using UnityEngine;


namespace Cabinet
{
    public class Foreign : ICharacter
    {

        public List<Player> known_players = new List<Player>();
        public Dictionary<Player, float> relations = new Dictionary<Player, float>();
        public ForeignStrategy foreign_strategy;

        public Foreign(List<string> names, CharacterEnums.CharacterGender gender, Player player, List<string> titles){
            this.first_name = names[0];
            this.last_name = names[1];
            this.gender = gender;
            this.character_type = CharacterEnums.CharacterType.Advisor;
            this.owner_player = player;
            this.title = titles[UnityEngine.Random.Range(0, titles.Count)];
        }


        public void SetForeignStrategy(int strategy){
            switch(strategy){
                case 0:
                    foreign_strategy = new ForeignStandard();
                    break;
                default:
                    foreign_strategy = new ForeignStandard();
                    break;
            }
        }

        public void GenerateStartingRelationship(List<Player> known_players){
            SetForeignStrategy(0);
            
            foreach(Player player in known_players){
                relations.Add(player, foreign_strategy.GenerateStartingRelationship(player, owner_player));
            }

        }

   

        public void ScanForNewPlayers(List<List<float>> territory_map, List<List<float>> fog_of_war, int player_id){
            for(int i = 0; i < territory_map.Count; i++){
                for(int j = 0; j < territory_map[i].Count; j++){
                    if(territory_map[i][j] == -1) continue;
                    
                    Player new_player = Player.player_id_to_player[(int) territory_map[i][j]];
                    if(IfIsValidPlayer(fog_of_war[i][j], territory_map[i][j], player_id, new_player)){
                        known_players.Add(new_player);
                    }
                    
                }
            }
        }

        public bool IfIsValidPlayer(float fog_map, float territory_map, int player_id, Player new_player){
            if(fog_map != (float) FogEnums.FogType.Discovered) return false;
            if(territory_map == player_id) return false;
            if(territory_map == -1) return false;
            if(known_players.Contains(new_player)) return false;
            if(new_player == null) return false;

            return true;
        }

        public List<Player> GetKnownPlayers(){
            return known_players;
        }

        public float GetRelationship(Player player){
            return relations[player];
        }

        public void PrintRelationships(){
            
            foreach(Player i in known_players){
                string MESSAGE = "Relationship with " + i.GetOfficialName() + " is " + relations[i] + "\n";
                List<string> relationship_calculations = foreign_strategy.CalculationValues(i, owner_player);
                foreach(string j in relationship_calculations){
                    MESSAGE += j + "\n";
                }
                Debug.Log(MESSAGE);
            }

        }

        public Player GetRandomKnownPlayer(){
            if(known_players.Count == 0) return null;
            
            return known_players[Random.Range(0, known_players.Count)];
        }




    }
}