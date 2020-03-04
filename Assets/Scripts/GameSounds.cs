using UnityEditor;
using UnityEngine;

[System.Serializable]
public class GameSounds : MonoBehaviour
{
    public string audioName;
    public AudioClip audioClip;

    [Range(0f, 10f)]
    public float volume = 5f;
    [Range(0.5f, 1.5f)]
    public float pitch = 1f;

    [Range(0f, 5f)]
    public float randvolume = 0.1f;
    [Range(0f, 0.5f)]
    public float randpitch = 0.1f;

    [HideInInspector]

    public AudioSource source;

    public bool isLooping;

    public bool playOnAwake;
}
