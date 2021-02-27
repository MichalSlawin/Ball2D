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

    // Update is called once per frame
    void Update()
    {
        
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
    }
}
