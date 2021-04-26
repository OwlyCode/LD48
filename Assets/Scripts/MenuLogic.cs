using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuLogic : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("Intro");
    }

    public void Exit()
    {
        Application.Quit();
    }
}
