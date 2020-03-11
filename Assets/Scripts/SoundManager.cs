using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using yaSingleton;

[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;

    public static SoundManager Instance;
    public GameSounds[] gameSounds;

    [Range(0f, 10f)]
    public float volume = 5f;
    [Range(0.5f, 1.5f)]
    public float pitch = 1f;

    [Range(0f, 5f)]
    public float randvolume = 0.1f;
    [Range(0f, 0.5f)]
    public float randpitch = 0.1f;

    private AudioSource soundeffectaudio;

    public void SetSoundEffectAudio(AudioSource _soundeffectaudio)
    {
        soundeffectaudio = _soundeffectaudio;
        soundeffectaudio.clip = clip;
    }

    public void Play()
    {
        soundeffectaudio.volume = volume * (1 + Random.Range(-randvolume / 2f, randvolume / 2f));
        soundeffectaudio.pitch = pitch * (1 + Random.Range(-randpitch / 2f, randpitch / 2f));
        Debug.Log("soundeffectaudio.volume: " + soundeffectaudio.volume);
        Debug.Log("soundeffectaudio.pitch: " + soundeffectaudio.pitch);
        soundeffectaudio.Play();
    }

}


public class SoundManager : Singleton<SoundManager>
{
    public new static SoundManager Instance = null;
    private AudioSource soundEffectAudio;

    
    public AudioClip ES_Walk_Forest_Trail;
    public AudioClip ES_Volcanoe_Lava_Bubbles;
    public AudioClip Ambiances_Cave_Level;
    public AudioClip ES_Dungeon_Dream_Cave;
    public AudioClip Fire_Staff_Fireball_hit;
    public AudioClip Fire_Staff_Woosh;
    public AudioClip Ice_Staff_Hit_1;
    public AudioClip Ice_Staff_Swoosh_1;
    public AudioClip Lightning_Staff_Lightning_Strike;
    public AudioClip Wolf_Howl;
    public AudioClip Wolf_Bite_2;
    public AudioClip Wolf_Damage_2;
    public AudioClip Wolf_Death_3;


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










