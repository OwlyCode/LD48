using UnityEngine;
using UnityEngine.SceneManagement;

public class Trap : MonoBehaviour
{
    public GlobalState state;

    void heroWalkIn(GameObject hero)
    {
        Debug.Log("Trap");

        hero.transform.position = state.GetStartPosition();

        state.RestartLevel();
    }
}
