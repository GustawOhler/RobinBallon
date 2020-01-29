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
        } else if (type == "hardcore")
        {
            SceneManager.LoadScene("MainGameScene");
            chosenDiff = MainController.LevelDifficulties.Hardcore;
            SceneManager.sceneLoaded += OnSceneLoaded;
        } else if (type == "training")
        {
            SceneManager.LoadScene("MainGameScene");
            chosenDiff = MainController.LevelDifficulties.Training;
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else if (type == "exit")
        {
#if UNITY_STANDALONE
            //Quit the application
            Application.Quit();
#endif

            //If we are running in the editor
#if UNITY_EDITOR
            //Stop playing the scene
            UnityEditor.EditorApplication.isPlaying = false;
#endif
        }
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        MainController.MainControllerInstance.ChooseLevelDiff(chosenDiff);
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}


