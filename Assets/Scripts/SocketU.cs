using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SocketU : MonoBehaviour
{
    public GlobalState state;

    void heroWalkIn(GameObject hero)
    {
        var manager = GameObject.Find("Transition").GetComponent<TransitionManager>();
        manager.FadeOut(() => {
            SceneManager.LoadScene("Outro", LoadSceneMode.Single);
        });
    }
}
