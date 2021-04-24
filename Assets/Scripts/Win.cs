using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Win : MonoBehaviour
{
    public string targetLevel;

    void heroWalkIn(GameObject hero)
    {
        Debug.Log("win");
        SceneManager.LoadScene(targetLevel);
    }
}
