using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sock : MonoBehaviour
{
    bool active = true;

    public GlobalState state;

    [TextArea(15,20)]
    public string Dialog;

    public string SockIdentifier;
    public string SockUI;

    void heroWalkIn(GameObject hero)
    {
        if (!active) {
            return;
        }

        active = false;

        GameObject.Find(SockUI).GetComponent<Image>().enabled = true;
        GetComponent<AudioSource>().Play();

        Achievements.socks++;

        hero.GetComponentInChildren<Animator>().SetTrigger("Idle");
        state.ShowPanel(Dialog, SockIdentifier);
        GetComponent<Sock>().enabled = false;
        GetComponentInChildren<SpriteRenderer>().enabled = false;
        GetComponentInChildren<SpriteMask>().enabled = false;
    }
}
