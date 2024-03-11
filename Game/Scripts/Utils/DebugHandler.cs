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
}
