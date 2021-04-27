using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Hero : MonoBehaviour
{
    const float MOVEMENT_RATIO = 4f;

    private bool lightsOn = false;

    private bool moving = false;

    private float movementAmount;

    private Vector3 target;
    private Vector3 origin;

    private Action onMoveComplete;

    bool moveRight = false;
    bool moveLeft = false;
    bool moveUp = false;
    bool moveDown = false;

    bool dead = false;
    bool locked = false;
    bool win = false;

    void Update()
    {
        if (!win && !locked && !dead && moveRight && !moving) {
            Move(Vector3.right);
            GetComponentInChildren<Animator>().SetTrigger("Right");
        }
        if (!win && !locked && !dead && moveLeft && !moving) {
            Move(Vector3.left);
            GetComponentInChildren<Animator>().SetTrigger("Left");
        }
        if (!win && !locked && !dead && moveUp && !moving) {
            Move(Vector3.up);
            GetComponentInChildren<Animator>().SetTrigger("Up");
        }
        if (!win && !locked && !dead && moveDown && !moving) {
            Move(Vector3.down);
            GetComponentInChildren<Animator>().SetTrigger("Down");
        }

        var audio = GetComponent<AudioSource>();
        if (moving) {
            if (!audio.isPlaying) {
                audio.Play();
            }
            movementAmount += Time.deltaTime * MOVEMENT_RATIO;

            transform.position = Vector3.Lerp(origin, target, movementAmount);

            if (transform.position == target) {
                moving = false;

                if (!moveLeft && !moveRight && !moveDown && !moveUp) {
                    GetComponentInChildren<Animator>().SetTrigger("Idle");
                }

                if (onMoveComplete != null) {
                    onMoveComplete();
                }
                onMoveComplete = null;
            }
        } else {
            audio.Pause();
        }
    }

    public void Die()
    {
        dead = true;
    }

    public void Respawn()
    {
        LightManager.HandleMinimum();
        dead = false;
    }

    public void Lock()
    {
        locked = true;
    }
    public void Win()
    {
        win = true;
    }
    public void ResetWin()
    {
        win = false;
    }

    public bool isInvulnerable()
    {
        return locked || dead;
    }

    public void Unlock()
    {
        locked = false;
    }

    public void Freeze()
    {
        moving = false;
    }

    public void OnMoveRight()
    {
        moveRight = !moveRight;
    }

    public void OnMoveLeft()
    {
        moveLeft = !moveLeft;
    }

    public void OnMoveUp()
    {
        moveUp = !moveUp;
    }

    public void OnMoveDown()
    {
        moveDown = !moveDown;
    }

    public void OnToggleLight()
    {
        if (win) {
            return;
        }

        if (dead || locked) {
            var state = GameObject.Find("GlobalState").GetComponent<GlobalState>();

            if (state.IsDialogSpeeded() || !state.IsTyping()) {
                state.HidePanel();
            } else {
                state.SpeedDialog();
            }
            return; // isDialogSpeeded
        }

        Achievements.lightLess = false;

        LightManager.playerLightSwitch();
        transform.Find("LightAudioSource").GetComponent<AudioSource>().Play();
    }

    private void Move(Vector3 offset)
    {
        if (moving || dead || locked || win) {
            return;
        }

        LightManager.SetStartLight(false);
        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, offset, 1.0f);

        foreach (var hit in hits) {
            if (hit.collider != null) {

                var mapElement = hit.collider.gameObject.GetComponent<MapElement>();

                if (mapElement == null || !mapElement.IsWalkable) {
                    GetComponentInChildren<Animator>().SetTrigger("Idle");
                    return;
                }
            }
        }

        onMoveComplete = () => {
            foreach (var hit in hits) {
                if (hit.collider != null) {
                    hit.collider.gameObject.SendMessage("heroWalkIn", gameObject, SendMessageOptions.DontRequireReceiver);
                }
            }
        };

        var grid = transform.parent.GetComponent<Grid>();
        target = grid.GetCellCenterLocal(grid.WorldToCell(transform.position + offset));
        origin = transform.position;
        movementAmount = 0f;
        moving = true;
    }
}
