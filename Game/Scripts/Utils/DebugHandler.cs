using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Cabinet;
using Character;
using Players;
using Strategy.Assets.Game.Scripts.Terrain;
using Strategy.Assets.Scripts.Objects;
using Terrain;
using TMPro;
using UnityEngine;



public static class DebugHandler
{

    /*
        DebugHandler is used to spawn debug viewers
        DebugHandler is used to print debug messages
    */


    public static void GetHexInformation(GameObject gameObject){  // Used to read HexTile object from GameObject from MouseInputHandler
        string MESSAGE = "Hex Information: \n";
        GameObject hex_go = gameObject.transform.parent.gameObject;
        HexTile hex = TerrainManager.hex_to_hex_go.FirstOrDefault(x => x.Value == hex_go).Key;
        MESSAGE += "Hex: " + hex.GetColRow().x + " " + hex.GetColRow().y + "\n";
        MESSAGE += "Elevation: " + hex.GetPosition().y  + "\n";
        MESSAGE += "Elevation Type: " + hex.GetElevationType()  + "\n";
        MESSAGE += "Region Type: " + hex.GetRegionType()  + "\n";
        MESSAGE += "Land Type: " + hex.GetLandType() + "\n";
        MESSAGE += "Feature Type: " + hex.GetFeatureType() + "\n";
        MESSAGE += "Resource Type: " + hex.GetResourceType() + "\n";
        MESSAGE += "Structure Type: " + hex.GetStructureType() + "\n";
        MESSAGE += "Nourishment: " + hex.nourishment + "\n";
        MESSAGE += "Construction: " + hex.construction + "\n";
        MESSAGE += "Movement Cost:" + hex.MovementCost + "\n";

        if(hex.GetOwnerPlayer() != null){
            MESSAGE += "Player Owner: " + hex.GetOwnerPlayer().GetName() + "\n";
        }
        else{
            MESSAGE += "Player Owner: None\n";
        }

        if(hex.GetOwnerCity() != null){
            MESSAGE += "City Owner: " + hex.GetOwnerCity().GetName() + "\n";
        }
        else{
            MESSAGE += "City Owner: None\n";
        }
        Debug.Log(MESSAGE);
    }

    public static void PrintMapDebug(string title,  List<List<float>> map){ // Used to print List<List<float>> maps
        string message = title + "\n";
        foreach(List<float> row in map){
            foreach(float value in row){
                message += value + " ";
            }
            message += "\n";
        }

        Debug.Log(message);
    }


    internal static void GetPlayerInformation(GameObject city_collider)
    {
        GameObject city_go = city_collider.transform.parent.gameObject;
        City city = TerrainManager.city_go_to_city[city_go];
        Player player = city.GetPlayer();

        string MESSAGE = "Player Information: \n";
        MESSAGE += "Player State: " +  player.GetOfficialName() + "\n";
        MESSAGE += "Government Type: " + player.GetGovernmentType() + "\n";
        MESSAGE += "Color: " + player.GetTeamColor() + "\n";
        MESSAGE += "Cities: " + player.GetCities().Count + "\n";
        MESSAGE += "Wealth: " + player.GetWealth() + "\n";
        MESSAGE += "Knowledge: " + player.GetKnowledgePoints() + "\n";
        MESSAGE += "Heritage: " + player.GetHeritagePoints() + "\n";
        MESSAGE += "Belief: " + player.GetBeliefPoints() + "\n";
                
        Debug.Log(MESSAGE);
    }

    public static void DisplayMessage(List<string> message){
        string MESSAGE = "";
        foreach(string line in message){
            MESSAGE += line + "\n";
        }
        Debug.Log(MESSAGE);
    }

    public static void DisplayCharacter(ICharacter character){
        string MESSAGE = "Character Information: \n";
        MESSAGE += "Name: " + character.GetFullName() + "\n";

        switch(character){
            case Leader:
                MESSAGE += "Role: Leader\n";
                break;
            case Domestic:
                MESSAGE += "Role: Domestic\n";
                break;
            case Foreign:
                MESSAGE += "Role: Foreign\n";
                break;
        }

        MESSAGE += "Type: " + character.character_type + "\n";
        MESSAGE += "Gender: " + character.gender + "\n\n";


        MESSAGE += "Charisma: " + character.charisma + "\n";
        MESSAGE += "Intelligence: " + character.intelligence + "\n";
        MESSAGE += "Skill: " + character.skill + "\n";
        MESSAGE += "Age: " + character.age + "\n";
        MESSAGE += "Health: " + character.health + "\n";
        MESSAGE += "Loyalty: " + character.loyalty + "\n";
        MESSAGE += "Wealth: " + character.wealth + "\n";
        MESSAGE += "Influence: " + character.influence + "\n\n";

        foreach(TraitBase trait in character.traits){
            MESSAGE += "Trait: " + trait.GetName() + "\n";
        }
        Debug.Log(MESSAGE);

    }

    public static void ClearLog(){
        var assembly = Assembly.GetAssembly(typeof(UnityEditor.Editor));
        var type = assembly.GetType("UnityEditor.LogEntries");
        var method = type.GetMethod("Clear");
        method.Invoke(new object(), null);
    }

}
