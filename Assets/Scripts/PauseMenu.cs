using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

    public static bool isPaused = false;
    public GameObject pauseMenu;
    public GameObject optionsScreen;
    public GameObject pauseScreen;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)) {
            if (isPaused) {
                Resume();
            } else {
                Pause();
            }
        }
    }

    public void Resume() {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    void Pause() {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void LoadMainMenu() {
        SceneManager.LoadScene(0);
        Time.timeScale = 1f;
    }

    public void LoadOptionsMenu() {
        pauseScreen.SetActive(false);
        optionsScreen.SetActive(true);
    }

    public void BackToPauseScreen() {
        optionsScreen.SetActive(false);
        pauseScreen.SetActive(true);
    }

    public void QuitGame() {
        Application.Quit();
    }
}
