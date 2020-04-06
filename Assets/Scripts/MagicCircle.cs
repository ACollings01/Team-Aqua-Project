using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MagicCircle : MonoBehaviour
{
    private AudioSource magicCircleAudioSource;
    private GameManager _gameManager;
    private bool canRunAudio = true;

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
        if (other.gameObject.tag == "Player" && canRunAudio)
        {
            StartCoroutine(ExitMap());
            canRunAudio = false;
            magicCircleAudioSource.Play();
        }
    }

    IEnumerator ExitMap()
    {
        yield return new WaitForSeconds(1.5f);
        mMenu.SetActive(true);
    }
}
