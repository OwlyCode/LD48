using UnityEngine;
using UnityEngine.SceneManagement;

public class Trap : MonoBehaviour
{
    public GlobalState state;

    void heroWalkIn(GameObject hero)
    {
        var manager = GameObject.Find("Transition").GetComponent<TransitionManager>();
        hero.GetComponent<Hero>().Die();

        if (LightManager.isPlayerLightOn()) {
            LightManager.playerLightSwitch();
        }

        GetComponentInChildren<SpriteRenderer>().enabled = true;

        hero.GetComponentInChildren<Animator>().SetTrigger("Fall");

        Achievements.deathLess = false;

        GetComponent<AudioSource>().Play();

        manager.Delay(() => {
            manager.FadeOut(() => {
                state.RestartLevel();
                hero.transform.position = state.GetStartPosition();
                hero.GetComponentInChildren<Animator>().SetTrigger("Idle");
                manager.FadeIn(() => {
                    hero.GetComponent<Hero>().Respawn();
                });
            });
        }, 2f);
    }
}
