using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TransitionManager : MonoBehaviour
{
    Action callback;

    public void FadeOut(Action callback)
    {
        GetComponent<Animator>().SetTrigger("Out");
        this.callback = callback;
    }
    public void FadeIn(Action callback)
    {
        GetComponent<Animator>().SetTrigger("In");
        this.callback = callback;
    }

    public void FadeCompleted()
    {
        callback();
    }

    public void Delay(Action callback, float duration)
    {
        StopAllCoroutines();
        StartCoroutine(DoDelay(callback, duration));
    }

    IEnumerator DoDelay(Action callback, float duration = 1.5f)
    {
        yield return new WaitForSeconds(duration);

        callback();
    }
}
