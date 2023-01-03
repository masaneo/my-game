using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisappearingPlatform : MonoBehaviour
{
    public float timeToToggle = 2;
    private float currentTime = 0;
    public bool enabled = true;

    // Start is called before the first frame update
    void Start()
    {
        enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
        if(currentTime >= timeToToggle) {
            currentTime = 0;
            TogglePlatform();
        }
    }

    void TogglePlatform() {
        enabled = !enabled;
        foreach(Transform child in gameObject.transform){
            child.gameObject.SetActive(enabled);
        }
    }
}
