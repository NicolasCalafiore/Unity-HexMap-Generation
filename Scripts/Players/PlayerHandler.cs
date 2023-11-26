using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Strategy.Assets.Scripts.Objects;

public class PlayerHandler : MonoBehaviour
{
    [SerializeField] private int player_number;
    private static List<Player> players = new List<Player>();
    void Start()
    {
        for(int i = 0; i < 4; i++)
        {
            players.Add(new Player(i));
            players[i].AddCity(new City("City " + i.ToString(), i, 0));
        }
    }





}
