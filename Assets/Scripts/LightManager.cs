using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightManager : MonoBehaviour
{
    private static bool playerLight = false;
    private static bool startLight = false;

    private static LightManager instance;

    void Awake() {
        instance = this;
    }

    void Start() {
        LightManager.RefreshLight();
    }

    public static void SetStartLight(bool state)
    {
        startLight = state;

        if (state) {
            instance.StartCoroutine("KillStartLight");
        }

        RefreshLight();
    }

    public static void playerLightSwitch()
    {
        playerLight = !playerLight;

        RefreshLight();
    }

    private static void RefreshLight()
    {
        if (playerLight || startLight) {
            LightsOn();
        } else {
            LightsOff();
        }
    }

    public static void LightsOff()
    {
        var elements = GameObject.FindObjectsOfType<MapElement>();
        foreach(var element in elements) {
            if (element.lightSensitive) {
                element.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            }
        }
    }

    public static void LightsOn()
    {
        var elements = GameObject.FindObjectsOfType<MapElement>();
        foreach(var element in elements) {
            if (element.lightSensitive) {
                element.gameObject.GetComponent<SpriteRenderer>().enabled = true;
            }
        }
    }

    IEnumerator KillStartLight() {
        yield return new WaitForSeconds(1f);
        SetStartLight(false);
    }
}
