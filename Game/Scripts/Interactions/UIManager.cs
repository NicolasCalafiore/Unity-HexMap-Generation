using System;
using System.Collections;
using System.Collections.Generic;
using Strategy.Assets.Game.Scripts.Terrain;
using Strategy.Assets.Game.Scripts.Terrain.Water;
using Strategy.Assets.Game.Scripts.Terrain.Regions;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;
using System.Linq;
using Players;
using UnityEngine.UI;
using Character;
using Cabinet;
using Cities;
using AI;




namespace Terrain {

    public static class UIManager
    {
        public static GameObject city_ui;
        public static GameObject dev_ui;
        public static GameObject cabinet_ui;
        public static GameObject hex_ui;
        public static GameObject character_ui;
        public static GameObject dev_panel_ui;
        public static GameObject timer_ui;
        public static GameObject summary_ui;
        public static Dictionary<Button, AbstractCharacter> char_button_binds = new Dictionary<Button, AbstractCharacter>();


        public static void UpdatePlayerUI(Player player){
            LoadPlayerName(player);
            LoadCabinetCharacters(player);
            LoadCharacterScreen(player.government.leader);  
            LoadCityMenu(player.GetCapital());

        }
        public static void FindUIComponents()
        {
            
            city_ui = GameObject.Find("CityUI");
            cabinet_ui = GameObject.Find("CabinetUI");
            character_ui = GameObject.Find("CharacterUI");
            hex_ui = GameObject.Find("HexUI");
            dev_ui = GameObject.Find("DevUI");
            dev_panel_ui = GameObject.Find("DevPanelUI");
            timer_ui = GameObject.Find("TimerUI");
            summary_ui = GameObject.Find("SummaryUI");

            dev_ui.SetActive(true);
            city_ui.SetActive(false);
            cabinet_ui.SetActive(false);
            character_ui.SetActive(false);
            hex_ui.SetActive(false);
            dev_panel_ui.SetActive(false);
            summary_ui.SetActive(false);
            
        }
        public static void LoadHexUI(HexTile hex){
            hex_ui.transform.Find("Shore").GetComponent<TextMeshProUGUI>().text = hex.is_coast ? "Yes" : "No";
            hex_ui.transform.Find("Terrain").GetComponent<TextMeshProUGUI>().text = hex.elevation_type.ToString();
            hex_ui.transform.Find("Region").GetComponent<TextMeshProUGUI>().text = hex.region_type.ToString();
            hex_ui.transform.Find("Owner").GetComponent<TextMeshProUGUI>().text = hex.owner_player == null ? "None" : hex.owner_player.name;
            hex_ui.transform.Find("Structure").GetComponent<TextMeshProUGUI>().text = hex.structure_type.ToString();
            hex_ui.transform.Find("Resource").GetComponent<TextMeshProUGUI>().text = hex.resource_type.ToString();
            hex_ui.transform.Find("Feature").GetComponent<TextMeshProUGUI>().text = hex.feature_type.ToString();
            hex_ui.transform.Find("Culture").GetComponent<TextMeshProUGUI>().text = hex.culture_id.ToString();
            hex_ui.transform.Find("Continent").GetComponent<TextMeshProUGUI>().text = hex.continent_id.ToString();
        }
        public static void LoadPlayerName(Player player){    

            GameObject.Find("WorldUI").transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = player.GetOfficialName();
            GameObject.Find("WorldUI").transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = player.team_color;
        }
        public static void LoadCabinetCharacters(Player player)
        {

            Domestic domestic = player.government.cabinet.domestic_advisor;
            Foreign foreign = player.government.cabinet.foreign_advisor;
            Leader leader = player.government.leader;

            Button leaderBtn = cabinet_ui.transform.Find("Leader").GetComponent<Button>();
            Button foreignBtn = cabinet_ui.transform.Find("Foreign").GetComponent<Button>();
            Button domesticBtn = cabinet_ui.transform.Find("Domestic").GetComponent<Button>();

            char_button_binds.Remove(leaderBtn.GetComponent<Button>());
            char_button_binds.Remove(foreignBtn.GetComponent<Button>());
            char_button_binds.Remove(domesticBtn.GetComponent<Button>());

            char_button_binds.Add(domesticBtn, domestic);
            char_button_binds.Add(foreignBtn, foreign);
            char_button_binds.Add(leaderBtn, leader);

            domesticBtn.transform.Find("Name").GetComponent<TextMeshProUGUI>().text = domestic.GetFullName();
            foreignBtn.transform.Find("Name").GetComponent<TextMeshProUGUI>().text = foreign.GetFullName();
            leaderBtn.transform.Find("Name").GetComponent<TextMeshProUGUI>().text = leader.GetFullName();
        
            domesticBtn.transform.Find("Rating").GetComponent<TextMeshProUGUI>().text = domestic.GetRating().ToString();
            foreignBtn.transform.Find("Rating").GetComponent<TextMeshProUGUI>().text = foreign.GetRating().ToString();
            leaderBtn.transform.Find("Rating").GetComponent<TextMeshProUGUI>().text = leader.GetRating().ToString();
        
            domesticBtn.transform.Find("Rating").GetComponent<TextMeshProUGUI>().color = GetRatingColor(domestic.GetRating());
            foreignBtn.transform.Find("Rating").GetComponent<TextMeshProUGUI>().color = GetRatingColor(foreign.GetRating());
            leaderBtn.transform.Find("Rating").GetComponent<TextMeshProUGUI>().color = GetRatingColor(leader.GetRating());
        }
        public static void LoadCityMenu(City city){
            TextMeshProUGUI city_name = city_ui.transform.Find("CityName").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI population = city_ui.transform.Find("Population").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI stability = city_ui.transform.Find("Stability").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI nutrition = city_ui.transform.Find("Nourishment").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI production = city_ui.transform.Find("Construction").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI region = city_ui.transform.Find("Region").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI wealth = city_ui.transform.Find("PlayerWealth").GetComponent<TextMeshProUGUI>();
            Button owner = city_ui.transform.Find("StateBtn").GetComponent<Button>();
            TextMeshProUGUI owner_text = owner.transform.Find("Name").GetComponent<TextMeshProUGUI>();

            city_name.text = city.name;
            population.text = city.owner_player.GetPopulation().ToString();
            stability.text = city.owner_player.GetStability().ToString();
            nutrition.text = city.owner_player.GetNutrition().ToString();
            production.text = city.owner_player.GetProduction().ToString();
            region.text = city.region_type.ToString();
            wealth.text = city.owner_player.wealth.ToString();
            owner_text.text = city.owner_player.GetOfficialName()  + " (" + city.owner_player.government_type + ")";
            owner_text.text = city.owner_player.GetOfficialName()  + " (" + city.owner_player.government_type + ")";
        }
        public static void LoadCharacterScreen(AbstractCharacter character)
        {
            char_button_binds.Remove(character_ui.transform.GetChild(24).GetComponent<Button>());
            char_button_binds.Add(character_ui.transform.GetChild(24).GetComponent<Button>(), character);

            TextMeshProUGUI title = character_ui.transform.Find("Title").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI name = character_ui.transform.Find("Name").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI wealth = character_ui.transform.Find("Wealth").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI health = character_ui.transform.Find("Health").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI age = character_ui.transform.Find("Age").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI skill = character_ui.transform.Find("Skill").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI intelligence = character_ui.transform.Find("Intelligence").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI charisma = character_ui.transform.Find("Charisma").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI influence = character_ui.transform.Find("Influence").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI loyalty = character_ui.transform.Find("Loyalty").GetComponent<TextMeshProUGUI>();

            Button trait1 = character_ui.transform.Find("TraitOne").GetComponent<Button>();
            Button trait2 = character_ui.transform.Find("TraitTwo").GetComponent<Button>();
            Button trait3 = character_ui.transform.Find("TraitThree").GetComponent<Button>();
            Button trait4 = character_ui.transform.Find("TraitFour").GetComponent<Button>();
            Button trait5 = character_ui.transform.Find("TraitFive").GetComponent<Button>();
            TextMeshProUGUI trait1_text = trait1.transform.Find("Name").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI trait2_text = trait2.transform.Find("Name").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI trait3_text = trait3.transform.Find("Name").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI trait4_text = trait4.transform.Find("Name").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI trait5_text = trait5.transform.Find("Name").GetComponent<TextMeshProUGUI>();

            title.text = character.title;
            name.text = character.GetName();
            wealth.text = character.wealth.ToString();
            health.text = character.health.ToString();
            age.text = character.age.ToString();
            skill.text = character.skill.ToString();
            intelligence.text = character.intelligence.ToString();
            charisma.text = character.charisma.ToString();
            influence.text = character.influence.ToString();
            loyalty.text = character.loyalty.ToString();

            wealth.color = GetRatingColor(character.wealth);
            health.color = GetRatingColor(character.health);
            age.color = GetRatingColor(character.age);
            skill.color = GetRatingColor(character.skill);
            intelligence.color = GetRatingColor(character.intelligence);
            charisma.color = GetRatingColor(character.charisma);
            influence.color = GetRatingColor(character.influence);
            loyalty.color = GetRatingColor(character.loyalty);

            trait1_text.text = character.GetTrait(0) == null ? "None" : character.GetTrait(0).Name;
            trait2_text.text = character.GetTrait(1) == null ? "None" : character.GetTrait(1).Name;
            trait3_text.text = character.GetTrait(2) == null ? "None" : character.GetTrait(2).Name;
            trait4_text.text = character.GetTrait(3) == null ? "None" : character.GetTrait(3).Name;
            trait5_text.text = character.GetTrait(4) == null ? "None" : character.GetTrait(4).Name;
        }
        public static void LoadSummary(){

            string left_text_box = "";
            string right_text_box = "";

            Button summary_btn = character_ui.transform.GetChild(24).GetComponent<Button>();

            if(char_button_binds[summary_btn] is Leader){
                Player owner = char_button_binds[character_ui.transform.GetChild(24).GetComponent<Button>()].owner_player;
                summary_ui.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = $"The {owner.government.leader.title}'s Summary";
        
                left_text_box = "Priorities:\n";

                foreach(CityPriority priority in owner.main_priorities)
                    left_text_box += priority.name + ": " + priority.priority + "\n";
            }

            if(char_button_binds[summary_btn] is Foreign){
                left_text_box  = "Relationships:\n";
                Foreign foreign = char_button_binds[character_ui.transform.GetChild(24).GetComponent<Button>()] as Foreign;
                List<TraitBase> traits = char_button_binds[character_ui.transform.GetChild(24).GetComponent<Button>()].owner_player.GetAllTraits();
                Debug.Log("t_count: " + traits.Count);
                Player owner = char_button_binds[character_ui.transform.GetChild(24).GetComponent<Button>()].owner_player;
                summary_ui.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = $"The {owner.government.cabinet.foreign_advisor.title}'s Summary";

                List<TraitBase> listed_traits = new List<TraitBase>();

                foreach(Player player in foreign.known_players)
                    left_text_box += player.name + ": " + foreign.GetRelationshipFloat(player) + "\n";

                right_text_box += "\nTraits:\n";
                foreach(Player player in foreign.known_players)
                    foreach(TraitBase trait in traits)
                        if(listed_traits.Contains(trait) == false && trait is ForeignTraitBase && ((ForeignTraitBase) trait).isActivated(player, owner)){
                            if(trait is ForeignTraitBase && ((ForeignTraitBase) trait).isBlankEffect == true){
                                listed_traits.Add(trait);
                                right_text_box += "(B) ";
                            }
                            else right_text_box += "(S) ";
                            
                            right_text_box += trait.Name + ": ";

                            if(trait is ForeignTraitBase && ((ForeignTraitBase) trait).isBlankEffect == false)
                                right_text_box += player.GetOfficialName() + "\n";
                                else right_text_box += "\n";
                        }
                       
        
            }

            if(char_button_binds[summary_btn] is Domestic){
                Foreign foreign = char_button_binds[character_ui.transform.GetChild(24).GetComponent<Button>()] as Foreign;
                List<TraitBase> traits = char_button_binds[character_ui.transform.GetChild(24).GetComponent<Button>()].owner_player.GetAllTraits();
                Player owner = char_button_binds[character_ui.transform.GetChild(24).GetComponent<Button>()].owner_player;
                summary_ui.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = $"The {owner.government.cabinet.domestic_advisor.title}'s Summary";

                left_text_box = "Traits:\n";
                foreach(TraitBase trait in traits)
                    if(trait is DomesticTraitBase && ((DomesticTraitBase) trait).isActivated(owner)) left_text_box += trait.Name + "\n";
        
            }

            summary_ui.transform.Find("Right").GetComponent<TextMeshProUGUI>().text = right_text_box;
            summary_ui.transform.Find("Left").GetComponent<TextMeshProUGUI>().text = left_text_box;
        }
        public static void UpdateSeconds(){
            // int minutes = Int32.Parse(timer_ui.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text);
            // int hours = Int32.Parse(timer_ui.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text);
            // minutes++;

            // if(minutes < 10) timer_ui.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "0" + minutes.ToString();
            // else timer_ui.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = minutes.ToString();

            // if(minutes == 60){
            //     hours++;
            //     if(hours < 10) timer_ui.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "0" + hours.ToString();
            //     else timer_ui.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = minutes.ToString();
            //     timer_ui.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "00";
            // }
        }
        private static Color GetRatingColor(int number){
            if(number > 90) return Color.magenta;
            if(number > 80) return Color.blue;
            if(number > 65) return Color.green;
            if(number > 45) return Color.yellow;
            if(number > 25) return new Color(1, 0.667f, 0);
            return Color.red;
        }
    }
}