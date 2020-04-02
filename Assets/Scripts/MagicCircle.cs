using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicCircle : MonoBehaviour
{
    private AudioSource magicCircleAudioSource;
    private GameManager _gameManager;

    void Start()
    {
        _gameManager = GameManager.Instance;
        magicCircleAudioSource = GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _gameManager.ActivateMCircle();
        }
        magicCircleAudioSource.Play();
    }
   
}
