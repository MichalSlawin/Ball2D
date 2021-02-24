using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField] private string nextLevelName = "";

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadNextLevel()
    {
        if(nextLevelName == "")
        {
            //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            Application.Quit();
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
