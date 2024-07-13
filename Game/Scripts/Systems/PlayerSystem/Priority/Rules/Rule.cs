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
using System.Linq;
using static Terrain.RegionsEnums;
using static Terrain.PriorityEnums;
using AI;
using static Terrain.ForeignEnums;


namespace AI {
    public class Rule {
        private Dictionary<MainPriority, float> priorities = new Dictionary<MainPriority, float>();
        private Dictionary<GovernmentType, float> governmentEffects = new Dictionary<GovernmentType, float>();
        private Dictionary<float, float> distanceEffects = new Dictionary<float, float>();
        private Dictionary<float, float> MustBeGreaterThan = new Dictionary<float, float>();
        private Dictionary<List<bool>, float> conditions = new Dictionary<List<bool>, float>();
        public string name;

        public float GetSum(){
            float sum = 0;
            sum += GetConditionsSum();

            return sum;
        }

public float GetConditionsSum()
{
    float sum = 0;

    foreach (var conditionPair in conditions)
    {
        List<bool> condition = conditionPair.Key;
        float value = conditionPair.Value;
        bool conditionMet = true;

        foreach (bool cond in condition)
        {
            if (!cond)
            {
                conditionMet = false;
                break;
            }
        }

        if (conditionMet)
        {
            sum += value;
            Debug.Log($"Condition met. Value: {value} added. Current Sum: {sum}");
        }
        else
        {
            Debug.Log("Condition not met. Skipping.");
        }
    }

    Debug.Log($"Final Sum: {sum}");
    return sum;
}

        public void AddCondition(List<bool> condition, float value){
            conditions.Add(condition, value);
        }

        
    }
}