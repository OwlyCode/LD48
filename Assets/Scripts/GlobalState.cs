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
            foreach (char c in line)
            {
                Debug.Log(c);
                switch (c)
                {
                    case '.':
                        Instantiate(Floor, new Vector3(x, y, 0), Quaternion.identity);
                        break;
                    case 'X':
                        Instantiate(Wall, new Vector3(x, y, 0), Quaternion.identity);
                        break;
                    case 'E':
                        Instantiate(InDoor, new Vector3(x, y, 0), Quaternion.identity);
                        break;
                    case 'S':
                        Instantiate(OutDoor, new Vector3(x, y, 0), Quaternion.identity);
                        break;
                    case 'T':
                        Instantiate(Hole, new Vector3(x, y, 0), Quaternion.identity);
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
