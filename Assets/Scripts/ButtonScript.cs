using System.Collections;
using System.Collections.Generic;
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
    public void Reset(GlobalState GState)
    {
        GState.RestartLevel();
    }
}
