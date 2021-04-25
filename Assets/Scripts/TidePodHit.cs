using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TidePodHit : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var px = GetComponentInChildren<ParticleSystem>();

        if (!px.IsAlive()) {
            Destroy(gameObject);
        }
    }
}
