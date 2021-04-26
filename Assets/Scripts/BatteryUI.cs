using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BatteryUI : MonoBehaviour
{
    public Image Bar;
    public Image Background;

    public Color emptyColor;

    Color backgroundColor;

    bool emptied = false;

    bool warned = false;

    // Start is called before the first frame update
    void Start()
    {
        backgroundColor = Background.color;
    }

    public void ResetWarning()
    {
        warned = false;
    }

    void Update()
    {
        var rate = LightManager.GetBatteryRate();

        Bar.fillAmount = rate;

        if (rate < 0.33f && !warned) {
            warned = true;

            transform.Find("LowBatteryAudio").GetComponent<AudioSource>().Play();
            GetComponent<Animator>().SetTrigger("LowBattery");
        }

        if (rate <= 0f) {
            Background.color = emptyColor;
            if (!emptied) {
                GetComponent<AudioSource>().Play();
            }
            emptied = true;
        } else {
            Background.color = backgroundColor;
        }
    }
}
