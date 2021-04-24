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

    void SourceLevel(string FileName)
    {
        string[] lines = File.ReadAllLines("Assets/Levels/" + FileName);
        int y = 0;
        int x = 0;

        
        foreach (string line in lines)
        {
            x = 0;
            foreach (char ch in line)
            {
                Debug.Log(ch);
                switch (ch)
                {
                    case '.':
                        GameObject a = Instantiate(Floor, new Vector3(x, y, 0), Quaternion.identity);
                        a.name = "Floor ("+x+", "+y + ")";
                        a.transform.parent = GameObject.Find(FileName).transform;
                        break;
                    case 'X':
                        GameObject b = Instantiate(Wall, new Vector3(x, y, 0), Quaternion.identity);
                        b.name = "Wall ("+x+", "+y + ")";
                        b.transform.parent = GameObject.Find(FileName).transform;
                        break;
                    case 'E':
                        GameObject c = Instantiate(InDoor, new Vector3(x, y, 0), Quaternion.identity);
                        c.name = "InDoor ("+x+", "+y + ")";
                        c.transform.parent = GameObject.Find(FileName).transform;
                        break;
                    case 'S':
                        GameObject d = Instantiate(OutDoor, new Vector3(x, y, 0), Quaternion.identity);
                        d.name = "OutDoor ("+x+", "+y + ")";
                        d.transform.parent = GameObject.Find(FileName).transform;
                        break;
                    case 'T':
                        GameObject e = Instantiate(Hole, new Vector3(x, y, 0), Quaternion.identity);
                        e.name = "Hole ("+x+", "+y + ")";
                        e.transform.parent = GameObject.Find(FileName).transform;
                        break;
                    default:
                        Debug.Log("Default !");
                        break;
                }
                x++;
            }
            y++;
        }

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
