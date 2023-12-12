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
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void LoadLoseScreen()
    {
        SceneManager.LoadScene("LoseScreen");
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

    }
    /*public void ExitGame()
    {
        Application.Quit();
    }*/
    //this is for the end of the level when the player enters a trigger to win the game
    private void OnTriggerEnter(Collider winScreen)
    {
        if (winScreen.gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene("WinScreen");
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

        }
    }

    private void OnEnable()
    {
        PlayerStats.PlayerDied += LoadLoseScreen;
        TimerControler.TimerEnded += LoadLoseScreen;
        PlayerCollisionManager.PlayerTouchedFloor += LoadLoseScreen;
    }

    private void OnDisable()
    {
        PlayerStats.PlayerDied -= LoadLoseScreen;
        TimerControler.TimerEnded -= LoadLoseScreen;
        PlayerCollisionManager.PlayerTouchedFloor -= LoadLoseScreen;
    }
}
