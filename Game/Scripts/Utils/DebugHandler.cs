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
        foreach(Player player in owner_player.government.GetForeignByIndex(0).known_players){
            
            List<string> message = new List<string>();

            float trait_impact = owner_player.government.GetForeignByIndex(0).foreign_strategy.GenerateStartingRelationship(player, owner_player);
            float relationship_impact = owner_player.government.GetForeignByIndex(0).foreign_strategy.CalculateRelationshipDependantRelationshipImpact(player, owner_player);
        
            message.Add($"{owner_player.GetOfficialName()} and {player.GetOfficialName()}");
            message.Add($"Relationship: {owner_player.government.GetForeignByIndex(0).GetRelationshipLevel(player)}");
            message.Add($"Relationship Value: {owner_player.government.GetForeignByIndex(0).GetRelationshipFloat(player)}");
            message.Add($"Final Trait Impact: {trait_impact}");
            message.Add($"Final Relationship Impact: {relationship_impact}");

            DisplayMessage(message);
            DisplayMessage(ForeignStandard.DEBUG_MESSAGE);
            ForeignStandard.DEBUG_MESSAGE.Clear();
        }
    }

    public static void ClearLogConsole() {
        var assembly = Assembly.GetAssembly(typeof(UnityEditor.Editor));
        var type = assembly.GetType("UnityEditor.LogEntries");
        var method = type.GetMethod("Clear");
        method.Invoke(new object(), null);
    }
}
