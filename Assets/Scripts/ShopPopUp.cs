using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopPopUp : MonoBehaviour
{
    private GameManager _gameManager;
    private AudioSource ShopPortalAudioSource;
    private bool canRunAudio = true;

    public GameObject Shop;

    void Start()
    {
        _gameManager = GameManager.Instance;
        ShopPortalAudioSource = GetComponent<AudioSource>();
        Shop.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag =="Player" && canRunAudio)
        {
            Shop.SetActive(true);
            StartCoroutine(ExitMap());
            canRunAudio = false;
            ShopPortalAudioSource.Play();
        }
    }

    IEnumerator ExitMap()
    {
        yield return new WaitForSeconds(0.5f);
    }
}
