using System.Collections;
using System.Collections.Generic;
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

    public static void CenterCamera(){
        GameManager.player_view = GameManager.player_view == GameManager.player_manager.player_list.Count - 1 ? 0 : GameManager.player_view + 1; // If player_view is at the end of the list, set player_view to 0, else increment player_view by 1
        GameManager.fog_manager.ShowFogOfWar(GameManager.player_manager.player_list[GameManager.player_view], GameManager.map_size); // Shows Fog of War for all players
        Vector2 coordinates = GameManager.player_manager.player_list[GameManager.player_view].GetCityByIndex(0).GetColRow();
        HexTile hexTile = GameManager.hex_list[(int) coordinates.x * (int) GameManager.map_size.y + (int) coordinates.y];
        GameObject hex = TerrainHandler.hex_to_hex_go[hexTile];
        Vector3 vector = hex.transform.position;
        vector.y += 10f;
        vector.z -= 10f;

        CameraMovement.MoveCameraTo(vector);
    }
}
