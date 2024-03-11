using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace Terrain
{
    public static class GovernmentEnums
    {

        public enum GovernmentType{
            None,
            Democracy,
            Monarchy,
            Dictatorship,
            Theocracy,
            Tribalism,

        }

        public static List<GovernmentType> GetGovernmentTypes() => Enum.GetValues(typeof(GovernmentType)).Cast<GovernmentType>().ToList();
        public static GovernmentType GetGovernmentType(float governmentValue) => (GovernmentType)Enum.Parse(typeof(GovernmentType), governmentValue.ToString());

    }
}