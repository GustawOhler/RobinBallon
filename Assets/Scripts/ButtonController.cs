using System;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ButtonController : MonoBehaviour
{
    public MainController.LevelDifficulties chosenDiff;
    public ButtonController()
    {
    }

    public void startGame(string type)
    {
        if(type == "normal")
        {
            SceneManager.LoadScene("MainGameScene");
            chosenDiff = MainController.LevelDifficulties.Normal;
            SceneManager.sceneLoaded += OnSceneLoaded;
        } else if (type == "long")
        {
            SceneManager.LoadScene("MainGameScene");
            chosenDiff = MainController.LevelDifficulties.Long;
            SceneManager.sceneLoaded += OnSceneLoaded;
        } else
        {
            SceneManager.LoadScene("MainGameScene");
            chosenDiff = MainController.LevelDifficulties.Hardcore;
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        MainController.MainControllerInstance.ChooseLevelDiff(chosenDiff);
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}


