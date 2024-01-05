using System;
using System.Collections;
using System.Collections.Generic;
using Players;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerManager
{
    /*
        PlayerManager is used to manipulate the players in the game
    */
    public delegate void PlayersCreatedEventHandler(object sender, EventArgs e);
    public event PlayersCreatedEventHandler PlayersCreated;

    public List<Player> player_list = new List<Player>();
    public PlayerManager(){
        
    }

    public void GeneratePlayers(int num_players){
        for(int i = 0; i < num_players; i++){
            player_list.Add(new Player("player", "Player " + i, Color.red, i));
        }
    }

}
