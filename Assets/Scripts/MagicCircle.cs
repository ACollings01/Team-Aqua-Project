using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MagicCircle : MonoBehaviour
{
    private AudioSource magicCircleAudioSource;
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
            Debug.Log("I hit the shop today");
            //mMenu.SetActive(true); //main menu pop up when player enters magic circle
            SceneManager.LoadScene("ReLevel2");
            StartCoroutine(ExitMap());
            //canRunAudio = false;
            magicCircleAudioSource.Play();
           
            //magicCircleAudioSource.PlayOneShot();
        }
    }

    IEnumerator ExitMap()
    {
        yield return new WaitForSeconds(1.5f);
    }
}
