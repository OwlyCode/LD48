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

        manager.FadeOut(() => {
            state.RestartLevel();
            hero.transform.position = state.GetStartPosition();
            manager.FadeIn(() => {
                hero.GetComponent<Hero>().Respawn();
            });
        });
    }
}
