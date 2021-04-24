using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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

    private void Move(Vector3 offset)
    {
        var grid = transform.parent.GetComponent<Grid>();
        transform.position = grid.GetCellCenterLocal(grid.WorldToCell(transform.position + offset));
    }
}
