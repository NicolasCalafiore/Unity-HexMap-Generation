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




namespace Terrain {

    public class UIManager : MonoBehaviour
    {
        public GameObject city_ui;
        public GameObject hex_ui;
        public GameObject world_ui;

        public UIManager(){
            city_ui = GameObject.Find("CityUI");
            city_ui.SetActive(false);

            hex_ui = GameObject.Find("HexUI");
            hex_ui.SetActive(false);

            world_ui = GameObject.Find("WorldUI");
        }

        internal void GetCityInformation(GameObject city_collider)
        {

            city_ui.SetActive(true);

            GameObject city_go = city_collider.transform.parent.gameObject;
            City city = TerrainGameHandler.city_go_to_city[city_go];
            Player player = GameManager.player_id_to_player[city.GetPlayerId()];

            TextMeshProUGUI city_title_ui = GameObject.Find("CityTitle").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI city_owner_ui = GameObject.Find("OwnerTitle").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI city_inhabitants_ui = GameObject.Find("InhabitantsTitle").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI city_stability_ui = GameObject.Find("StabilityTitle").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI city_nourishment_ui = GameObject.Find("NourishmentTitle").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI city_construction_ui = GameObject.Find("ConstructionTitle").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI city_territory_ui = GameObject.Find("TerritoryTitle").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI city_construction_rate = GameObject.Find("ECCTitle").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI city_nourishment_rate = GameObject.Find("ECNTitle").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI state_leader_name = GameObject.Find("StateLeader").GetComponent<TextMeshProUGUI>();

            city_title_ui.text = city.GetName();
            city_owner_ui.text = player.name;
            city_inhabitants_ui.text = city.inhabitants.ToString();
            city_stability_ui.text = city.stability.ToString();
            city_nourishment_ui.text = city.nourishment.ToString();
            city_construction_ui.text = city.construction.ToString();
            city_territory_ui.text = city.hex_territory_list.Count.ToString();
            city_construction_rate.text = GameManager.territory_manager.CalculateCityConstruction(city).ToString();
            city_nourishment_rate.text = GameManager.territory_manager.CalculateCityNourishment(city).ToString();

            string gender = player.GetGovernment().GetLeader().gender == EnumHandler.CharacterGender.Male ? "M" : "F";        //TO DO: REFACTOR THIS
            state_leader_name.text = player.GetGovernment().GetLeader().GetFullName() + " (" + gender + ")";
            
            

        }

    
        public void GetHexInformation(GameObject gameObject){  // Used to read HexTile object from GameObject from MouseInputHandler
            hex_ui.SetActive(true);

            GameObject hex_go = gameObject.transform.parent.gameObject;
            HexTile hex = TerrainGameHandler.hex_to_hex_go.FirstOrDefault(x => x.Value == hex_go).Key;

            TextMeshProUGUI hex_elevation_ui = GameObject.Find("ElevationType").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI hex_region_ui = GameObject.Find("RegionType").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI hex_land_ui = GameObject.Find("LandType").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI hex_feature_ui = GameObject.Find("FeatureType").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI hex_resource_ui = GameObject.Find("ResourceType").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI hex_structure_ui = GameObject.Find("StructureType").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI hex_nourishment_ui = GameObject.Find("NourishmentValue").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI hex_construction_ui = GameObject.Find("ConstructionValue").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI hex_movecost = GameObject.Find("MoveCostValue").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI hex_owner = GameObject.Find("PlayerTitle").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI hex_city = GameObject.Find("CityTitle").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI coast_title = GameObject.Find("CoastTitle").GetComponent<TextMeshProUGUI>();

            hex_elevation_ui.text = hex.elevation_type.ToString();
            hex_region_ui.text = hex.region_type.ToString();
            hex_land_ui.text = hex.land_type.ToString();
            hex_feature_ui.text = hex.feature_type.ToString();
            hex_resource_ui.text = hex.resource_type.ToString();
            hex_structure_ui.text = hex.structure_type.ToString();
            hex_nourishment_ui.text = hex.nourishment.ToString();
            hex_construction_ui.text = hex.construction.ToString();
            hex_movecost.text = hex.MovementCost.ToString();
            hex_owner.text = hex.owner_player != null ? hex.owner_player.name : "None";
            hex_city.text = hex.owner_city != null ? hex.owner_city.GetName() : "None";
            coast_title.text = hex.IsCoast() ? "Coast" : "Not Coast";


        }

        public void CloseHexUI(){
            hex_ui.SetActive(false);
        }


        public void CloseCityUI(){
            city_ui.SetActive(false);
        }




    }
}