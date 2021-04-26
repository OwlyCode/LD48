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
        var hero = col.gameObject;
        if (hero.GetComponent<Hero>().isInvulnerable()) {
            return;
        }

        stopped = true;

        var manager = GameObject.Find("Transition").GetComponent<TransitionManager>();
        hero.GetComponent<Hero>().Die();

        if (LightManager.isPlayerLightOn()) {
            LightManager.playerLightSwitch();
        }

        GetComponentInChildren<SpriteRenderer>().enabled = true;

        hero.GetComponentInChildren<Animator>().SetTrigger("KO");
        GetComponent<AudioSource>().Play();

        Achievements.deathLess = false;

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

        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, direction, 0.5f);

        bool rebound = false;

        foreach (var hit in hits) {
            bool shouldCross = hit.collider.GetComponent<MapElement>() == null || hit.collider.GetComponent<MapElement>().IsWalkable == true;
            rebound |= !shouldCross;
        }

        if (!rebound) {
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
