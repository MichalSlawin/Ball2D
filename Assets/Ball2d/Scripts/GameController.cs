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
}
