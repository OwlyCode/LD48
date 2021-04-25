using UnityEngine;
using UnityEngine.SceneManagement;

public class Trap : MonoBehaviour
{
    public GlobalState state;

    void heroWalkIn(GameObject hero)
    {
        var manager = GameObject.Find("Transition").GetComponent<TransitionManager>();
        hero.GetComponent<Hero>().Die();

        GetComponentInChildren<SpriteRenderer>().enabled = true;

        hero.GetComponentInChildren<Animator>().SetTrigger("Fall");

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
