using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace Terrain
{
    public static class GovernmentEnums
    {
        public static List<GovernmentType> GetGovernmentTypes()
        {
            return Enum.GetValues(typeof(GovernmentType)).Cast<GovernmentType>().ToList();
        }

        public static GovernmentType GetGovernmentType(float governmentValue)
        {
            return governmentDict.TryGetValue(governmentValue, out var government) ? government : default;
        }

        private static readonly Dictionary<float, GovernmentType> governmentDict = new Dictionary<float, GovernmentType>
        {
            { (int) GovernmentType.None, GovernmentType.None },
            { (int) GovernmentType.Democracy, GovernmentType.Democracy },
            { (int) GovernmentType.Monarchy, GovernmentType.Monarchy },
            { (int) GovernmentType.Dictatorship, GovernmentType.Dictatorship },
            { (int) GovernmentType.Theocracy, GovernmentType.Theocracy },
            { (int) GovernmentType.Tribalism, GovernmentType.Tribalism },
        };

        public enum GovernmentType{
            None,
            Democracy,
            Monarchy,
            Dictatorship,
            Theocracy,
            Tribalism,

        }
    }
}