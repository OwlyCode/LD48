using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
using System;

public class Intro : MonoBehaviour
{
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
        SceneManager.LoadScene("Game", LoadSceneMode.Single);
    }
}
