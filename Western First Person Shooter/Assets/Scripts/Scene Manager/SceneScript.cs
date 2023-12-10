using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneScript : MonoBehaviour
{
    //following three functions load specific scenes or quits the game for menu buttons
    public void StartGame()
    {
        SceneManager.LoadScene("TestScene");
    }
    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void ExitGame()
    {
        Application.Quit();
    }
    //this is for the end of the level when the player enters a trigger to win the game
    private void OnTriggerEnter(Collider winScreen)
    {
        if (winScreen.gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene("WinScreen");
        }
    }
}
