    
    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Terrain;

public class ButtonInput : MonoBehaviour
{
    public void NextPlayer(){

        GameManager.player_view = GameManager.player_view == GameManager.player_manager.player_list.Count - 1 ? 0 : GameManager.player_view + 1; // If player_view is at the end of the list, set player_view to 0, else increment player_view by 1
        GameManager.fog_manager.ShowFogOfWar(GameManager.player_manager.player_list[GameManager.player_view], GameManager.map_size); // Shows Fog of War for all players
        Vector2 coordinates = GameManager.player_manager.player_list[GameManager.player_view].GetCity(0).GetColRow();
        HexTile hexTile = GameManager.hex_list[(int) coordinates.x * (int) GameManager.map_size.y + (int) coordinates.y];
        GameObject hex = TerrainHandler.hex_to_hex_go[hexTile];
        Vector3 vector = hex.transform.position;
        vector.y += 10f;
        vector.z -= 10f;

        CameraMovement.MoveCameraTo(vector);
    }

    public void DestroyFog(){
        GameManager.fog_manager.DestroyFog();
    }

}