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
        hero.GetComponentInChildren<Animator>().SetTrigger("Victory");
        hero.GetComponent<Hero>().Lock();

        GameObject.Find("BubblesLeft").GetComponent<ParticleSystem>().Play();
        GameObject.Find("BubblesRight").GetComponent<ParticleSystem>().Play();

        if (LightManager.isPlayerLightOn()) {
            LightManager.playerLightSwitch();
        }

        manager.Delay(() => {
            manager.FadeOut(() => {
                state.NextLevel();
                manager.FadeIn(() => {
                    hero.GetComponent<Hero>().Unlock();
                    hero.GetComponentInChildren<Animator>().SetTrigger("Idle");
                });
            });
        }, 2f);
    }
}
