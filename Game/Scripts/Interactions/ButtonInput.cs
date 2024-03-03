    
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
    public static Dictionary<Button, ICharacter> character_binds = new Dictionary<Button, ICharacter>();
    public static Player player;

    public void NextPlayer(){
        Player.NextPlayer();
        CameraMovement.CenterCamera();
        TerrainManager.SpawnAIFlags();
    }

    public void DestroyFog(){
            FogManager.DestroyFog();
    }

    public void ShowRelationships(){
        Debug.Log("Showing All Relationships");
        TerrainManager.ShowRelationships();
    }

    public void ShowPlayerMenu(){
        UIManager.ShowPlayerMenu(player);
    }

    public void ShowCharacterMenu(){
        GameObject button = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;
        Debug.Log("Showing Character Menu");
        Debug.Log(character_binds[button.GetComponent<Button>()].GetFullName());
        UIManager.ShowCharacterMenu(character_binds[button.GetComponent<Button>()]);
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