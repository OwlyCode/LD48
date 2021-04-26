using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sock : MonoBehaviour
{
    public GlobalState state;

    [TextArea(15,20)]
    public string Dialog;

    public string SockIdentifier;
    public string SockUI;

    void heroWalkIn(GameObject hero)
    {
        GameObject.Find(SockUI).GetComponent<Image>().enabled = true;

        Achievements.socks++;

        hero.GetComponentInChildren<Animator>().SetTrigger("Idle");
        state.ShowPanel(Dialog, SockIdentifier);
        Destroy(gameObject);
    }
}
