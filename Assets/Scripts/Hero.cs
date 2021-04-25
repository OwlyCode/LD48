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

    void Update()
    {
        if (!locked && !dead && moveRight && !moving) {
            Move(Vector3.right);
            GetComponentInChildren<Animator>().SetTrigger("Right");
        }
        if (!locked && !dead && moveLeft && !moving) {
            Move(Vector3.left);
            GetComponentInChildren<Animator>().SetTrigger("Left");
        }
        if (!locked && !dead && moveUp && !moving) {
            Move(Vector3.up);
            GetComponentInChildren<Animator>().SetTrigger("Up");
        }
        if (!locked && !dead && moveDown && !moving) {
            Move(Vector3.down);
            GetComponentInChildren<Animator>().SetTrigger("Down");
        }

        if (moving) {
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
        }
    }

    public void Die()
    {
        dead = true;
    }

    public void Respawn()
    {
        dead = false;
    }

    public void Lock()
    {
        locked = true;
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
        if (dead || locked) {
            return;
        }

        LightManager.playerLightSwitch();
        transform.Find("LightAudioSource").GetComponent<AudioSource>().Play();
    }

    private void Move(Vector3 offset)
    {
        if (moving || dead || locked) {
            return;
        }

        LightManager.SetStartLight(false);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, offset, 1.0f);

        if (hit.collider != null) {

            var mapElement = hit.collider.gameObject.GetComponent<MapElement>();

            if (mapElement == null || !mapElement.IsWalkable) {
                GetComponentInChildren<Animator>().SetTrigger("Idle");
                return;
            }
        }

        onMoveComplete = () => {
            if (hit.collider != null) {
                hit.collider.gameObject.SendMessage("heroWalkIn", gameObject, SendMessageOptions.DontRequireReceiver);
            }
        };

        var grid = transform.parent.GetComponent<Grid>();
        target = grid.GetCellCenterLocal(grid.WorldToCell(transform.position + offset));
        origin = transform.position;
        movementAmount = 0f;
        moving = true;
    }
}
