using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 
using UnityEngine.UI;
using TMPro;

public class Player : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI healthCountText;
    private PlayerScore playerScore;
    private Vector3 spawnPoint;
    

    void Start() {
        spawnPoint = transform.position;
        playerScore = FindObjectOfType<PlayerScore>();
    }

    public void CheckHealth() {
        if(playerScore.GetHealth() <= 0) {
            Debug.Log("Your time: " + playerScore.GetTimer());
            SceneManager.LoadScene(1);
        }
    }
    public void UpdateHealth() {
        healthCountText.text = "Health: " + playerScore.GetHealth();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "VoidFallDetector" || other.tag == "Spike") {
            transform.position = spawnPoint;
            playerScore.DecreaseHealth();
            UpdateHealth();
            CheckHealth();        
        }    
        if(other.tag == "Heart") {
            playerScore.IncreaseHealth();
            UpdateHealth();
            Destroy(other.gameObject);
        }
    }
}
