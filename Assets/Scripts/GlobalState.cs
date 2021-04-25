using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
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
    public GameObject WallBD1;
    public GameObject WallBD4;
    public GameObject WallHD1;
    public GameObject WallHD4;
    public GameObject WallBG1;
    public GameObject WallBG4;
    public GameObject WallHG1;
    public GameObject WallHG4;
    public GameObject Floor1;
    public GameObject Floor2;
    public GameObject Floor4;
    public GameObject InDoor;
    public GameObject OutDoor;
    public GameObject Hole1;
    public GameObject Hole4;
    public GameObject SockU;
    public GameObject TidePod;
    public GameObject Panel;
    public string level;
    public int env;
    public RuntimeAnimatorController SockBas;
    public RuntimeAnimatorController SockRasta;
    public RuntimeAnimatorController SockEinst;

    private Vector3 startPosition;
    private int Anim=0;

    RuntimeAnimatorController GetNextSockAnim()
    {
        RuntimeAnimatorController[] AnimCon= {SockEinst,SockRasta,SockBas};    
        if (Anim >2) {Anim=0;}
        return AnimCon[Anim++];
    }
    
    RuntimeAnimatorController GetNextSockAnim(string Choose)
    {
        if (Choose.ToLower() == "rasta") { return SockRasta;}
        else if (Choose.ToLower() == "einstein") { return SockEinst;}
        else { return SockBas;}
    }


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


        string[] data = Regex.Split(text, "---");
        string[] metas = Regex.Split(data[0], "\n");

        foreach (string meta in metas) {
            string[] parts = meta.Split(':');

            if (parts[0].Trim() == "env") {
                env = int.Parse(parts[1].Trim());
            }
        }

        string[] lines = Regex.Split(data[1], "\n");
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
                    case 'V':
                        GameObject tipepodV = Instantiate(TidePod, position, Quaternion.identity);
                        tipepodV.name = "TidePod ("+x+", "+y + ")";
                        tipepodV.transform.parent = root.transform;
                        tipepodV.GetComponent<TidePod>().state = this;
                        tipepodV.GetComponent<TidePod>().direction = Vector3.up;

                        GameObject floorBoulder = Instantiate(Floor2, position, Quaternion.identity);
                        floorBoulder.name = "Floor2 ("+x+", "+y + ")";
                        floorBoulder.transform.parent = root.transform;
                        break;
                    case 'H':
                        GameObject tidepodH = Instantiate(TidePod, position, Quaternion.identity);
                        tidepodH.name = "TidePod ("+x+", "+y + ")";
                        tidepodH.transform.parent = root.transform;
                        tidepodH.GetComponent<TidePod>().state = this;
                        tidepodH.GetComponent<TidePod>().direction = Vector3.left;

                        GameObject floorBoulder2 = Instantiate(Floor2, position, Quaternion.identity);
                        floorBoulder2.name = "Floor2 ("+x+", "+y + ")";
                        floorBoulder2.transform.parent = root.transform;
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
                   case '┌':
                        GameObject whg = Instantiate(((env==1)?WallHG1:WallHG4), position, Quaternion.identity);
                        whg.name = "WallHG ("+x+", "+y + ")";
                        whg.transform.parent = root.transform;
                        break;
                    case '╖':
                        GameObject whd = Instantiate(((env==1)?WallHD1:WallHD4), position, Quaternion.identity);
                        whd.name = "WallHD ("+x+", "+y + ")";
                        whd.transform.parent = root.transform;
                        break;
                    case '╝':
                        GameObject wbd = Instantiate(((env==1)?WallBD1:WallBD4), position, Quaternion.identity);
                        wbd.name = "WallBD ("+x+", "+y + ")";
                        wbd.transform.parent = root.transform;
                        break;
                    case '╘':
                        GameObject wbg  = Instantiate(((env==1)?WallBG1:WallBG4), position, Quaternion.identity);
                        wbg.name = "WallBG ("+x+", "+y + ")";
                        wbg.transform.parent = root.transform;
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

    IEnumerator WriteRP(string AssetString, Text TextStage)
    {
        TextStage.text="";
        foreach (char ch in AssetString) 
        {
            TextStage.text+= ch;
            yield return new WaitForSeconds (Random.Range(0.01f, 0.2f));
        }
        //TextStage.text = AssetString;
    }
    public void ShowPanel(string AssetString) 
    {
        this.Panel.SetActive(true);
         this.Panel.transform.position = new Vector3(this.Panel.transform.position.x+6000,this.Panel.transform.position.y,this.Panel.transform.position.z);
        Panel.transform.Find("Anim").GetComponent<Animator>().runtimeAnimatorController = GetNextSockAnim()  as RuntimeAnimatorController;
        Text PanelText = Panel.transform.Find("Text").GetComponent<Text>();

        StartCoroutine(WriteRP(AssetString, PanelText));
    }

    public void ShowPanel(string AssetString, string SockChooser) 
    {
        this.Panel.SetActive(true);
         this.Panel.transform.position = new Vector3(this.Panel.transform.position.x+6000,this.Panel.transform.position.y,this.Panel.transform.position.z);
        Panel.transform.Find("Anim").GetComponent<Animator>().runtimeAnimatorController = GetNextSockAnim(SockChooser)  as RuntimeAnimatorController;
        
        Text PanelText = Panel.transform.Find("PanelText").GetComponent<Text>();

        StartCoroutine(WriteRP(AssetString, PanelText));
    }

    public void HidePanel()
    {
        Debug.Log(Panel.transform.position.ToString());
        this.Panel.transform.position = new Vector3(this.Panel.transform.position.x-6000,this.Panel.transform.position.y,this.Panel.transform.position.z);
        Debug.Log(Panel.transform.position.ToString());
    }
    void InitPanel()
    {

    }
    // Start is called before the first frame update
    void Start()
    {
        //InitPanel();
        HidePanel();
        ShowPanel("have a UI element gameObject that displays the players gold he has collected thus far, however when I load a new scene I noticed in the inspector that the public gameObject space had been empty, I tried solving this with a simple script. goldTexfdsfsdfdsssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssdsdsfsdfsdfsdfsfsdft");
        SourceLevel(level);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
