using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SocketU : MonoBehaviour
{
    public GlobalState state;

    void heroWalkIn(GameObject hero)
    {
        GetComponent<AudioSource>().Play();

        hero.GetComponentInChildren<Animator>().SetTrigger("Victory");
        hero.GetComponent<Hero>().Win();

        GameObject.Find("Bubbles").GetComponent<ParticleSystem>().Play();

        var manager = GameObject.Find("Transition").GetComponent<TransitionManager>();
        manager.Delay(() => {
            manager.FadeOut(() => {
                SceneManager.LoadScene("Outro", LoadSceneMode.Single);
            });
        }, 2.5f);


        Achievements.endTime = Time.time;
    }
}
