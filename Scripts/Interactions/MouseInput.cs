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
                    GameManager.ui_manager.CloseHexUI();
                    GameManager.ui_manager.GetHexInformation(hex_go);
     
                }
                if(hit.transform.gameObject.tag.Contains("City")){  
                    GameObject city = hit.transform.gameObject;
                    GameManager.ui_manager.CloseCityUI();  
                    GameManager.ui_manager.GetCityInformation(city);
                }
            }
        }
        
    }
}
