using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace Terrain
{
    public static class PriorityEnums
    {

        public enum MainPriority{
        Stability,
        Religion,
        Economy,
        Science,
        Nourishment,
        Production,
        Defense,
        }


        public static List<MainPriority> GetPlayerPriorities(){
            return Enum.GetValues(typeof(MainPriority)).Cast<MainPriority>().ToList();
        }


    }
}