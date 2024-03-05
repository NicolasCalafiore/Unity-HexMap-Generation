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
using Cabinet;




namespace Terrain {

    public static class UIManager
    {
        public static GameObject city_ui;
        public static GameObject dev_ui;
        public static GameObject cabinet_ui;
        public static GameObject hex_ui;
        public static GameObject character_ui;
        public static GameObject dev_panel_ui;
        public static Dictionary<Button, ICharacter> character_binds = new Dictionary<Button, ICharacter>();

        public static void InitializeUI()
        {

            city_ui = GameObject.Find("CityUI");
            cabinet_ui = GameObject.Find("CabinetUI");
            character_ui = GameObject.Find("CharacterUI");
            hex_ui = GameObject.Find("HexUI");
            dev_ui = GameObject.Find("DevUI");
            dev_panel_ui = GameObject.Find("DevPanelUI");



            dev_ui.SetActive(true);
            city_ui.SetActive(false);
            cabinet_ui.SetActive(false);
            character_ui.SetActive(false);
            hex_ui.SetActive(false);
            dev_panel_ui.SetActive(false);
        }



        public static void SetHexUI(HexTile hex){

            hex_ui.SetActive(true);
            hex_ui.transform.Find("Shore").GetComponent<TextMeshProUGUI>().text = hex.IsCoast() ? "Yes" : "No";
            hex_ui.transform.Find("Terrain").GetComponent<TextMeshProUGUI>().text = hex.GetElevationType().ToString();
            hex_ui.transform.Find("Region").GetComponent<TextMeshProUGUI>().text = hex.GetRegionType().ToString();


            hex_ui.transform.Find("Owner").GetComponent<TextMeshProUGUI>().text = hex.GetOwnerPlayer() == null ? "None" : hex.GetOwnerPlayer().GetName();


            hex_ui.transform.Find("Structure").GetComponent<TextMeshProUGUI>().text = hex.GetStructureType().ToString();
            hex_ui.transform.Find("Resource").GetComponent<TextMeshProUGUI>().text = hex.GetResourceType().ToString();
            hex_ui.transform.Find("Feature").GetComponent<TextMeshProUGUI>().text = hex.GetFeatureType().ToString();
            
        }

        public static void SetPlayerName(Player player){
            GameObject.Find("WorldUI").transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = player.GetOfficialName();
            GameObject.Find("WorldUI").transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = player.GetTeamColor();

        }

        internal static void LoadCabinetCharacters(Player player)
        {
            Domestic domestic = player.GetGovernment().cabinet.GetDomestic(0);
            Foreign foreign = player.GetGovernment().cabinet.GetForeign(0);
            Leader leader = player.GetGovernment().GetLeader();


            character_binds.Clear();
            try{
                character_binds.Add(cabinet_ui.transform.GetChild(3).GetComponent<Button>(), domestic);
                character_binds.Add(cabinet_ui.transform.GetChild(2).GetComponent<Button>(), foreign);
                character_binds.Add(cabinet_ui.transform.GetChild(1).GetComponent<Button>(), leader);
            }
            catch(Exception e){
                Debug.Log("Normal Bug 1 --> STARTUP INTIALIZATION");
            }


            try{
                cabinet_ui.transform.GetChild(3).GetComponent<Button>().transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = domestic.GetFullName();
                cabinet_ui.transform.GetChild(2).GetComponent<Button>().transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = foreign.GetFullName();
                cabinet_ui.transform.GetChild(1).GetComponent<Button>().transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = leader.GetFullName();
            }
            catch(Exception e){
                Debug.Log("Normal Bug 1 --> STARTUP INTIALIZATION");
            }

            try{
                cabinet_ui.transform.GetChild(3).GetComponent<Button>().transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = domestic.GetRating().ToString();
                cabinet_ui.transform.GetChild(2).GetComponent<Button>().transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = foreign.GetRating().ToString();
                cabinet_ui.transform.GetChild(1).GetComponent<Button>().transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = leader.GetRating().ToString();
            
                cabinet_ui.transform.GetChild(3).GetComponent<Button>().transform.GetChild(1).GetComponent<TextMeshProUGUI>().color = GetNumberColor(domestic.GetRating());
                cabinet_ui.transform.GetChild(2).GetComponent<Button>().transform.GetChild(1).GetComponent<TextMeshProUGUI>().color = GetNumberColor(foreign.GetRating());
                cabinet_ui.transform.GetChild(1).GetComponent<Button>().transform.GetChild(1).GetComponent<TextMeshProUGUI>().color = GetNumberColor(leader.GetRating());
            
            }
            catch(Exception e){
                Debug.Log("Normal Bug 1 --> STARTUP INTIALIZATION");
            }
        }

        public static void UpdatePlayerUI(Player player){
            SetPlayerName(player);
            LoadCabinetCharacters(player);
            LoadCharacterScreen(player.GetGovernment().GetLeader());

        }

        public static void ShowCityMenu(City city){
            city_ui.SetActive(true);
            if(city_ui == null) Debug.LogError("City UI is null");
            city_ui.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = city.GetName();
            city_ui.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = city.GetPopulation().ToString();
            city_ui.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = city.GetStability().ToString();
            city_ui.transform.GetChild(4).GetComponent<TextMeshProUGUI>().text = city.GetNourishment().ToString();
            city_ui.transform.GetChild(5).GetComponent<TextMeshProUGUI>().text = city.GetConstruction().ToString();
            city_ui.transform.GetChild(6).GetComponent<TextMeshProUGUI>().text = city.GetRegionType().ToString();
            city_ui.transform.GetChild(7).GetComponent<Button>().transform.GetChild(0).GetComponent<TextMeshPro>().text = city.GetPlayer().GetOfficialName();
        }

        private static Color GetNumberColor(int number){
            if(number > 90) return Color.magenta;
            if(number > 80) return Color.blue;
            if(number > 65) return Color.green;
            if(number > 45) return Color.yellow;
            if(number > 25) return new Color(1, 0.667f, 0);
            return Color.red;
        }

        internal static void LoadCharacterScreen(ICharacter character)
        {
            try{
                character_ui.transform.GetChild(9+1).GetComponent<TextMeshProUGUI>().text = character.loyalty.ToString();
                character_ui.transform.GetChild(8+1).GetComponent<TextMeshProUGUI>().text = character.influence.ToString();
                character_ui.transform.GetChild(7+1).GetComponent<TextMeshProUGUI>().text = character.charisma.ToString();
                character_ui.transform.GetChild(6+1).GetComponent<TextMeshProUGUI>().text = character.intelligence.ToString();
                character_ui.transform.GetChild(5+1).GetComponent<TextMeshProUGUI>().text = character.skill.ToString();
                character_ui.transform.GetChild(4+1).GetComponent<TextMeshProUGUI>().text = character.age.ToString();
                character_ui.transform.GetChild(3+1).GetComponent<TextMeshProUGUI>().text = character.health.ToString();
                character_ui.transform.GetChild(2+1).GetComponent<TextMeshProUGUI>().text = character.wealth.ToString();
                character_ui.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = character.title;
                character_ui.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = character.GetName();


                character_ui.transform.GetChild(9+1).GetComponent<TextMeshProUGUI>().color = GetNumberColor(character.loyalty);
                character_ui.transform.GetChild(8+1).GetComponent<TextMeshProUGUI>().color = GetNumberColor(character.influence);
                character_ui.transform.GetChild(7+1).GetComponent<TextMeshProUGUI>().color = GetNumberColor(character.charisma);
                character_ui.transform.GetChild(6+1).GetComponent<TextMeshProUGUI>().color = GetNumberColor(character.intelligence);
                character_ui.transform.GetChild(5+1).GetComponent<TextMeshProUGUI>().color = GetNumberColor(character.skill);
                character_ui.transform.GetChild(4+1).GetComponent<TextMeshProUGUI>().color = GetNumberColor(character.age);
                character_ui.transform.GetChild(3+1).GetComponent<TextMeshProUGUI>().color = GetNumberColor(character.health);
                character_ui.transform.GetChild(2+1).GetComponent<TextMeshProUGUI>().color = GetNumberColor(character.wealth);

                character_ui.transform.GetChild(13).GetComponent<Button>().transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = character.GetTrait(0) == null ? "None" : character.GetTrait(0).GetName(); 
                character_ui.transform.GetChild(12).GetComponent<Button>().transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = character.GetTrait(1) == null ? "None" : character.GetTrait(1).GetName(); 
                character_ui.transform.GetChild(11).GetComponent<Button>().transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = character.GetTrait(2) == null ? "None" : character.GetTrait(2).GetName(); 
            }
            catch(Exception e){
                Debug.Log("Normal Bug 1 --> STARTUP INTIALIZATION");
            }
            
        }

    }
}