using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using GoogleMobileAds.Api;

public class GameController : MonoBehaviour
{
    [SerializeField] private string nextLevelName = "";

    private UIController uIController;
    private int currentLevelNum = 0;
    private bool levelFinished = false;

    private InterstitialAd interstitial;

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;

        uIController = FindObjectOfType<UIController>();
        if (uIController == null) throw new System.Exception("UIController not found");

        SetCurrentLevelNum();

        LoadAds();

        Time.timeScale = 1;
    }

    private void Update()
    {
        if(levelFinished && Time.timeScale == 1)
        {
            Time.timeScale = 0;
        }
    }

    private void LoadAds()
    {
        // Initialize an InterstitialAd.
        interstitial = new InterstitialAd(SecretData.adUnitId);
        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the interstitial with the request.
        interstitial.LoadAd(request);
    }

    private void ShowAd()
    {
        if (interstitial.IsLoaded())
        {
            interstitial.Show();
        }
    }

    private void SetCurrentLevelNum()
    {
        string currSceneName = SceneManager.GetActiveScene().name;

        string currNumStr = "";
        for (int i = 0; i < currSceneName.Length; i++)
        {
            if (Char.IsDigit(currSceneName[i]))
                currNumStr += currSceneName[i];
        }

        currentLevelNum = int.Parse(currNumStr);
    }

    public void FinishLevel(int points, int deaths)
    {
        GameData.FileData.UnlockedLevelNum = currentLevelNum + 1;

        int stars = 1;
        if(points == 5)
        {
            stars = 2;
            if(deaths == 0)
            {
                stars = 3;
            }
        }
        GameData.FileData.SetStarsInLevel(currentLevelNum, stars);

        GameData.SaveFile();

        uIController.PauseGame();
        uIController.ShowFinishedLevelMenu(stars);

        levelFinished = true;

        if (currentLevelNum % 2 == 0)
        {
            ShowAd();
        }
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
