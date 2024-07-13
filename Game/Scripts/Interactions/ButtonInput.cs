using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Terrain;
using UnityEngine.UI;
using TMPro;
using Players;
using Character;
using Graphics;

public class ButtonInput : MonoBehaviour
{

    public static void OpenDevPanel(){

        if(UIManager.dev_panel_ui.activeSelf) 
            UIManager.dev_panel_ui.SetActive(false);
        else 
            UIManager.dev_panel_ui.SetActive(true);
    }
    public static void OpenCabinetPanel(){

        if(UIManager.cabinet_ui.activeSelf)
            UIManager.cabinet_ui.SetActive(false);
        
        else{
            UIManager.LoadCabinetCharacters(PlayerManager.player_view);
            UIManager.cabinet_ui.SetActive(true);
            }
    }
    public static void OpenCharacterPanel(){

        GameObject button = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;
        AbstractCharacter character = UIManager.char_button_binds[button.GetComponent<Button>()];

        UIManager.LoadCharacterScreen(character);
        UIManager.character_ui.SetActive(true);
    }
    public static void NextPlayer(){

        PlayerManager.NextPlayer();
    }
    public static void CloseCharacterPanel(){

        UIManager.character_ui.SetActive(false);
        UIManager.summary_ui.SetActive(false);
    }
    public static void OpenSummaryPanel(){

        if(!UIManager.summary_ui.activeSelf){
            UIManager.LoadSummary();
            UIManager.summary_ui.SetActive(true);
        }
        else 
            UIManager.summary_ui.SetActive(false);
    }
    public static void CloseCityPanel() => UIManager.city_ui.SetActive(false);
    public static void ShowRelationships() => GraphicsManager.ShowRelationships();
    public static void DestroyAllFog() => FogManager.ClearFog();
    public static void DefenseMap() => GraphicsManager.ShowDefenseMap();
    public static void NourishmentMap() => GraphicsManager.ShowNourishmentMap();
    public static void ConstructionMap() => GraphicsManager.ShowConstructionMap();
    public static void AppealMap() => GraphicsManager.ShowAppealMap();
    public static void ClearFog() => FogManager.ClearFog();
    public static void ShowContinents() => GraphicsManager.ShowContinents();
    public static void ShowRegions() => GraphicsManager.ShowRegions();
}