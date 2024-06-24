using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager_Script : MonoBehaviour
{
    public Slider volumeSlider;
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("musicVolume"))
        {
            load();
        }
        else
        {
            PlayerPrefs.SetFloat("musicVolume", .5f);
            load();
        }
    }

    // Update is called once per frame
    public void changeSound()
    {
        AudioListener.volume = volumeSlider.value;
        save();
    }

    void load()
    {
        volumeSlider.value = PlayerPrefs.GetFloat("gameVolume");
    }

    void save()
    {
        PlayerPrefs.SetFloat("gameVolume", volumeSlider.value);
    }
}
