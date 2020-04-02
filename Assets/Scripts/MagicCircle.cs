using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicCircle : MonoBehaviour
{
    public AudioClip portal;
    private AudioSource magicCircleAudioSource;
    private GameManager _gameManager;

    void Start()
    {
        _gameManager = GameManager.Instance;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _gameManager.ActivateMCircle();
        }
        magicCircleAudioSource.PlayOneShot(portal);
    }
   
}
