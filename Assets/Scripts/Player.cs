using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 
using UnityEngine.UI;
using TMPro;

public class Player : MonoBehaviour
{
    [SerializeField] private int health = 3;
    [SerializeField] private TextMeshProUGUI healthCountText;
    private PlayerScore playerScore;
    private Vector3 spawnPoint;
    

    void Start() {
        spawnPoint = transform.position;
        playerScore = FindObjectOfType<PlayerScore>();
    }

    public void SetHealth(int value) {
        health = value;
    }

    public int GetHealth() {
        return health;
    }

    public void IncreaseHealth() {
        health += 1;
    }

    public void DecreaseHealth() {
        health -= 1;
    }

    public void checkHealth() {
        Debug.Log("Health: " + playerScore.GetHealth());
        if(playerScore.GetHealth() == 0) {
            SceneManager.LoadScene(1);
        }
    }
    public void updateHealth() {
        healthCountText.text = "Health: " + playerScore.GetHealth();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "VoidFallDetector" || other.tag == "Spike") {
            transform.position = spawnPoint;
            Debug.Log("Death by" + other.tag);
            playerScore.DecreaseHealth();
            updateHealth();
            checkHealth();        
        }    
        if(other.tag == "Heart") {
            playerScore.IncreaseHealth();
            updateHealth();
            Destroy(other.gameObject);
        }
    }
}
