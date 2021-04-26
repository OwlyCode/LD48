using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterDropImpact : MonoBehaviour
{
    void AnimationComplete()
    {
        Destroy(gameObject);
    }
}
