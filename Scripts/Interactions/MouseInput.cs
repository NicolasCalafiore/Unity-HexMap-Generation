using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Terrain;

public class MouseInput : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                if(hit.transform.gameObject.name.Contains("Hex")){  
                    GameObject hex_go = hit.transform.gameObject;
                    DebugHandler.GetHexFromInput(hex_go);
     
                }
            }
        }
        
    }
}
