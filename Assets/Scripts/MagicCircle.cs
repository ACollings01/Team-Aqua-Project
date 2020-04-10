using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MagicCircle : MonoBehaviour
{
    private AudioSource magicCircleAudioSource;
    private GameManager _gameManager;

    public GameObject mMenu;

    void Start()
    {
        _gameManager = GameManager.Instance;
        magicCircleAudioSource = GetComponent<AudioSource>();
        mMenu.SetActive(false);
    }

    //void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject.CompareTag("Player"))
    //    {
    //        _gameManager.ActivateMCircle();
    //    }
    //    magicCircleAudioSource.Play();
    //}

    void OnTriggerEnter(Collider player)
    {
        if (player.gameObject.tag == "Player")
        {
            mMenu.SetActive(true);

        }
    }
}
