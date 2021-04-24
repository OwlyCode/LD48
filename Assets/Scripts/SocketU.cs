using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SocketU : MonoBehaviour
{
    public GlobalState state;

    void heroWalkIn(GameObject hero)
    {
        Debug.Log("WinSock");
        SceneManager.LoadScene("WinSlate", LoadSceneMode.Single);

    }
}
