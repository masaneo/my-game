using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScore : MonoBehaviour
{
    [SerializeField] private int health;
    private int coins;
    private static PlayerScore playerScore;
    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Timer();
    }

    void Awake() {
        DontDestroyOnLoad(this);

        if(playerScore == null) {
            playerScore = this;
        } else {
            DestroyObject(gameObject);
        }
    }

    public void IncreaseHealth() {
        health += 1;
    }

    public void DecreaseHealth() {
        health -= 1;
    }

    public int GetHealth() {
        return health;
    }

    public void SetHealth(int value) {
        health = value;
    }

    private void Timer() { 
        timer += Time.deltaTime;
    }

    public float GetTimer() {
        return timer;
    }

    public void ResetTimer() {
        timer = 0;
    }
}
