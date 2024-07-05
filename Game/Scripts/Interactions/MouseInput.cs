using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Terrain;
using System.Linq;
using Players;
using Cities;
using Graphics;

public class MouseInput : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButtonDown(0)){
            LeftClick();
        }
    }

    private void LeftClick(){
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if(Physics.Raycast(ray, out hit))

            if(hit.transform.gameObject.tag.Contains("City"))
                CityMethod(hit);
                
            else if(hit.transform.gameObject.name.Contains("Hex"))
                HexMethod(hit);
    }
    private void CityMethod(RaycastHit hit){
        GameObject game_object = hit.transform.gameObject;
        
        GameObject city_go = game_object.transform.parent.gameObject;
        City city = GraphicsManager.city_go_to_city[city_go];

        UIManager.city_ui.SetActive(true);
        PlayerManager.SetPlayerView(city.owner_player);

        if(city.owner_player != PlayerManager.player_view)
            PlayerManager.SetPlayerView(city.owner_player);
    }
    private void HexMethod(RaycastHit hit){
        GameObject hex_go = hit.transform.parent.gameObject;
        HexTile hexTile = HexGraphicManager.hex_go_to_hex[hex_go];
        UIManager.LoadHexUI(hexTile);
        UIManager.hex_ui.SetActive(true);
    }
}
