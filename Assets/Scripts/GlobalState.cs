using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Text.RegularExpressions;

public class GlobalState : MonoBehaviour
{
    public GameObject Wall1;
    public GameObject Wall4;
    public GameObject WallB1;
    public GameObject WallB4;
    public GameObject WallD1;
    public GameObject WallD4;
    public GameObject WallG1;
    public GameObject WallG4;
    public GameObject WallH1;
    public GameObject WallH4;
    public GameObject Floor1;
    public GameObject Floor2;
    public GameObject Floor4;
    public GameObject InDoor;
    public GameObject OutDoor;
    public GameObject Hole1;
    public GameObject Hole4;
    public GameObject SockU;
    public string level;
    public int env;

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
        string text = Resources.Load<TextAsset>("Levels/" + FileName).text;
        string[] lines = Regex.Split(text, "\n");
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
                        GameObject f1 = Instantiate(Floor1, position, Quaternion.identity);
                        f1.name = "Floor ("+x+", "+y + ")";
                        f1.transform.parent = root.transform;
                        break;
                    case '~':
                        GameObject f2 = Instantiate(Floor2, position, Quaternion.identity);
                        f2.name = "Floor2 ("+x+", "+y + ")";
                        f2.transform.parent = root.transform;
                        break;
                    case '#':
                        GameObject f4 = Instantiate(Floor4, position, Quaternion.identity);
                        f4.name = "Floor4 ("+x+", "+y + ")";
                        f4.transform.parent = root.transform;
                        break;
                    case '■':
                    case 'X':
                        GameObject w = Instantiate(((env==1)?Wall1:Wall4), position, Quaternion.identity);
                        w.name = "Wall ("+x+", "+y + ")";
                        w.transform.parent = root.transform;
                        break;
                    case '─':
                        GameObject wh = Instantiate(((env==1)?WallH1:WallH4), position, Quaternion.identity);
                        wh.name = "WallH ("+x+", "+y + ")";
                        wh.transform.parent = root.transform;
                        break;
                    case '║':
                        GameObject wd = Instantiate(((env==1)?WallD1:WallD4), position, Quaternion.identity);
                        wd.name = "WallD ("+x+", "+y + ")";
                        wd.transform.parent = root.transform;
                        break;
                    case '═':
                        GameObject wb = Instantiate(((env==1)?WallB1:WallB4), position, Quaternion.identity);
                        wb.name = "WallB ("+x+", "+y + ")";
                        wb.transform.parent = root.transform;
                        break;
                    case '│':
                        GameObject wg  = Instantiate(((env==1)?WallG1:WallG4), position, Quaternion.identity);
                        wg.name = "WallG ("+x+", "+y + ")";
                        wg.transform.parent = root.transform;
                        break;
                    case 'E':
                        GameObject i = Instantiate(InDoor, position, Quaternion.identity);
                        i.name = "InDoor ("+x+", "+y + ")";
                        i.transform.parent = root.transform;
                        startPosition = i.transform.position;
                        break;
                    case 'S':
                        GameObject o = Instantiate(OutDoor, position, Quaternion.identity);
                        o.GetComponent<Win>().state = this;
                        o.name = "OutDoor ("+x+", "+y + ")";
                        o.transform.parent = root.transform;
                        break;
                    case 'T':
                        GameObject h =  Instantiate(((env==1)?Hole1:Hole4), position, Quaternion.identity);
                        h.GetComponent<Trap>().state = this;
                        h.name = "Hole ("+x+", "+y + ")";
                        h.transform.parent = root.transform;
                        break;
                    case 'G':
                        GameObject su = Instantiate(SockU, position, Quaternion.identity);
                        su.GetComponent<SocketU>().state = this;
                        su.name = "SocketU ("+x+", "+y + ")";
                        su.transform.parent = root.transform;
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
