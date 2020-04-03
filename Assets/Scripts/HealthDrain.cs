using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthDrain : Player
{

    public GameObject player;

    Image Health;
    float healthHold = 50f;
    float hp;
    // Start is called before the first frame update
    void Start()
    {
        Health = GetComponent<Image>();

        //hp = healthHold;

    }

    // Update is called once per frame
    void Update()
    {
        hp = player.GetComponent<Player>().health;
        Health.fillAmount = hp / healthHold;

    }
}