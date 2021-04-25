using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Battery : MonoBehaviour
{
    public GlobalState state;

    void heroWalkIn(GameObject hero)
    {
        LightManager.Refill();
        Destroy(gameObject);

        GameObject.Find("GlobalState").GetComponent<GlobalState>().DisableBattery();
    }
}
