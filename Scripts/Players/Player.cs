using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Strategy.Assets.Scripts.Objects;

public class Player{

    private string name;
    private int player_number;
    List<City> cities = new List<City>();

    public Player(int player_number)
    {
        this.player_number = player_number;
        this.name = "Player " + player_number;
    }

    public void AddCity(City city)
    {
        string city_name = name + "'s city " + cities.Count.ToString();
        cities.Add(new City(city_name, player_number, cities.Count));
    }   
}
