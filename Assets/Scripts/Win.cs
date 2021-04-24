using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Win : MonoBehaviour
{
    public GlobalState state;

    void heroWalkIn(GameObject hero)
    {
        Debug.Log("win");

        var manager = GameObject.Find("Transition").GetComponent<TransitionManager>();
        hero.GetComponent<Hero>().Die();

        manager.FadeOut(() => {
            state.NextLevel();
            Debug.Log("IN");
            manager.FadeIn(() => {
                hero.GetComponent<Hero>().Respawn();
            });
        });
    }
}
