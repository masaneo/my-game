using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class FlagScript : MonoBehaviour
{
    public ItemCollector itemCollector;
    public int coinsOnLevel;
    [SerializeField] private TextMeshProUGUI missingCoinsText;

    IEnumerator ShowMissingCoinsMessage(int seconds){
        missingCoinsText.enabled = true;
        yield return new WaitForSeconds(seconds);
        missingCoinsText.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player") {
            if(itemCollector.coinCount == coinsOnLevel) {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                itemCollector.coinCount = 0;
            }
            else {
                StartCoroutine(ShowMissingCoinsMessage(3));
            }
        }
    }
}
