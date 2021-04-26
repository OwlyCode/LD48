using System.Collections;
using System.Collections.Generic;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

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
        if (Settings.enableAudio) {
            Settings.enableAudio = false;
        }
        else {
            Settings.enableAudio = true;
        }

        RefreshSound();
    }

    public void RefreshSound()
    {
        var text = GameObject.Find("SoundText");
        string label = "";

        if (!Settings.enableAudio) {
            label = "Sound: off";
        }
        else {
            label = "Sound: on";
        }

        if (text == null) {
            return;
        }

        if (text.GetComponent<Text>() != null) {
            text.GetComponent<Text>().text = label;
        }
        if (text.GetComponent<TextMesh>() != null) {
            text.GetComponent<Text>().text = label;
        }
    }

    void Start()
    {
        RefreshSound();
    }
}
