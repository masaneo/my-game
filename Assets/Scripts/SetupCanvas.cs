using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SetupCanvas : MonoBehaviour
{
    private TextMeshProUGUI healthCountText;
    private PlayerScore playerScore;

    // Start is called before the first frame update
    void Start()
    {
        UpdateHealthText();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Awake() {
        healthCountText = GameObject.Find("Health Count").GetComponent<TextMeshProUGUI>();
        playerScore = FindObjectOfType<PlayerScore>();
    }

    void UpdateHealthText() {
        healthCountText.text = "Health: " + playerScore.GetHealth();
        Debug.Log("Health from setup canvca: " + playerScore.GetHealth());
    }
}
