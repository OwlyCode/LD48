using UnityEngine;
using UnityEngine.SceneManagement;

public class Trap : MonoBehaviour
{
    public GlobalState state;

    void heroWalkIn(GameObject hero)
    {
        Debug.Log("Trap");
        var manager = GameObject.Find("Transition").GetComponent<TransitionManager>();
        hero.GetComponent<Hero>().Die();

        GetComponentInChildren<SpriteRenderer>().enabled = true;

        hero.GetComponent<Animator>().SetTrigger("Fall");

        manager.FadeOut(() => {
            state.RestartLevel();
            hero.transform.position = state.GetStartPosition();
            hero.GetComponent<Animator>().SetTrigger("Respawn");
            manager.FadeIn(() => {
                hero.GetComponent<Hero>().Respawn();
            });
        });
    }
}
