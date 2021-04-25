using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
using System;

public class Intro : MonoBehaviour
{
    private bool hasSkipped = false;

    void Update()
    {
        var VP = Camera.main.gameObject.GetComponent<VideoPlayer>();
        long playerCurrentFrame = VP.GetComponent<VideoPlayer>().frame;
        long playerFrameCount = Convert.ToInt64(VP.GetComponent<VideoPlayer>().frameCount);

        if (playerCurrentFrame >= playerFrameCount - 2) {
            Skip();
        }
    }

    public void Skip()
    {
        if (hasSkipped) {
            return;
        }

        hasSkipped = true;
        GameObject.Find("Transition").GetComponent<TransitionManager>().FadeOut(() => {
            SceneManager.LoadScene("Game", LoadSceneMode.Single);
        });
    }
}
