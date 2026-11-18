using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    Image imgYou;           // your selected image (rock, paper, scissor)
    Image imgCom;           // computer selected image (rock, paper, scissor)

    Text txtYou;            // the score you win (Legacy Text)
    Text txtCom;            // the score computer win (Legacy Text)
    Text txtResult;         // the result (Legacy Text)

    TMP_Text tmpYou;        // the score you win (TextMeshPro)
    TMP_Text tmpCom;        // the score computer win (TextMeshPro)
    TMP_Text tmpResult;     // the result (TextMeshPro)

    int cntYou = 0;         // number you win
    int cntCom = 0;         // number computer win

    private void InitGame()
    {
        GameObject youImgObj = GameObject.Find("ImgYou");
        if (youImgObj != null) imgYou = youImgObj.GetComponent<Image>();

        GameObject comImgObj = GameObject.Find("ImgCom") ?? GameObject.Find("ImgCam");
        if (comImgObj != null) imgCom = comImgObj.GetComponent<Image>();

        GameObject youScoreObj = FindScoreTextObject("ImgScore_You", "TxtYou");
        if (youScoreObj != null)
        {
            txtYou = youScoreObj.GetComponent<Text>();
            tmpYou = youScoreObj.GetComponent<TMP_Text>();
        }

        GameObject comScoreObj = FindScoreTextObject("ImgScore_Com", "TxtCom")
                              ?? FindScoreTextObject("ImgScore_Cam", "TxtCam");
        if (comScoreObj != null)
        {
            txtCom = comScoreObj.GetComponent<Text>();
            tmpCom = comScoreObj.GetComponent<TMP_Text>();
        }

        GameObject resultObj = GameObject.Find("TxtResult_")
                            ?? GameObject.Find("TxtResult")
                            ?? GameObject.Find("TxtResults");
        if (resultObj != null)
        {
            txtResult = resultObj.GetComponent<Text>();
            tmpResult = resultObj.GetComponent<TMP_Text>();
        }

        SetText(txtResult, tmpResult, "Select the button below");
    }

    private GameObject FindScoreTextObject(string parentName, string txtName)
    {
        GameObject namedObj = GameObject.Find(txtName);
        GameObject boxTextObj = GameObject.Find(parentName + "/Text");
        if (boxTextObj != null && namedObj != null)
        {
            TMP_Text tmp = namedObj.GetComponent<TMP_Text>();
            Text legacy = namedObj.GetComponent<Text>();
            string current = tmp != null ? tmp.text : (legacy != null ? legacy.text : "");
            if (!int.TryParse(current.Trim(), out _))
            {
                return boxTextObj;
            }
        }
        return namedObj ?? boxTextObj;
    }

    private void SetText(Text legacyText, TMP_Text tmp, string value)
    {
        if (legacyText != null) legacyText.text = value;
        if (tmp != null) tmp.text = value;
    }

    void Start()
    {
        InitGame();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    public void onButtonClick(GameObject buttonObject)
    {
        OnButtonClick(buttonObject);
    }

    public void OnButtonClick(GameObject buttonObject)
    {
        Debug.Log(buttonObject.name + " clicked ");

        int you;
        if (!int.TryParse(buttonObject.name.Substring(0, 1), out you))
        {
            string lower = buttonObject.name.ToLowerInvariant();
            if (lower.Contains("rock")) you = 1;
            else if (lower.Contains("paper")) you = 2;
            else you = 3;
        }

        CheckResult(you);
    }

    void CheckResult(int yourResult)
    {
        int comResult = UnityEngine.Random.Range(1, 4);
        int k = yourResult - comResult;

        if (k == 0)
        {
            SetText(txtResult, tmpResult, "Draw.");
        }
        else if (k == 1 || k == -2)
        {
            SetText(txtResult, tmpResult, "You win.");
            cntYou++;
        }
        else
        {
            SetText(txtResult, tmpResult, "Computer win.");
            cntCom++;
        }

        SetResult(yourResult, comResult);
    }

    void SetResult(int you, int com)
    {
        if (imgYou != null)
            imgYou.sprite = Resources.Load("img_" + you, typeof(Sprite)) as Sprite;

        if (imgCom != null)
        {
            imgCom.sprite = Resources.Load("img_" + com, typeof(Sprite)) as Sprite;
            imgCom.transform.localScale = new Vector3(-1, 1, 1);
        }

        SetText(txtYou, tmpYou, cntYou.ToString());
        SetText(txtCom, tmpCom, cntCom.ToString());
    }
}
