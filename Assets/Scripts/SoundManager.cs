using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;


public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    public GameSounds[] gameSounds;
    private float timeToReset;
    private bool timerIsSet = false;
    private float tempName;
    private float tempVolume;
    private bool isLowered = false;
    private bool fadeOut = false;
    private bool fadeIn = false;
    private string fadeInUsedString;



    // Start is called before the first frame update
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);

        foreach (var s in gameSounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.audioClip;
            s.source.volume = s.volume;
            s.source.loop = s.isLooping;
            if (s.playOnAwake)
            {
                s.source.Play();
            }
        }
    }

    public static void PlayMusic (String name)
    {
      GameSounds s = Array.Find(Instance.gameSounds, GameSounds => GameSounds.audioName == name);
      if (s == null)
      {
          Debug.LogError("Sound name" + name + "not found!");

          return;
      }
      else
      {
       s.source.Play();
      }
    }

    public static void StopMusic(String name)
    {
        GameSounds s = Array.Find(Instance.gameSounds, GameSounds => GameSounds.audioName == name);
        if (s == null)
        {
            Debug.LogError("Sound name" + name + "not found!");

            return;
        }
        else
        {
            s.source.Stop();
        }
    }

    public static void PauseMusic(String name)
    {
        GameSounds s = Array.Find(Instance.gameSounds, GameSounds => GameSounds.audioName == name);
        if (s == null)

        {

            Debug.LogError("Sound name" + name + "not found!");

            return;

        }

        else

        {

            s.source.Pause();

        }
    }

    public static void UnPauseMusic(String name)
    {
        GameSounds s = Array.Find(Instance.gameSounds, GameSounds => GameSounds.audioName == name);
        if (s == null)

        {

            Debug.LogError("Sound name" + name + "not found!");

            return;

        }

        else

        {

            s.source.UnPause();

        }
    }

    public static void LowerVolume(String name, float _duration)

    {

        if (Instance.isLowered == false)

        {
            GameSounds s = Array.Find(Instance.gameSounds, GameSounds => GameSounds.audioName == name);
            if (s == null)

            {

                Debug.LogError("Sound name" + name + "not found!");

                return;

            }

            else
            {
                Instance.tempName = name;
                Instance.tempVolume = s.volume;
                Instance.timeToReset = Time.time + _duration;
                Instance.timerIsSet = true;
                s.source.volume = s.source.volume / 5;

            }

            Instance.isLowered = true;
        }
    }

    public static void IFadeOut(String name, float duration)

    {

        Instance.StartCoroutine(Instance.IFadeOut(name, duration));

    }

    public static void FadeIn(String name, float targetVolume, float duration)

    {

        instance.StartCoroutine(instance.IFadeIn(name, targetVolume, duration));

    }





