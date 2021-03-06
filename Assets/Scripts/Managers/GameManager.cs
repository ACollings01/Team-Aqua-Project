﻿using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    protected GameManager() { }

    private static GameObject _player;

    public GameObject weaponSelector;

    private void Awake()
    {
        weaponSelector.SetActive(true);
    }

    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    // Returns the player GameObject
    public GameObject GetPlayer()
    {
        return _player;
    }

    // Script will be filled later, will activate when player steps on a magic circle
    public void ActivateMCircle()
    {
        SceneManager.LoadScene("MagicCircleUI");
    }

    public void ActivateShop()
    {
        SceneManager.LoadScene("ShopUI");
    }
}
