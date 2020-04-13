﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MagicCircle : MonoBehaviour
{
    private AudioSource magicCircleAudioSource;
    private GameManager _gameManager;
    private bool canRunAudio = true;

    public GameObject mMenu; //Play menu in unity ..to trigger to pop up

    void Start()
    {
        _gameManager = GameManager.Instance;
        magicCircleAudioSource = GetComponent<AudioSource>();
        mMenu.SetActive(false);
    }

    void OnTriggerEnter(Collider player)
    {
        if (player.gameObject.tag == "Player" && canRunAudio)
        {
            mMenu.SetActive(true); //main menu pop up when player enters magic circle
            StartCoroutine(ExitMap());
            canRunAudio = false;
            magicCircleAudioSource.Play();
            SceneManager.LoadScene("Relevel2");
        }
    }

    IEnumerator ExitMap()
    {
        yield return new WaitForSeconds(1.5f);
    }
}
