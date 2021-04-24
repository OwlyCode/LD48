using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Hero : MonoBehaviour
{
    const float MOVEMENT_RATIO = 0.2f;

    private bool lightsOn = false;

    private bool moving = false;

    private float movementAmount;

    private Vector3 target;

    private Action onMoveComplete;

    bool moveRight = false;
    bool moveLeft = false;
    bool moveUp = false;
    bool moveDown = false;

    void Update()
    {
        if (moveRight && !moving) {
            Move(Vector3.right);
        }
        if (moveLeft && !moving) {
            Move(Vector3.left);
        }
        if (moveUp && !moving) {
            Move(Vector3.up);
        }
        if (moveDown && !moving) {
            Move(Vector3.down);
        }

        if (moving) {
            movementAmount += Time.deltaTime * MOVEMENT_RATIO;

            transform.position = Vector3.Lerp(transform.position, target, movementAmount);

            if (transform.position == target) {
                moving = false;
                if (onMoveComplete != null) {
                    onMoveComplete();
                }
                onMoveComplete = null;
            }
        }
    }

    public void Freeze()
    {
        moving = false;
    }

    public void OnMoveRight()
    {
        moveRight = !moveRight;
        //Move(Vector3.right);
    }

    public void OnMoveLeft()
    {
        moveLeft = !moveLeft;
        Move(Vector3.left);
    }

    public void OnMoveUp()
    {
        moveUp = !moveUp;
        Move(Vector3.up);
    }

    public void OnMoveDown()
    {
        moveDown = !moveDown;
        Move(Vector3.down);
    }

    public void OnToggleLight()
    {
        LightManager.playerLightSwitch();
    }

    private void Move(Vector3 offset)
    {
        if (moving) {
            return;
        }

        LightManager.SetStartLight(false);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, offset, 1.0f);

        if (hit.collider != null) {

            var mapElement = hit.collider.gameObject.GetComponent<MapElement>();

            if (mapElement == null || !mapElement.IsWalkable) {
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
        movementAmount = 0f;
        moving = true;
    }
}
