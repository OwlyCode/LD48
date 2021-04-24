using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{
    private bool lightsOn = false;

    void Start() {
        RefreshLights();
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
        lightsOn = !lightsOn;

        RefreshLights();
    }

    private void RefreshLights()
    {
        if (!lightsOn) {
            LightsOff();
        } else {
            LightsOn();
        }
    }

    private void LightsOff()
    {
        var elements = GameObject.FindObjectsOfType<MapElement>();
        foreach(var element in elements) {
            if (element.lightSensitive) {
                element.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            }
        }
    }

    private void LightsOn()
    {
        var elements = GameObject.FindObjectsOfType<MapElement>();
        foreach(var element in elements) {
            if (element.lightSensitive) {
                element.gameObject.GetComponent<SpriteRenderer>().enabled = true;
            }
        }
    }

    private void Move(Vector3 offset)
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, offset, 1.0f);

        if (hit.collider != null) {
            hit.collider.gameObject.SendMessage("heroWalkIn", gameObject, SendMessageOptions.DontRequireReceiver);

            var mapElement = hit.collider.gameObject.GetComponent<MapElement>();

            if (mapElement == null || !mapElement.IsWalkable) {
                return;
            }
        }

        var grid = transform.parent.GetComponent<Grid>();
        transform.position = grid.GetCellCenterLocal(grid.WorldToCell(transform.position + offset));
    }
}
