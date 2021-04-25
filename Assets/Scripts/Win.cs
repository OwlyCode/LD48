using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Win : MonoBehaviour
{
    public GlobalState state;

    void heroWalkIn(GameObject hero)
    {
        var manager = GameObject.Find("Transition").GetComponent<TransitionManager>();
        hero.GetComponent<Hero>().Lock();

        manager.FadeOut(() => {
            state.NextLevel();
            manager.FadeIn(() => {
                hero.GetComponent<Hero>().Unlock();
            });
        });
    }
}
