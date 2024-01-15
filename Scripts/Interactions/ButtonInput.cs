    
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Terrain;

public class ButtonInput : MonoBehaviour
{
    public void NextPlayer(){
        CameraMovement.CenterCamera();
    }

    public void DestroyFog(){
        GameManager.fog_manager.DestroyFog();
    }

    public void CloseCityUI(){
        GameManager.ui_manager.city_ui.SetActive(false);
    }
    public void CloseHexUI(){
        GameManager.ui_manager.hex_ui.SetActive(false);
    }
    public void QuitGame(){
        Application.Quit();
    }

}