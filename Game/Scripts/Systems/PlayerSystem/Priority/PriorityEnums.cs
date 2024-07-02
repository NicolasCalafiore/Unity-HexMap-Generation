using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace Terrain
{
    public static class PriorityEnums
    {

        public enum PlayerPriority{
        Stability,
        Religion,
        Economy,
        Science,
        Nourishment,
        Production,
        Defense,
        }

        public static List<PlayerPriority> GetPlayerPriorities(){
            return Enum.GetValues(typeof(PlayerPriority)).Cast<PlayerPriority>().ToList();
        }


    }
}