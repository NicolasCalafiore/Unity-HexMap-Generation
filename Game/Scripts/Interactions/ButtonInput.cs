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

    public static Player player;

    public void OpenDevPanel(){
        if(UIManager.dev_panel_ui.activeSelf) UIManager.dev_panel_ui.SetActive(false);
        else UIManager.dev_panel_ui.SetActive(true);
    }

    public void OpenCabinet(){
        if(UIManager.cabinet_ui.activeSelf){
            UIManager.cabinet_ui.SetActive(false);
        }
        else{
            UIManager.cabinet_ui.SetActive(true);
            UIManager.LoadCabinetCharacters(PlayerManager.player_view);
            }
    }

    public void OpenCharacterScreen(){
        GameObject button = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;
        AbstractCharacter character = UIManager.character_binds[button.GetComponent<Button>()];
        UIManager.character_ui.SetActive(true);
        UIManager.LoadCharacterScreen(character);
    
    }

    public void CloseCity(){
        UIManager.city_ui.SetActive(false);
    }

    public void ShowRelationships(){
        TerrainManager.ShowRelationships();
    }


    public void NextPlayer(){
        PlayerManager.NextPlayer();

        UIManager.SetPlayerName(PlayerManager.player_view);
        UIManager.UpdatePlayerUI(PlayerManager.player_view);
    }

        public static void CloseCharacterScreen(){
            UIManager.character_ui.SetActive(false);
        }
}