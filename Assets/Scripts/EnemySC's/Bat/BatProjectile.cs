﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatProjectile : BatSc
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Destroy(gameObject, 5f);
    }

    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<Player>().health -= dealDamageToPlayer(minDamage, maxDamage);
            Destroy(gameObject);
        }
    }
}
