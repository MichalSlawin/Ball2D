using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{
    GameController gameController;

    void Start()
    {
        gameController = FindObjectOfType<GameController>();
        if (gameController == null) throw new System.Exception("Game controller not found.");
    }

    public void RestartLevel()
    {
        ResumeGame();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitToMenu()
    {
        ResumeGame();
        SceneManager.LoadScene("MenuScene");
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
    }

    public void ShowFinishedLevelMenu(int stars)
    {
        GameObject nextLevelButton = GameObject.Find("NextLevelButton");
        if (nextLevelButton == null) throw new System.Exception("NextLevelButton not found");
        Transform nlbChild = nextLevelButton.transform.GetChild(0);
        if (nlbChild == null) throw new System.Exception("NextLevelButton Text not found");

        nextLevelButton.GetComponent<Image>().enabled = true;
        nextLevelButton.GetComponent<Button>().enabled = true;
        nlbChild.gameObject.GetComponent<TextMeshProUGUI>().text = "Next";

        Color32 yellow = new Color32(224, 210, 0, 255);

        GameObject star1 = GameObject.Find("Star1");
        if (star1 == null) throw new System.Exception("Star1 not found");
        star1.GetComponent<Image>().enabled = true;
        if (stars > 0) star1.GetComponent<Image>().color = yellow;

        GameObject star2 = GameObject.Find("Star2");
        if (star2 == null) throw new System.Exception("Star2 not found");
        star2.GetComponent<Image>().enabled = true;
        if (stars > 1) star2.GetComponent<Image>().color = yellow;

        GameObject star3 = GameObject.Find("Star3");
        if (star3 == null) throw new System.Exception("Star3 not found");
        star3.GetComponent<Image>().enabled = true;
        if (stars > 2) star3.GetComponent<Image>().color = yellow;
    }

    public void LoadNextLevel()
    {
        gameController.LoadNextLevel();
    }
}
