using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [SerializeField] private int levelsCount = 8;
    private int visibleLevelNum = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
        visibleLevelNum = visibleLevelNum % levelsCount + change;
        if (visibleLevelNum <= 0) visibleLevelNum += levelsCount;


        GameObject levelText = GameObject.Find("LevelText");
        if(levelText == null) throw new System.Exception("levelText not found");

        levelText.GetComponent<TextMeshProUGUI>().text = "Level " + visibleLevelNum;
    }
}
