using System.Collections;
using System.Collections.Generic;
using Players;
using Strategy.Assets.Scripts.Objects;
using Terrain;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    float speed = .01f;
    void Start(){
        transform.position = new Vector3(100, 5, 100);
    }
    void Update()
    {

        if(transform.position.y < .5f){
            transform.position = new Vector3(transform.position.x, .5f, transform.position.z);
        }
        if(Input.GetKeyDown(KeyCode.LeftShift)){
            speed = speed + .025f;
        }
        if(Input.GetKeyUp(KeyCode.LeftShift)){
            speed = speed - .025f;
        }

        if(Input.GetKeyUp(KeyCode.LeftControl)){
            speed = speed - .085f;
        }
        if(Input.GetKeyDown(KeyCode.LeftControl)){
            speed = speed + .085f;
        }

        if(Input.GetKey(KeyCode.W)){
            transform.position += new Vector3(0, 0, speed);
        }
        if(Input.GetKey(KeyCode.S)){
            transform.position += new Vector3(0, 0, -speed);
        }
        if(Input.GetKey(KeyCode.A)){
            transform.position += new Vector3(-speed, 0, 0);
        }
        if(Input.GetKey(KeyCode.D)){
            transform.position += new Vector3(speed, 0, 0);
        }
        if(Input.GetKey(KeyCode.UpArrow)){
            transform.position += new Vector3(0, speed, 0);
        }
        if(Input.GetKey(KeyCode.DownArrow)){
            transform.position += new Vector3(0, -speed, 0);
        }

    }


    public static void MoveCameraTo(Vector3 position){
        Camera.main.transform.position = position;
    }

    public static void CenterCamera(FogManager fog_manager, MapManager map_generation){
        // TO DO: REIMPLEMENT -->If player_view is at the end of the list, set player_view to 0, else increment player_view by 1
        fog_manager.ShowFogOfWar(map_generation); // Shows Fog of War for all players
        
        Vector2 coordinates = Player.GetPlayerView().GetCityByIndex(0).GetColRow();
        HexTile hexTile = HexTile.GetHexList()[(int) coordinates.x * (int) map_generation.GetMapSize().y + (int) coordinates.y];
        GameObject hex = TerrainManager.hex_to_hex_go[hexTile];
        Vector3 vector = hex.transform.position;
        vector.y += 10f;
        vector.z -= 10f;

        CameraMovement.MoveCameraTo(vector);
    }
}
