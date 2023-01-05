using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScore : MonoBehaviour
{
    [SerializeField] private int health;
    private int coins;
    private static PlayerScore playerScore;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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

    public int GetHealth(){
        return health;
    }
}
