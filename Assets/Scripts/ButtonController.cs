using System;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ButtonController : MonoBehaviour
{
  
    public ButtonController()
    {
    }

    public void startGame(string type)
    {
        if(type == "normal")
        {
            SceneManager.LoadScene("MainGameScene");
            print("normal");
        } else if (type == "long")
        {
            SceneManager.LoadScene("MainGameScene");
            print("long");
        } else
        {
            SceneManager.LoadScene("MainGameScene");
            print("hardcore");
        }
    }
}


