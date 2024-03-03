using System;
using System.Collections;
using System.Collections.Generic;
using Strategy.Assets.Game.Scripts.Terrain;
using Strategy.Assets.Game.Scripts.Terrain.Water;
using Strategy.Assets.Game.Scripts.Terrain.Regions;
using Unity.VisualScripting;
using UnityEngine;
using Strategy.Assets.Scripts.Objects;
using TMPro;
using System.Linq;
using Players;
using UnityEngine.UI;
using Character;




namespace Terrain {

    public class UIManager : MonoBehaviour
    {
        public static GameObject city_menu;
        public static GameObject player_menu;
        public static GameObject game_menu;
        public static GameObject character_menu;


        public void Start()
        {
            city_menu = GameObject.Find("CityMenu");
            player_menu = GameObject.Find("PlayerMenu");
            game_menu = GameObject.Find("GameMenu");
            character_menu = GameObject.Find("CharacterMenu");
            Debug.Log("All menus initialized");

            city_menu.SetActive(false);
            player_menu.SetActive(false);
            character_menu.SetActive(false);
        }


        public void CloseCityUI(){
            city_menu.SetActive(false);
        }

        public void ClosePlayerUI(){
            player_menu.SetActive(false);
        }

        public void CloseCharacterUI(){
            character_menu.SetActive(false);
        }

        public static void ShowCityMenu(City city)
        {
            Player player = city.GetPlayer();

            TextMeshProUGUI city_name = city_menu.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
            // = GameObject.Find("CityName").GetComponent<TextMeshProUGUI>();
            city_name.text = city.GetName();

            TextMeshProUGUI population = city_menu.transform.GetChild(7).GetComponent<TextMeshProUGUI>();
            population.text = "" + city.GetPopulation();

            TextMeshProUGUI region = city_menu.transform.GetChild(8).GetComponent<TextMeshProUGUI>();
            region.text = "" + city.GetRegionType();

            TextMeshProUGUI player_name = city_menu.transform.GetChild(12).transform.GetChild(0).GetComponent<TextMeshProUGUI>();
            player_name.text = player.GetName();
            
            TextMeshProUGUI construction = city_menu.transform.GetChild(11).GetComponent<TextMeshProUGUI>();
            construction.text = "" + city.GetConstruction();

            TextMeshProUGUI nourishment = city_menu.transform.GetChild(10).GetComponent<TextMeshProUGUI>();
            nourishment.text = "" + city.GetNourishment();

            TextMeshProUGUI stability = city_menu.transform.GetChild(9).GetComponent<TextMeshProUGUI>();
            stability.text = "" + city.GetStability();

            ButtonInput.player = player;
            city_menu.SetActive(true);

        
        }

        public static void ShowPlayerMenu(Player player){

            TextMeshProUGUI official_name = player_menu.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
            official_name.text = player.GetOfficialName();

            TextMeshProUGUI government_type = player_menu.transform.GetChild(2).GetComponent<TextMeshProUGUI>();
            government_type.text = "Government Type: " + player.GetGovernmentType();

            TextMeshProUGUI wealth = player_menu.transform.GetChild(4).GetComponent<TextMeshProUGUI>();
            wealth.text = "" + player.GetWealth();

            TextMeshProUGUI domestic_name =player_menu.transform.GetChild(7).transform.GetChild(0).GetComponent<TextMeshProUGUI>();
            domestic_name.text =  player.GetGovernment().GetDomestic(0).GetFullName();

            
            TextMeshProUGUI foreign_name = player_menu.transform.GetChild(6).transform.GetChild(0).GetComponent<TextMeshProUGUI>();
            foreign_name.text =  player.GetGovernment().GetForeign(0).GetFullName();

            TextMeshProUGUI leader_name = player_menu.transform.GetChild(5).transform.GetChild(0).GetComponent<TextMeshProUGUI>();
            leader_name.text = player.GetGovernment().GetLeader().GetFullName();


            TextMeshProUGUI domestic_rating = player_menu.transform.GetChild(7).transform.GetChild(1).GetComponent<TextMeshProUGUI>();
            domestic_rating.text =  "" + player.GetGovernment().GetDomestic(0).GetRating();
            domestic_rating.color = GetNumberColor(player.GetGovernment().GetDomestic(0).GetRating());

            TextMeshProUGUI leader_rating = player_menu.transform.GetChild(5).transform.GetChild(1).GetComponent<TextMeshProUGUI>();
            leader_rating.text =  "" + player.GetGovernment().GetLeader().GetRating();
            leader_rating.color = GetNumberColor(player.GetGovernment().GetLeader().GetRating());

            TextMeshProUGUI foreign_rating = player_menu.transform.GetChild(6).transform.GetChild(1).GetComponent<TextMeshProUGUI>();
            foreign_rating.text =  "" + player.GetGovernment().GetForeign(0).GetRating();
            foreign_rating.color = GetNumberColor(player.GetGovernment().GetForeign(0).GetRating());

            ButtonInput.character_binds[player_menu.transform.GetChild(7).GetComponent<Button>()] = player.GetGovernment().GetDomestic(0);
            ButtonInput.character_binds[player_menu.transform.GetChild(6).GetComponent<Button>()] = player.GetGovernment().GetForeign(0);
            ButtonInput.character_binds[player_menu.transform.GetChild(5).GetComponent<Button>()] = player.GetGovernment().GetLeader();

            player_menu.SetActive(true);
        }

        public static void ShowCharacterMenu(ICharacter character){
            TextMeshProUGUI name = character_menu.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
            name.text = character.GetName();

            TextMeshProUGUI role = character_menu.transform.GetChild(3).GetComponent<TextMeshProUGUI>();
            role.text = "" + character.character_type;

            TextMeshProUGUI title = character_menu.transform.GetChild(2).GetComponent<TextMeshProUGUI>();
            title.text = "" + character.title;

            TextMeshProUGUI charisma = character_menu.transform.GetChild(4).GetComponent<TextMeshProUGUI>();
            charisma.text = "" + character.charisma;
            charisma.color = GetNumberColor(character.charisma);

            TextMeshProUGUI intelligence = character_menu.transform.GetChild(5).GetComponent<TextMeshProUGUI>();
            intelligence.text = "" + character.intelligence;
            intelligence.color = GetNumberColor(character.intelligence);

            TextMeshProUGUI skill = character_menu.transform.GetChild(6).GetComponent<TextMeshProUGUI>();
            skill.text = "" + character.skill;
            skill.color = GetNumberColor(character.skill);

            TextMeshProUGUI age = character_menu.transform.GetChild(7).GetComponent<TextMeshProUGUI>();
            age.text = "" + character.age;

            TextMeshProUGUI health = character_menu.transform.GetChild(8).GetComponent<TextMeshProUGUI>();
            health.text = "" + character.health;
            health.color = GetNumberColor(character.health);

            TextMeshProUGUI loyalty = character_menu.transform.GetChild(9).GetComponent<TextMeshProUGUI>();
            loyalty.text = "" + character.loyalty;
            loyalty.color = GetNumberColor(character.loyalty);

            TextMeshProUGUI wealth = character_menu.transform.GetChild(10).GetComponent<TextMeshProUGUI>();
            wealth.text = "" + character.wealth;
            wealth.color = GetNumberColor(character.wealth);

            TextMeshProUGUI influence = character_menu.transform.GetChild(11).GetComponent<TextMeshProUGUI>();
            influence.text = "" + character.influence;
            influence.color = GetNumberColor(character.influence);

            TextMeshProUGUI trait1 = character_menu.transform.GetChild(21).transform.GetChild(0).GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI trait2 = character_menu.transform.GetChild(22).transform.GetChild(0).GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI trait3 = character_menu.transform.GetChild(23).transform.GetChild(0).GetComponent<TextMeshProUGUI>();

            trait1.text = "None";
            trait2.text = "None";
            trait3.text = "None";

            foreach(TraitBase trait in character.GetTraits()){
                if(trait1.text == "None"){
                    trait1.text = trait.GetName();
                } else if(trait2.text == "None"){
                    trait2.text = trait.GetName();
                } else if(trait3.text == "None"){
                    trait3.text = trait.GetName();
                }
            }


            character_menu.SetActive(true);

        }

        public static void UpdatePlayerView(Player player_view)
        {
            
            if(city_menu.activeSelf){
                ShowCityMenu(player_view.GetCityByIndex(0));
            } 
            if(player_menu.activeSelf){
                ShowPlayerMenu(player_view);
            }
            if(character_menu.activeSelf){
                ShowCharacterMenu(player_view.GetGovernment().GetLeader());
            }
        }

        private static Color GetNumberColor(int number){
            if(number > 90) return Color.magenta;
            if(number > 80) return Color.blue;
            if(number > 65) return Color.green;
            if(number > 45) return Color.yellow;
            if(number > 25) return new Color(1, 0.667f, 0);
            return Color.red;
        }
    }
}