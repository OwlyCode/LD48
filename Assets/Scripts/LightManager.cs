using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LightManager : MonoBehaviour
{
    const float MAX_BATTERY = 6f;
    const float START_LIGHT_DURATION = 0f;

    const float BATTERY_REFILL_AMOUNT = 3f;

    const float BATTERY_MIN = 1f;

    private static bool playerLight = false;
    private static bool startLight = false;

    private static float batteryLife = MAX_BATTERY;

    private static LightManager instance;

    void Awake() {
        instance = this;
    }

    public static void HandleMinimum()
    {
        if (batteryLife < BATTERY_MIN) {
            batteryLife = BATTERY_MIN;
        }
    }

    public static void Refill()
    {
        batteryLife += BATTERY_REFILL_AMOUNT;

        if (batteryLife > MAX_BATTERY) {
            batteryLife = MAX_BATTERY;
        }

        GameObject.Find("BatteryUI").GetComponent<BatteryUI>().ResetWarning();
    }
    public static void RefillMax()
    {
        batteryLife = MAX_BATTERY;

        GameObject.Find("BatteryUI").GetComponent<BatteryUI>().ResetWarning();
    }

    public static void SetStartLight(bool state)
    {
        if (startLight == state) {
            return;
        }

        startLight = state;

        if (state) {
            instance.StopAllCoroutines();
            instance.StartCoroutine("KillStartLight");
        }

        RefreshLight();
    }


    public static bool isPlayerLightOn()
    {
        return playerLight;
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
        GameObject.Find("LightShade").GetComponent<SpriteRenderer>().enabled = true;
        var elements = GameObject.FindObjectsOfType<MapElement>();
        foreach(var element in elements) {
            if (element.lightSensitive) {
                var sr = element.gameObject.GetComponentInChildren<SpriteRenderer>();
                var an = element.gameObject.GetComponentInChildren<Animator>();

                if (sr != null)
                    sr.enabled = false;

                if (an != null)
                    an.enabled = false;
            }
        }
    }

    public static void LightsOn()
    {
        GameObject.Find("LightShade").GetComponent<SpriteRenderer>().enabled = false;
        var elements = GameObject.FindObjectsOfType<MapElement>();
        foreach(var element in elements) {
            if (element.lightSensitive) {
                var sr = element.gameObject.GetComponentInChildren<SpriteRenderer>();
                var an = element.gameObject.GetComponentInChildren<Animator>();

                if (sr != null)
                    sr.enabled = true;

                if (an != null)
                    an.enabled = true;
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
