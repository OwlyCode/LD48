using UnityEngine;
using UnityEngine.SceneManagement;

public class Trap : MonoBehaviour
{
    void heroWalkIn(GameObject hero)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
