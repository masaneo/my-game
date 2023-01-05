using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Options : MonoBehaviour
{
    [SerializeField] private Slider volumeSlider = null;

    void Start() {
        volumeSlider.value = PlayerPrefs.GetFloat("VolumeValue");
    }

    public void VolumeSlider() {
        PlayerPrefs.SetFloat("VolumeValue", volumeSlider.value);
        AudioListener.volume = volumeSlider.value;
    }
}
