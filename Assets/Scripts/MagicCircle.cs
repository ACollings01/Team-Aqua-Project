using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MagicCircle : MonoBehaviour
{
    private AudioSource magicCircleAudioSource;
    public AudioClip portalSound;
    private GameManager _gameManager;


    public GameObject mMenu; //Play menu in unity ..to trigger to pop up

    void Start()
    {
        _gameManager = GameManager.Instance;
        magicCircleAudioSource = GetComponent<AudioSource>();
        mMenu.SetActive(false);
    }

    void OnTriggerEnter(Collider player)
    {
        if (player.gameObject.tag == "Player")
        {
            if(this.name == "Magic Circle")
            {
                magicCircleAudioSource.PlayOneShot(portalSound, 0.5f);
                StartCoroutine(LoadLevel2());
            }
            else
            {
                Debug.Log("I hit the shop today");
                mMenu.SetActive(true); //main menu pop up when player enters magic circle
            }
        }
    }

    IEnumerator LoadLevel2()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("ReLevel2");
    }
}
