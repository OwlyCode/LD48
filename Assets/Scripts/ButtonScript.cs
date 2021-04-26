using System.Collections;
using System.Collections.Generic;
using UnityEngine.Video;
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
    public void SoundVolume()
    {
        if (Settings.enableAudio)
            Settings.enableAudio = false;
        else 
            Settings.enableAudio = true;
    }
/*    public void VideoVolume()
    {
        VideoPlayer test;

        test = Camera.main.GetComponent<VideoPlayer>();
        if (test.volume != 0)
        test.volume = 1;
        else 
        test.volume = 0;
    }*/
}
