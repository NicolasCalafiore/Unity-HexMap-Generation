using UnityEngine;
using Terrain;
using System.Collections.Generic;
using System;
using Random = UnityEngine.Random;
using Cabinet;
using PlayerGovernment;
using Unity.VisualScripting;
using Character;
using static Terrain.GovernmentEnums;
using Cities;
using Diplomacy;
using Graphics;

namespace Players {
    public static class PlayerUtils {

        public static bool HasSameCapitalContinent(Player player1, Player player2){
            return CityManager.city_to_hex[player1.GetCapital()].continent_id == CityManager.city_to_hex[player2.GetCapital()].continent_id;
        }
    }
}