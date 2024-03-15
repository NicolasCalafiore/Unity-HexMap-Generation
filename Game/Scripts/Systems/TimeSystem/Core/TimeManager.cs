using UnityEngine;
using System.Collections;

namespace Terrain {
    public class TimeManager : MonoBehaviour{

        void Start()
        {
            StartCoroutine(LogEverySecond());
        }

        IEnumerator LogEverySecond()
        {
            while (true)
            {
                yield return new WaitForSeconds(1);
                UIManager.UpdateSeconds();
            }
        }
    }
}