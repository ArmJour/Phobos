using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    PlayerController playerController;
    private void Start()
    {
        playerController = GetComponent<PlayerController>();
    }
    public void playGame()
    {
        SceneManager.LoadScene("Main Scene");
    }

    public void setting() 
    {
        SceneManager.LoadScene("Setting");
    }

    public void quitGame() {
        Application.Quit();
    }

}
