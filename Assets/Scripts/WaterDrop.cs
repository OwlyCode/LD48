using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterDrop : MonoBehaviour
{
    const float DROP_SPEED = 9f;

    Vector3 target;

    bool move = false;

    public GameObject impact;

    void Start()
    {
        target = transform.position;
        ResetDrop();
    }

    void ResetDrop()
    {
        transform.position = target + Vector3.up * (10f + Random.Range(0f, 30f));
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target, DROP_SPEED * Time.deltaTime);
        if (transform.position == target) {
            Instantiate(impact, transform.position, transform.rotation);
            ResetDrop();
        }
    }
}
