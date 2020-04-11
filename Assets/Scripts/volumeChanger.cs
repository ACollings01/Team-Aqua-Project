using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class volumeChanger : SoundManager
{

    private AudioSource soundcontrol;
    private float SliderVolume = 1f;
    private float SliderSFX = 1f;

    // Start is called before the first frame update
    void Start()
    {
        soundcontrol = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        soundcontrol.volume = SliderVolume;
        soundcontrol.volume = SliderSFX; 
    }

    public void SetVolume(float vol)
    {
        SliderVolume = vol;
    }

    public void SetEffectsVolume(float gamesounds)
    {
        SliderSFX = gamesounds;
    }
}
