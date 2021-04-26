using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuLogic : MonoBehaviour
{
    void Update()
    {
        if (Settings.enableAudio)
            AudioListener.volume = 1;
        else
            AudioListener.volume = 0;

    }
    public void StartGame()
    {
        SceneManager.LoadScene("Intro");
    }

    public void Exit()
    {
        Application.Quit();
    }
}
