    
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Terrain;
using UnityEngine.UI;
using TMPro;
using Players;
using Character;

public class ButtonInput : MonoBehaviour
{
    public static Dictionary<Button, ICharacter> button_binds = new Dictionary<Button, ICharacter>();

//PlayerManager player_manager, FogManager fog_manager, MapGeneration map_generation, HexManager hex_manager
    public void NextPlayer(){
        Player.NextPlayer();
        CameraMovement.CenterCamera(GameManager.fog_manager, GameManager.map_manager);
        TerrainManager.SpawnAIFlags();
    }

    public void DestroyFog(){
        // GameManager.fog_manager.DestroyFog();
    }

    public void CloseCityUI(){
        // GameManager.ui_manager.city_ui.SetActive(false);
        // button_binds.Clear();
    }
    public void CloseHexUI(){
        // GameManager.ui_manager.hex_ui.SetActive(false);
    }
    public void CloseCharacterUI(){
        // GameManager.ui_manager.character_ui.SetActive(false);
    }
    public void QuitGame(){
        // Application.Quit();
    }

    public void GetCharacterInformation(){
        // GameObject go = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;
        // ICharacter character = button_binds[go.GetComponent<Button>()];

        // GameManager.ui_manager.GetCharacterInformation(character);
    }

}