using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Outro : MonoBehaviour
{
    const float CREDITS_SPEED = 60f;

    void Update()
    {
        var rt = GetComponent<RectTransform>();
        rt.localPosition = rt.localPosition + Vector3.up * Time.deltaTime * CREDITS_SPEED;
    }
}
