using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 
using UnityEngine.UI;
using TMPro;

public class Player : MonoBehaviour
{
    public int health;
    [SerializeField] private TextMeshProUGUI healthCountText;

    void update() {

    }
    public void checkHealth() {
        if(health == 0) {
            SceneManager.LoadScene(1);
        }
    }
    public void updateHealth() {
        healthCountText.text = "Health: " + health;
    }
}
