using System;
using System.Collections;
using System.Collections.Generic;
using Strategy.Assets.Game.Scripts.Terrain;
using Strategy.Assets.Game.Scripts.Terrain.Water;
using Strategy.Assets.Game.Scripts.Terrain.Regions;
using Unity.VisualScripting;
using UnityEngine;




namespace Terrain {

    public class TurnManager : MonoBehaviour
    {
        /*
            TurnManager is used to manage the turns of the game
        */

        public void EndTurn(){
            Debug.Log("End Turn");
        }

    }
}