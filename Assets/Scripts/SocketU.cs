using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SocketU : MonoBehaviour
{
    public GlobalState state;

    void heroWalkIn(GameObject hero)
    {
        SceneManager.LoadScene("Outro", LoadSceneMode.Single);
    }
}
