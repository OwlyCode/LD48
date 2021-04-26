using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sock : MonoBehaviour
{
    public GlobalState state;

    [TextArea(15,20)]
    public string Dialog;

    public string SockIdentifier;

    void heroWalkIn(GameObject hero)
    {
        hero.GetComponentInChildren<Animator>().SetTrigger("Idle");
        state.ShowPanel(Dialog, SockIdentifier);
        Destroy(gameObject);
    }
}
