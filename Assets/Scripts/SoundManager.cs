using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;

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

    float masterVolume = 1.0f;

    //public AudioClip ES_Walk_Forest_Trail;
    //public AudioClip ES_Volcanoe_Lava_Bubbles;
    //public AudioClip Ambiances_Cave_Level;
    //public AudioClip ES_Dungeon_Dream_Cave;
    //public AudioClip Fire_Staff_Fireball_hit;
    //public AudioClip Fire_Staff_Woosh;
    //public AudioClip Ice_Staff_Hit_1;
    //public AudioClip Ice_Staff_Swoosh_1;
    //public AudioClip Lightning_Staff_Lightning_Strike;
    //public AudioClip Wolf_Howl;
    //public AudioClip Wolf_Bite_2;
    //public AudioClip Wolf_Damage_2;
    //public AudioClip Wolf_Death_3;

    public Sound ES_Walk_Forest_Trail;
    public Sound ES_Volcanoe_Lava_Bubbles;
    public Sound Ambiances_Cave_Level;
    public Sound ES_Dungeon_Dream_Cave;
    public Sound Fire_Staff_Fireball_hit;
    public Sound Fire_Staff_Woosh;
    public Sound Ice_Staff_Hit_1;
    public Sound Ice_Staff_Swoosh_1;
    public Sound Lightning_Staff_Lightning_Strike;
    public Sound Wolf_Howl;
    public Sound Wolf_Bite_2;
    public Sound Wolf_Damage_2;
    public Sound Wolf_Death_3;
    public Sound ES_Slime_Pour_Horror;
    public Sound ES_Impact_Mud_Drop;
    public Sound Sword_Swoosh_4;
    public Sound ES_Bandit;
    public Sound ES_Button_Switch_10;
    public Sound ES_Bow_Release_Arrow;
    public Sound ES_Bow_Staff_hit;
    public Sound ES_Shield_Scrape_Metal_1;
    public Sound LotsOfBats;
    public Sound Portalsound;
    public Sound town;
    public Sound batspit;

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
            ES_Walk_Forest_Trail.SetSoundEffectAudio(soundEffectAudio);
            Wolf_Howl.SetSoundEffectAudio(soundEffectAudio);
            Wolf_Bite_2.SetSoundEffectAudio(soundEffectAudio);
            Wolf_Damage_2.SetSoundEffectAudio(soundEffectAudio);
            Fire_Staff_Fireball_hit.SetSoundEffectAudio(soundEffectAudio);
            ES_Volcanoe_Lava_Bubbles.SetSoundEffectAudio(soundEffectAudio);
            Lightning_Staff_Lightning_Strike.SetSoundEffectAudio(soundEffectAudio);
            Ambiances_Cave_Level.SetSoundEffectAudio(soundEffectAudio);


        }
    }

    public void PlayClip(AudioSource clip)
    {
        Debug.Log("SMPLAY");
        //soundEffectAudio.clip = clip;
        //clip.SetSoundEffectAudio(soundEffectAudio);
        //clip.volume = masterVolume * clip.volume;
        clip.Play();
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










