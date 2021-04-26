using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Achievements
{
    public static int socks;
    public static float time;
    public static bool deathLess;
    public static float startTime;
    public static float endTime;

    public static float getTime()
    {
        return endTime - startTime;
    }

    public static string getRunInfos()
    {
        string text = "Run completed! \n";

        if (socks == 3) {
            text += "all socks ";
        } else {
            text += "any% ";
        }

        if (deathLess) {
            text += "deathless ";
        }

        text += TimeSpan.FromSeconds(getTime()).ToString("hh\\:mm\\:ss");

        return text;
    }
}
