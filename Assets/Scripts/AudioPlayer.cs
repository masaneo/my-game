using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    
    private static AudioPlayer backgroundAudio;
    // Start is called before the first frame update
    void Start()
    {
        AudioListener.volume = PlayerPrefs.GetFloat("VolumeValue");
    }

    void Awake() {
        DontDestroyOnLoad(transform.gameObject);
        if(backgroundAudio == null) {
            backgroundAudio = this;
        } else {
            DestroyObject(gameObject);
        }
    }
}
