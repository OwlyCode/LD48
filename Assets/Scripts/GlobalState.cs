using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GlobalState : MonoBehaviour
{
    public GameObject Wall;
    public GameObject Floor;
    public GameObject InDoor;
    public GameObject OutDoor;
    public GameObject Hole;
    public string level;

    private Vector3 startPosition;

    string GetNextLevel()
    {
        switch(level) {
            case "level0":
                return "level1";
        }

        return "level0";
    }

    public Vector3 GetStartPosition()
    {
        return startPosition;
    }

    public void NextLevel()
    {
        Destroy(GameObject.Find(level));

        level = GetNextLevel();

        SourceLevel(level);
    }
    public void RestartLevel()
    {
        Destroy(GameObject.Find(level));

        SourceLevel(level);
    }

    void SourceLevel(string FileName)
    {
        string[] lines = File.ReadAllLines("Assets/Levels/" + FileName);
        int y = 0;
        int x = 0;

        var grid = GameObject.Find("Grid").GetComponent<Grid>();
        int maxLength = 0;

        GameObject root = new GameObject(level);

        foreach (string line in lines)
        {
            x = 0;
            foreach (char ch in line)
            {
                if (line.Length > maxLength) {
                    maxLength = line.Length;
                }

                var position = grid.GetCellCenterLocal(grid.WorldToCell(new Vector3(x, y, 0)));

                switch (ch)
                {
                    case '.':
                        GameObject a = Instantiate(Floor, position, Quaternion.identity);
                        a.name = "Floor ("+x+", "+y + ")";
                        a.transform.parent = root.transform;
                        break;
                    case 'X':
                        GameObject b = Instantiate(Wall, position, Quaternion.identity);
                        b.name = "Wall ("+x+", "+y + ")";
                        b.transform.parent = root.transform;
                        break;
                    case 'E':
                        GameObject c = Instantiate(InDoor, position, Quaternion.identity);
                        c.name = "InDoor ("+x+", "+y + ")";
                        c.transform.parent = root.transform;
                        startPosition = c.transform.position;
                        break;
                    case 'S':
                        GameObject d = Instantiate(OutDoor, position, Quaternion.identity);
                        d.GetComponent<Win>().state = this;
                        d.name = "OutDoor ("+x+", "+y + ")";
                        d.transform.parent = root.transform;
                        break;
                    case 'T':
                        GameObject e = Instantiate(Hole, position, Quaternion.identity);
                        e.GetComponent<Trap>().state = this;
                        e.name = "Hole ("+x+", "+y + ")";
                        e.transform.parent = root.transform;
                        break;
                    default:
                        Debug.Log("Default !");
                        break;
                }
                x++;
            }
            y++;
        }

        Camera.main.transform.position = (-Vector3.forward * 10) + grid.GetCellCenterLocal(grid.WorldToCell(new Vector3(maxLength / 2, lines.Length / 2, 0)));
        Camera.main.orthographicSize = Mathf.Max(maxLength/2, lines.Length/2);
        GameObject.Find("Hero").transform.position = GetStartPosition();
    }

    // Start is called before the first frame update
    void Start()
    {
        SourceLevel(level);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
