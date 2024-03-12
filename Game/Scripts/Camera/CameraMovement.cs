using System.Collections;
using System.Collections.Generic;
using Players;
using Cities;
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
        
        FogManager.ShowFogOfWar();
        Vector2 coordinates = PlayerManager.player_view.GetCapitalCoordinate();
        HexTile hexTile = HexManager.hex_list[(int) coordinates.x * (int) MapManager.GetMapSize().y + (int) coordinates.y];
        GameObject hex = TerrainManager.hex_to_hex_go[hexTile];
        Vector3 vector = hex.transform.position;
        vector.y += 5f;
        vector.z -= 5f;


        CameraMovement.MoveCameraTo(vector);
    }
}
