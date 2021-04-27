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
        hero.GetComponent<Hero>().Win();

        GameObject.Find("Bubbles").GetComponent<ParticleSystem>().Play();

        if (LightManager.isPlayerLightOn()) {
            LightManager.playerLightSwitch();
        }

        GetComponent<AudioSource>().Play();

        manager.Delay(() => {
            manager.FadeOut(() => {
                state.NextLevel();
                manager.FadeIn(() => {
                    hero.GetComponent<Hero>().ResetWin();
                    hero.GetComponentInChildren<Animator>().SetTrigger("Idle");
                });
            });
        }, 2.5f);
    }
}
