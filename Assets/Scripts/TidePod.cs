using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TidePod : MonoBehaviour
{

    float TIDEPOD_SPEED = 3f;

    public GlobalState state;

    public Vector3 direction = Vector3.up;

    bool stopped = false;

    public GameObject hitFx;

    void OnCollisionEnter2D(Collision2D col)
    {
        stopped = true;

        var hero = col.gameObject;

        var manager = GameObject.Find("Transition").GetComponent<TransitionManager>();
        hero.GetComponent<Hero>().Die();

        if (LightManager.isPlayerLightOn()) {
            LightManager.playerLightSwitch();
        }

        GetComponentInChildren<SpriteRenderer>().enabled = true;

        hero.GetComponentInChildren<Animator>().SetTrigger("KO");

        manager.Delay(() => {
            manager.FadeOut(() => {
                state.RestartLevel();
                hero.transform.position = state.GetStartPosition();
                hero.GetComponentInChildren<Animator>().SetTrigger("Idle");
                manager.FadeIn(() => {
                    hero.GetComponent<Hero>().Respawn();
                });
            });
        }, 0.5f);
    }

    void Update()
    {
        if (stopped) {
            return;
        }

        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, 0.5f);

        if (hit.collider == null || hit.collider.GetComponent<Trap>() != null) {
            transform.position += direction * Time.deltaTime * TIDEPOD_SPEED;
        } else {
            Instantiate(hitFx, transform.position + direction / 2, Quaternion.identity);
            if (direction == Vector3.up) {
                direction = Vector3.down;
            } else if (direction == Vector3.down) {
                direction = Vector3.up;
            } else if (direction == Vector3.right) {
                direction = Vector3.left;
            } else {
                direction = Vector3.right;
            }
        }
    }
}
