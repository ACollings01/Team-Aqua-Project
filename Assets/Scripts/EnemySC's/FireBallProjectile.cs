﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallProjectile : MonoBehaviour
{
    GameObject Necromancer;
    [SerializeField] public AudioClip fireball;

    // Start is called before the first frame update
    void Start()
    {
        Necromancer = GameObject.Find("Necromancer");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<Player>().health -= Necromancer.GetComponent<NecromancerSc>().dealFireballDamage(Necromancer.GetComponent<NecromancerSc>().minDamage, Necromancer.GetComponent<NecromancerSc>().maxDamage);
            Destroy(gameObject);
        }
    }
}
