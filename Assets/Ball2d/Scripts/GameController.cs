using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField] private string nextLevelName = "";

    private UIController uIController;

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;

        uIController = FindObjectOfType<UIController>();
        if (uIController == null) throw new System.Exception("UIController not found");
    }

    public void FinishLevel(int points, int deaths)
    {
        string currSceneName = SceneManager.GetActiveScene().name;
        int currLevelNum =  (int)Char.GetNumericValue(currSceneName[5]);
        GameData.FileData.UnlockedLevelNum = currLevelNum + 1;

        int stars = 1;
        if(points == 5)
        {
            stars = 2;
            if(deaths == 0)
            {
                stars = 3;
            }
        }
        GameData.FileData.SetStarsInLevel(currLevelNum, stars);

        GameData.SaveFile();

        uIController.PauseGame();
        uIController.ShowFinishedLevelMenu(stars);
    }

    public void LoadNextLevel()
    {
        if(nextLevelName == "")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        else
        {
            SceneManager.LoadScene(nextLevelName);
        }
    }

    public void RespawnGreatDeathBalls()
    {
        GreatDeathBall[] balls = FindObjectsOfType<GreatDeathBall>();

        foreach(GreatDeathBall ball in balls)
        {
            ball.RespawnBall();
        }
    }

    public void RespawnGoingLeftBalls()
    {
        GreatDeathBall[] balls = FindObjectsOfType<GreatDeathBall>();

        foreach (GreatDeathBall ball in balls)
        {
            ball.RespawnAndGoLeft();
        }
    }
}
