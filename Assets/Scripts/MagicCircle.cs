using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicCircle : MonoBehaviour
{
    private AudioSource magicCircleAudioSource;
    private GameManager _gameManager;
    private bool canRunAudio = true;

    void Start()
    {
        _gameManager = GameManager.Instance;
        magicCircleAudioSource = GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && canRunAudio)
        {
            StartCoroutine(ExitMap());
            canRunAudio = false;
            magicCircleAudioSource.Play();
        }
    }

    IEnumerator ExitMap()
    {
        yield return new WaitForSeconds(1.5f);
        _gameManager.ActivateMCircle();
    }
   
}
