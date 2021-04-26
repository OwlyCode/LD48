using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
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
    public GameObject Battery;
    public GameObject Panel;
    public GameObject Einstein;
    public GameObject Rasta;
    public GameObject Bas;
    public GameObject WaterDrop;

    public string level;
    public int env;
    public RuntimeAnimatorController SockBas;
    public RuntimeAnimatorController SockRasta;
    public RuntimeAnimatorController SockEinst;

    bool disableBattery = false;

    bool disableSocks = false;

    private Vector3 startPosition;
    private int Anim = 0;

    RuntimeAnimatorController GetNextSockAnim()
    {
        RuntimeAnimatorController[] AnimCon = { SockEinst, SockRasta, SockBas };
        if (Anim > 2) { Anim = 0; }
        return AnimCon[Anim++];
    }

    RuntimeAnimatorController GetNextSockAnim(string Choose)
    {
        if (Choose.ToLower() == "rasta") { return SockRasta; }
        else if (Choose.ToLower() == "einstein") { return SockEinst; }
        else { return SockBas; }
    }


    string GetNextLevel()
    {
        switch (level)
        {
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
            case "level7":
                return "level8";
            case "level8":
                return "level9";
            case "level9":
                return "level10";
            case "level10":
                return "level11";
            case "level11":
                return "level12";
            case "level12":
                return "level13";
            case "level13":
                return "level14";
            case "level14":
                return "level15";
            case "level15":
                return "level0";
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

        disableBattery = false;
        disableSocks = false;

        SourceLevel(level);
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        LightManager.RefillMax();
    }

    public void RestartLevel()
    {
        Destroy(GameObject.Find(level));

        SourceLevel(level);
    }

    public void DisableBattery()
    {
        disableBattery = true;
    }

    public void DisableSocks()
    {
        disableSocks = true;
    }

    void MayAddDrop(GameObject root, Vector3 position)
    {
        if (Random.Range(0, 20) == 1)
        {
            var drop = Instantiate(WaterDrop, position, Quaternion.identity);
            drop.transform.parent = root.transform;
        }
    }

    void SourceLevel(string FileName)
    {
        var grid = GameObject.Find("Grid").GetComponent<Grid>();
        GameObject root = new GameObject(level);

        string text = Resources.Load<TextAsset>("Levels/" + FileName).text;

        string[] data = Regex.Split(text, "---");
        string[] metas = Regex.Split(data[0], "\n");

        string[] lines = Regex.Split(data[1], "\n");
        int y = 0;
        int x = 0;

        int maxLength = 0;

        foreach (string meta in metas)
        {
            string[] parts = meta.Split(':');

            if (parts[0].Trim() == "env")
            {
                env = int.Parse(parts[1].Trim());
            }
        }

        y = lines.Length;
        foreach (string line in lines)
        {
            x = 0;
            foreach (char ch in line)
            {
                if (line.Length > maxLength)
                {
                    maxLength = line.Length;
                }

                var position = grid.GetCellCenterLocal(grid.WorldToCell(new Vector3(x, y, 0)));

                switch (ch)
                {
                    case '.':
                        GameObject f1 = Instantiate(((env == 1) ? Floor1 : Floor4), position, Quaternion.identity);
                        f1.name = "Floor (" + x + ", " + y + ")";
                        f1.transform.parent = root.transform;
                        MayAddDrop(root, position);
                        break;
                    case '~':
                        GameObject f2 = Instantiate(Floor2, position, Quaternion.identity);
                        f2.name = "Floor2 (" + x + ", " + y + ")";
                        f2.transform.parent = root.transform;
                        MayAddDrop(root, position);
                        break;
                    case 'v':
                        GameObject tipepodv = Instantiate(TidePod, position, Quaternion.identity);
                        tipepodv.name = "TidePod (" + x + ", " + y + ")";
                        tipepodv.transform.parent = root.transform;
                        tipepodv.GetComponent<TidePod>().state = this;
                        tipepodv.GetComponent<TidePod>().direction = Vector3.up;

                        GameObject floorBoulderv = Instantiate(Floor2, position, Quaternion.identity); // 
                        floorBoulderv.name = "Floor (" + x + ", " + y + ")";
                        floorBoulderv.transform.parent = root.transform;
                        break;
                    case 'V':
                        GameObject tipepodV = Instantiate(TidePod, position, Quaternion.identity);
                        tipepodV.name = "TidePod (" + x + ", " + y + ")";
                        tipepodV.transform.parent = root.transform;
                        tipepodV.GetComponent<TidePod>().state = this;
                        tipepodV.GetComponent<TidePod>().direction = Vector3.up;

                        //GameObject floorBoulder = Instantiate(Floor2, position, Quaternion.identity); // 
                        GameObject floorBoulder = Instantiate(((env == 1) ? Floor1 : Floor4), position, Quaternion.identity);
                        floorBoulder.name = "Floor (" + x + ", " + y + ")";
                        floorBoulder.transform.parent = root.transform;
                        break;
                    case 'h':
                        GameObject tidepodH = Instantiate(TidePod, position, Quaternion.identity);
                        tidepodH.name = "TidePod (" + x + ", " + y + ")";
                        tidepodH.transform.parent = root.transform;
                        tidepodH.GetComponent<TidePod>().state = this;
                        tidepodH.GetComponent<TidePod>().direction = Vector3.left;

                        GameObject floorBoulder2 = Instantiate(Floor2, position, Quaternion.identity);
                        floorBoulder2.name = "Floor2 (" + x + ", " + y + ")";
                        floorBoulder2.transform.parent = root.transform;
                        break;
                    case 'H':
                        GameObject tidepodh = Instantiate(TidePod, position, Quaternion.identity);
                        tidepodh.name = "TidePod (" + x + ", " + y + ")";
                        tidepodh.transform.parent = root.transform;
                        tidepodh.GetComponent<TidePod>().state = this;
                        tidepodh.GetComponent<TidePod>().direction = Vector3.left;

                        GameObject floorBoulder2h = Instantiate(((env == 1) ? Floor1 : Floor4), position, Quaternion.identity);
                        floorBoulder2h.name = "Floor (" + x + ", " + y + ")";
                        floorBoulder2h.transform.parent = root.transform;
                        break;
                    case 'B':
                        if (!disableBattery)
                        {
                            GameObject b = Instantiate(Battery, position, Quaternion.identity);
                            b.name = "Battery (" + x + ", " + y + ")";
                            b.transform.parent = root.transform;
                        }

                        GameObject floorBattery = Instantiate(((env == 1) ? Floor1 : Floor4), position, Quaternion.identity);
                        floorBattery.name = "Floor (" + x + ", " + y + ")";
                        floorBattery.transform.parent = root.transform;
                        break;
                    case '#':
                        GameObject f4 = Instantiate(Floor4, position, Quaternion.identity);
                        f4.name = "Floor4 (" + x + ", " + y + ")";
                        f4.transform.parent = root.transform;
                        break;
                    case '■':
                    case 'X':
                        GameObject w = Instantiate(((env == 1) ? Wall1 : Wall4), position, Quaternion.identity);
                        w.name = "Wall (" + x + ", " + y + ")";
                        w.transform.parent = root.transform;
                        break;
                    case '─':
                        GameObject wh = Instantiate(((env == 1) ? WallH1 : WallH4), position, Quaternion.identity);
                        wh.name = "WallH (" + x + ", " + y + ")";
                        wh.transform.parent = root.transform;
                        break;
                    case '║':
                        GameObject wd = Instantiate(((env == 1) ? WallD1 : WallD4), position, Quaternion.identity);
                        wd.name = "WallD (" + x + ", " + y + ")";
                        wd.transform.parent = root.transform;
                        break;
                    case '═':
                        GameObject wb = Instantiate(((env == 1) ? WallB1 : WallB4), position, Quaternion.identity);
                        wb.name = "WallB (" + x + ", " + y + ")";
                        wb.transform.parent = root.transform;
                        break;
                    case '│':
                        GameObject wg = Instantiate(((env == 1) ? WallG1 : WallG4), position, Quaternion.identity);
                        wg.name = "WallG (" + x + ", " + y + ")";
                        wg.transform.parent = root.transform;
                        break;
                    case '┌':
                        GameObject whg = Instantiate(((env == 1) ? WallHG1 : WallHG4), position, Quaternion.identity);
                        whg.name = "WallHG (" + x + ", " + y + ")";
                        whg.transform.parent = root.transform;
                        break;
                    case '╖':
                        GameObject whd = Instantiate(((env == 1) ? WallHD1 : WallHD4), position, Quaternion.identity);
                        whd.name = "WallHD (" + x + ", " + y + ")";
                        whd.transform.parent = root.transform;
                        break;
                    case '╝':
                        GameObject wbd = Instantiate(((env == 1) ? WallBD1 : WallBD4), position, Quaternion.identity);
                        wbd.name = "WallBD (" + x + ", " + y + ")";
                        wbd.transform.parent = root.transform;
                        break;
                    case '╘':
                        GameObject wbg = Instantiate(((env == 1) ? WallBG1 : WallBG4), position, Quaternion.identity);
                        wbg.name = "WallBG (" + x + ", " + y + ")";
                        wbg.transform.parent = root.transform;
                        break;
                    case 'E':
                        GameObject i = Instantiate(InDoor, position, Quaternion.identity);
                        i.name = "InDoor (" + x + ", " + y + ")";
                        i.transform.parent = root.transform;
                        startPosition = i.transform.position;
                        break;
                    case 'S':
                        GameObject o = Instantiate(OutDoor, position, Quaternion.identity);
                        o.GetComponent<Win>().state = this;
                        o.name = "OutDoor (" + x + ", " + y + ")";
                        o.transform.parent = root.transform;
                        break;
                    case 'T':
                        GameObject h = Instantiate(((env == 1) ? Hole1 : Hole4), position, Quaternion.identity);
                        h.GetComponent<Trap>().state = this;
                        h.name = "Hole (" + x + ", " + y + ")";
                        h.transform.parent = root.transform;
                        break;
                    case 'G':
                        GameObject su = Instantiate(SockU, position, Quaternion.identity);
                        su.GetComponent<SocketU>().state = this;
                        su.name = "SocketU (" + x + ", " + y + ")";
                        su.transform.parent = root.transform;
                        break;
                    case '1':
                        if (!disableSocks)
                        {
                            GameObject einstein = Instantiate(Einstein, position, Quaternion.identity);
                            einstein.GetComponent<Sock>().state = this;
                            einstein.name = "Einstein (" + x + ", " + y + ")";
                            einstein.transform.parent = root.transform;
                        }

                        GameObject floorEinstein = Instantiate(((env == 1) ? Floor1 : Floor4), position, Quaternion.identity);
                        floorEinstein.name = "Floor (" + x + ", " + y + ")";
                        floorEinstein.transform.parent = root.transform;
                        break;
                    case '2':
                        if (!disableSocks)
                        {
                            GameObject rasta = Instantiate(Rasta, position, Quaternion.identity);
                            rasta.GetComponent<Sock>().state = this;
                            rasta.name = "Rasta (" + x + ", " + y + ")";
                            rasta.transform.parent = root.transform;
                        }

                        GameObject floorRasta = Instantiate(((env == 1) ? Floor1 : Floor4), position, Quaternion.identity);
                        floorRasta.name = "Floor (" + x + ", " + y + ")";
                        floorRasta.transform.parent = root.transform;
                        break;
                    case '3':
                        if (!disableSocks)
                        {
                            GameObject bas = Instantiate(Bas, position, Quaternion.identity);
                            bas.GetComponent<Sock>().state = this;
                            bas.name = "Bas (" + x + ", " + y + ")";
                            bas.transform.parent = root.transform;
                        }

                        GameObject floorBas = Instantiate(((env == 1) ? Floor1 : Floor4), position, Quaternion.identity);
                        floorBas.name = "Floor (" + x + ", " + y + ")";
                        floorBas.transform.parent = root.transform;
                        break;
                    default:
                        break;
                }
                x++;
            }
            y--;
        }

        Bounds bounds = new Bounds(root.transform.position, Vector3.zero);

        foreach (Renderer r in root.GetComponentsInChildren<Renderer>())
        {
            bounds.Encapsulate(r.bounds);
        }

        Camera.main.transform.position = (-Vector3.forward * 10) + new Vector3(bounds.size.x / 2f, bounds.size.y / 2f, 0);

        Camera.main.orthographicSize = Mathf.Max(bounds.size.x / 2, bounds.size.y / 2);

        float bx = bounds.size.x * Screen.height / Screen.width * 0.5f;
        float by = bounds.size.y * Screen.height / Screen.width;

        if (bounds.size.x > bounds.size.y)
        {
            Camera.main.orthographicSize = bx;
        }
        else
        {
            Camera.main.orthographicSize = by;
        }

        float screenRatio = (float)Screen.width / (float)Screen.height;
        float targetRatio = bounds.size.x / bounds.size.y;

        if (screenRatio >= targetRatio)
        {
            Camera.main.orthographicSize = bounds.size.y / 2;
        }
        else
        {
            float differenceInSize = targetRatio / screenRatio;
            Camera.main.orthographicSize = bounds.size.y / 2 * differenceInSize;
        }


        GameObject.Find("Bubbles").GetComponent<ParticleSystem>().Clear();
        GameObject.Find("Bubbles").GetComponent<ParticleSystem>().Stop();

        GameObject.Find("Bubbles").transform.position = Vector3.forward * 10 + Camera.main.transform.position + Vector3.down * (2 + bounds.size.y / 2);


        // After camera resize to avoid fillers moving the camera
        foreach (string meta in metas)
        {
            string[] parts = meta.Split(':');

            if (parts[0].Trim() == "filler")
            {
                string[] fillerParts = parts[1].Split('|');
                var fillerType = int.Parse(fillerParts[0].Trim());
                var fillerX = int.Parse(fillerParts[1].Trim());
                var fillerY = int.Parse(fillerParts[2].Trim());
                var fillerRotation = int.Parse(fillerParts[3].Trim());

                var dictionnary = GameObject.Find("FillerDictionnary").GetComponent<FillerDictionnary>();

                Vector3 fillerPos = grid.GetCellCenterLocal(grid.WorldToCell(new Vector3(fillerX, fillerY, 0)));

                GameObject filler = Instantiate(dictionnary.Fillers[fillerType], fillerPos, Quaternion.Euler(0f, 0f, fillerRotation));
                filler.name = "Filler " + fillerX + " " + fillerY;
                filler.transform.parent = root.transform;
            }
        }

        GameObject.Find("Hero").transform.position = GetStartPosition();
        LightManager.SetStartLight(true);
    }

    IEnumerator WriteRP(string AssetString, Text TextStage)
    {
        TextStage.text = "";
        string display = "";
        foreach (char ch in AssetString)
        {
            display += ch;
            string filler = "";

            foreach (char ch2 in AssetString.Substring(display.Length))
            {
                if (ch2 == ' ')
                {
                    filler += " ";
                }
                else
                {
                    filler += "\u00a0";
                }
            }
            TextStage.text = display + filler;

            yield return new WaitForSeconds(Random.Range(0.01f, 0.2f) / 3f);
        }
    }
    public void ShowPanel(string AssetString)
    {
        this.Panel.SetActive(true);
        this.Panel.transform.position = new Vector3(this.Panel.transform.position.x + 6000, this.Panel.transform.position.y, this.Panel.transform.position.z);
        Panel.transform.Find("Anim").GetComponent<Animator>().runtimeAnimatorController = GetNextSockAnim() as RuntimeAnimatorController;
        Text PanelText = Panel.transform.Find("Text").GetComponent<Text>();

        StopAllCoroutines();
        StartCoroutine(WriteRP(AssetString, PanelText));
    }

    public void ShowPanel(string AssetString, string SockChooser)
    {
        GameObject.Find("Hero").GetComponent<Hero>().Lock();
        this.Panel.SetActive(true);
        this.Panel.transform.position = new Vector3(this.Panel.transform.position.x + 6000, this.Panel.transform.position.y, this.Panel.transform.position.z);
        Panel.transform.Find("Anim").GetComponent<Animator>().runtimeAnimatorController = GetNextSockAnim(SockChooser) as RuntimeAnimatorController;

        Text PanelText = Panel.transform.Find("Text").GetComponent<Text>();

        StopAllCoroutines();
        StartCoroutine(WriteRP(AssetString, PanelText));
    }

    public void HidePanel()
    {
        GameObject.Find("Hero").GetComponent<Hero>().Unlock();
        this.Panel.transform.position = new Vector3(this.Panel.transform.position.x - 6000, this.Panel.transform.position.y, this.Panel.transform.position.z);
    }

    void Start()
    {
        HidePanel();
        SourceLevel(level);
        Achievements.deathLess = true;
        Achievements.lightLess = true;
        Achievements.socks = 0;
        Achievements.startTime = Time.time;

    }

    void Update()
    {
        if (Settings.enableAudio)
            AudioListener.volume = 1;
        else
            AudioListener.volume = 0;
    }
}
