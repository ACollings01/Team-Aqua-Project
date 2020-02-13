using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManger Instance = null;
    private AudioSource soundEffectAudio;

    public AudioClip ES_Walk_Forest_Trail;
    public AudioClip ES_Volcanoe_Lava_Bubbles;
    public AudioClip ES_Cave_Water_Drips;
    public AudioClip ES_Alien_Cave_Wind;

    // Start is called before the first frame update
    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
            AudioSource[] sources = GetComponents<AudioSource>();
            foreach (AudioSource source in sources)
            {
                if (source.clip == null)
                {
                    soundEffectAudio = source;
                }
            }
        }
    }

    public void PlayOneShot(AudioClip clip)
    {
        soundEffectAudio.PlayOneShot(clip);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
