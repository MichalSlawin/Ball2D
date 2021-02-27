using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    private int visibleLevelNum = 1;

    // Start is called before the first frame update
    void Start()
    {
        GetFileData();
    }

    private void GetFileData()
    {
        FileData fileData = GameData.LoadFile();
        if (fileData != null)
        {
            
        }
    }

    public void LoadLevel()
    {
        string levelName = "Level" + visibleLevelNum + "Scene";
        SceneManager.LoadScene(levelName);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ChangeVisibleLevel(int change)
    {
        visibleLevelNum = visibleLevelNum % GameData.GetLevelsCount() + change;
        if (visibleLevelNum <= 0) visibleLevelNum += GameData.GetLevelsCount();


        GameObject levelText = GameObject.Find("LevelText");
        if(levelText == null) throw new System.Exception("levelText not found");

        levelText.GetComponent<TextMeshProUGUI>().text = "Level " + visibleLevelNum;

        GameObject locked = GameObject.Find("Locked");
        if (locked == null) throw new System.Exception("Locked not found");
        GameObject startLevelButton = GameObject.Find("StartLevelButton");
        if (startLevelButton == null) throw new System.Exception("StartLevelButton not found");
        Transform stbChild = startLevelButton.transform.GetChild(0);
        if (stbChild == null) throw new System.Exception("StartLevelButton Text not found");

        if (visibleLevelNum > GameData.FileData.UnlockedLevelNum)
        {
            locked.GetComponent<Image>().enabled = true;

            startLevelButton.GetComponent<Image>().enabled = false;
            startLevelButton.GetComponent<Button>().enabled = false;
            stbChild.gameObject.GetComponent<TextMeshProUGUI>().text = "";
        }
        else
        {
            locked.GetComponent<Image>().enabled = false;

            startLevelButton.GetComponent<Image>().enabled = true;
            startLevelButton.GetComponent<Button>().enabled = true;
            stbChild.gameObject.GetComponent<TextMeshProUGUI>().text = "Start";
        }
        SetStarsColors();
    }

    public void SetStarsColors()
    {
        Color32 yellow = new Color32(224, 210, 0, 255);
        Color32 grey = new Color32(51, 51, 51, 255);

        GameObject star1 = GameObject.Find("Star1");
        if (star1 == null) throw new System.Exception("Star1 not found");
        star1.GetComponent<Image>().color = grey;

        GameObject star2 = GameObject.Find("Star2");
        if (star2 == null) throw new System.Exception("Star2 not found");
        star2.GetComponent<Image>().color = grey;

        GameObject star3 = GameObject.Find("Star3");
        if (star3 == null) throw new System.Exception("Star3 not found");
        star3.GetComponent<Image>().color = grey;

        if (GameData.FileData.GetStarsInLevelNum(visibleLevelNum) > 0)
        {
            star1.GetComponent<Image>().color = yellow;

            if(GameData.FileData.GetStarsInLevelNum(visibleLevelNum) > 1)
            {
                star2.GetComponent<Image>().color = yellow;

                if (GameData.FileData.GetStarsInLevelNum(visibleLevelNum) > 2)
                {
                    star3.GetComponent<Image>().color = yellow;
                }
            }
        }
    }
}
