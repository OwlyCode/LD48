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
    public GameObject SockU;
    public string level;

    private Vector3 startPosition;

    string GetNextLevel()
    {
        switch(level) {
            case "level0":
                return "level1";
            case "level1":
                return "level2";
            case "level2":
                return "level3";
            case "level3":
                return "level4";
            case "level4":
                return "level5";
            case "level5":
                return "level6";
            case "level6":
                return "level7";
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
        y = lines.Length;
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
                    case '~':
                        GameObject f = Instantiate(SockU, position, Quaternion.identity);
                        f.GetComponent<SocketU>().state = this;
                        f.name = "SocketU ("+x+", "+y + ")";
                        f.transform.parent = root.transform;
                        break;
                    default:
                        Debug.Log("Default !");
                        break;
                }
                x++;
            }
            y--;
        }

        Bounds bounds = new Bounds(root.transform.position, Vector3.zero);

        foreach(Renderer r in root.GetComponentsInChildren<Renderer>()) {
            bounds.Encapsulate(r.bounds);
        }

        Camera.main.transform.position = (-Vector3.forward * 10) + grid.GetCellCenterLocal(grid.WorldToCell(new Vector3( bounds.size.x/2, bounds.size.y/2, 0)));

        Camera.main.orthographicSize = Mathf.Max(1f + bounds.size.x/4, 1f + bounds.size.y/2);

        Debug.Log(Camera.main.orthographicSize);

        GameObject.Find("Hero").transform.position = GetStartPosition();
        LightManager.SetStartLight(true);
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
