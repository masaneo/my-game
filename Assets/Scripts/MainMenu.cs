using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private PlayerScore playerScore;

    public void Awake() { 
        playerScore = FindObjectOfType<PlayerScore>();
    }

    public void PlayGame () {
        SceneManager.LoadScene(2);
        playerScore.SetHealth(3);
        playerScore.ResetTimer();
    }

    public void QuitGame () {
        Application.Quit();
    }
}
