using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RunDisplay : MonoBehaviour
{
    const float CREDITS_SPEED = 60f;

    float cooldown = 6f;

    void Start()
    {
        GetComponent<Text>().text = Achievements.getRunInfos();
    }

    void Update()
    {
        if (cooldown > 0f) {
            cooldown -= Time.deltaTime;
            return;
        }


        var rt = GetComponent<RectTransform>();
        rt.localPosition = rt.localPosition + Vector3.up * Time.deltaTime * CREDITS_SPEED;
    }
}
