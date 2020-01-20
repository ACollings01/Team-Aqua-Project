using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Game game;
    public float health;
    public float armour;
    // Start is called before the first frame update
    void Start()
    {

    }

    public void TakeDamage(float amount)
    {
        float healthDamage = amount;
        health -= healthDamage * armour;

        if (health <= 0)
        {
            game.GameOver();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
