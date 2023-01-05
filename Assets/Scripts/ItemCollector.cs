using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ItemCollector : MonoBehaviour
{
    public int coinCount = 0;
    [SerializeField] private TextMeshProUGUI coinCountText; 
    public AudioSource coinSound;

    private void Awake() {
        coinSound.volume = 1;
    }

    private void OnTriggerEnter2D(Collider2D collision){
        if(collision.gameObject.CompareTag("Coin")) {
            Destroy(collision.gameObject);
            Debug.Log("coin++");
            coinCount++;
            coinCountText.text = "Collected coins: " + coinCount;
        }

        if(collision.gameObject.CompareTag("Coin") || collision.gameObject.CompareTag("Heart")) {
            coinSound.Play();
        }
    }

}
