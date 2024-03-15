using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Cabinet;
using Character;
using Players;
using Strategy.Assets.Game.Scripts.Terrain;
using Cities;
using Terrain;
using TMPro;
using UnityEngine;
using Diplomacy;
using UnityEditor;



public static class DebugHandler
{

    /*
        DebugHandler is used to spawn debug viewers
        DebugHandler is used to print debug messages
    */

    public static void DisplayMessage(List<string> message){
        string MESSAGE = "";
        foreach(string line in message){
            MESSAGE += line + "\n";
        }
        Debug.Log(MESSAGE);
    }

    public static void Print2DMap(List<List<float>> map){
        string MESSAGE = "";
        for(int i = 0; i < map.Count; i++){
            for(int j = 0; j < map[i].Count; j++){
                MESSAGE += map[i][j] + " ";
            }
            MESSAGE += "\n";
        }
        Debug.Log(MESSAGE);
    }

    public static void RelationshipBreakDown(Player owner_player){
        foreach(Player other_player in owner_player.government.cabinet.foreign_advisor.known_players){
            
            List<string> message = new List<string>();
            message.Add($"{owner_player.GetOfficialName()} <--> {other_player.GetOfficialName()}");
            message.Add($"Final Relationship: {owner_player.government.cabinet.foreign_advisor.GetRelationshipLevel(other_player)} ({owner_player.government.cabinet.foreign_advisor.relations[other_player]})");
            message.Add($"Base Relationship: {DiplomacyManager.GenerateBaseRelationship(owner_player, other_player)}");
            message.Add($"Trait Relationship Impact: {DiplomacyManager.CalculateTraitRelationshipImpact(owner_player, other_player)}");
            message.Add($"Similiar Trait Impact: {DiplomacyManager.CalculateSimiliarTraitsImpact(owner_player, other_player)}");
            message.Add($"Dependant Relationship Impact: {DiplomacyManager.CalculateRelationshipDependantRelationshipImpact(owner_player, other_player)}");

            DisplayMessage(message);
        }
    }

    public static void ClearLogConsole() {
        var assembly = Assembly.GetAssembly(typeof(UnityEditor.Editor));
        var type = assembly.GetType("UnityEditor.LogEntries");
        var method = type.GetMethod("Clear");
        method.Invoke(new object(), null);
    }
}
