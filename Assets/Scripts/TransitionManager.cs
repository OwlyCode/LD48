using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TransitionManager : MonoBehaviour
{
    public void FadeOut(Action callback)
    {
        GetComponent<Animator>().SetTrigger("Out");
        StopAllCoroutines();
        StartCoroutine(AfterAnimation(callback));
    }
    public void FadeIn(Action callback)
    {
        GetComponent<Animator>().SetTrigger("In");
        StopAllCoroutines();
        StartCoroutine(AfterAnimation(callback));
    }

    IEnumerator AfterAnimation(Action callback)
    {
        yield return new WaitForSeconds(1.5f);

        callback();
    }
}
