using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Battery : MonoBehaviour
{
    public GlobalState state;
    bool picked = false;

    void heroWalkIn(GameObject hero)
    {
        if (picked) {
            return;
        }

        picked = true;

        GetComponent<SpriteRenderer>().enabled = false;

        LightManager.Refill();

        GameObject.Find("GlobalState").GetComponent<GlobalState>().DisableBattery();
        GetComponent<AudioSource>().Play();
    }
}
