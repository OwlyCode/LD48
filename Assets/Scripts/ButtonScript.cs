using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class ButtonScript : MonoBehaviour
{
    public void ClosePanel(GlobalState GState)
    {
        GState.HidePanel();
    }
    public void QuitTheGame() 
    {
        Application.Quit();
    }
    public void ResetGame()
    {
        SceneManager.LoadScene("Game");
        LightManager.RefillMax();
    }
    public void ResetMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
