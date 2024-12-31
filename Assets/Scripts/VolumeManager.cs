using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeManager : MonoBehaviour
{
    // Start is called before the first frame update
    public Slider slider;
    
    void Start()
    {
        slider.value = PlayerPrefs.GetFloat("Volume", 1f);
        SetVolume(slider.value);
    }
    public void SetVolume(float volume)
    {
        AudioListener.volume = volume;
        PlayerPrefs.SetFloat("Volume", volume);
    }
}
