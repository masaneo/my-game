using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class Victory : MonoBehaviour
{
    private PlayerScore playerScore;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI bestTimeText;
    private float bestTime;


    void Awake() {
        playerScore = FindObjectOfType<PlayerScore>();

        if(PlayerPrefs.HasKey("BestTime")){
            if(PlayerPrefs.GetFloat("BestTime") > playerScore.GetTimer()) {
                PlayerPrefs.SetFloat("BestTime", playerScore.GetTimer());
            }
        } else {
            PlayerPrefs.SetFloat("BestTime", playerScore.GetTimer());
        }
    }

    void Start() {
        timerText.text = "Your time: " + GetFormatedTime(playerScore.GetTimer());
        bestTimeText.text = "Your best time: " + GetFormatedTime(PlayerPrefs.GetFloat("BestTime"));
    }

    public void BackToStartMenu () {
        SceneManager.LoadScene(0);
    }

    private string GetFormatedTime(float floatTime) {
        string value;
        int minutes;
        int seconds;

        minutes = (int)Mathf.Floor(floatTime/60);
        seconds = (int)floatTime%60;
        value = minutes + "min " + seconds + "sec";

        return value;
    }
}
