using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{
    const float MOVEMENT_RATIO = 1f;

    private bool lightsOn = false;

    private bool moving = false;

    private float movementAmount;

    private Vector3 target;

    void Start()
    {
        // TEMP
        LightManager.SetStartLight(true);
    }

    void Update()
    {
        if (moving) {
            movementAmount += Time.deltaTime * MOVEMENT_RATIO;

            transform.position = Vector3.Lerp(transform.position, target, movementAmount);

            if (transform.position == target) {
                moving = false;
            }
        }
    }

    public void OnMoveRight()
    {
        Move(Vector3.right);
    }

    public void OnMoveLeft()
    {
        Move(Vector3.left);
    }

    public void OnMoveUp()
    {
        Move(Vector3.up);
    }

    public void OnMoveDown()
    {
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
            hit.collider.gameObject.SendMessage("heroWalkIn", gameObject, SendMessageOptions.DontRequireReceiver);

            var mapElement = hit.collider.gameObject.GetComponent<MapElement>();

            if (mapElement == null || !mapElement.IsWalkable) {
                return;
            }
        }

        var grid = transform.parent.GetComponent<Grid>();
        target = grid.GetCellCenterLocal(grid.WorldToCell(transform.position + offset));
        movementAmount = 0f;
        moving = true;
    }
}
