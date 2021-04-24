using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LightManager : MonoBehaviour
{
    const float MAX_BATTERY = 6f;
    const float START_LIGHT_DURATION = 5f;

    private static bool playerLight = false;
    private static bool startLight = false;

    private static float batteryLife = MAX_BATTERY;

    private static LightManager instance;

    void Awake() {
        instance = this;
    }

    public static void SetStartLight(bool state)
    {
        startLight = state;

        if (state) {
            instance.StopAllCoroutines();
            instance.StartCoroutine("KillStartLight");
        }

        RefreshLight();
    }

    public static void playerLightSwitch()
    {
        playerLight = !playerLight;

        if (batteryLife <= 0f) {
            playerLight = false;
        }

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
        GameObject.Find("LightShade").GetComponent<Image>().enabled = true;
        var elements = GameObject.FindObjectsOfType<MapElement>();
        foreach(var element in elements) {
            if (element.lightSensitive) {
                element.gameObject.GetComponentInChildren<SpriteRenderer>().enabled = false;
            }
        }
    }

    public static void LightsOn()
    {
        GameObject.Find("LightShade").GetComponent<Image>().enabled = false;
        var elements = GameObject.FindObjectsOfType<MapElement>();
        foreach(var element in elements) {
            if (element.lightSensitive) {
                element.gameObject.GetComponentInChildren<SpriteRenderer>().enabled = true;
            }
        }
    }

    void FixedUpdate()
    {
        if (playerLight) {
            batteryLife -= Time.fixedDeltaTime;

            if (batteryLife <= 0f) {
                playerLight = false;
                LightManager.RefreshLight();
            }
        }
    }

    public static float GetBatteryRate()
    {
        return batteryLife / MAX_BATTERY;
    }

    IEnumerator KillStartLight() {
        yield return new WaitForSeconds(START_LIGHT_DURATION);
        SetStartLight(false);
    }
}
