﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MiniSlimeSc : EnemyAI
{
    public GameObject inv;
    public AudioClip slime;

    private bool spawned = false;

    bool isDead = false;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        anim.SetFloat("Health", health);
        inv = GameObject.Find("InventoryScreen");

        this.name = "MiniSlime";
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetFloat("Distance", Vector3.Distance(transform.position, player.transform.position));

        if (!spawned)
            crawlToSurface();

        damageCheck();

        anim.SetFloat("Health", health);

        if (health <= 0)
        {
            if (!isDead)
            {
                inv.GetComponent<DisplayInventory>().inventory.Container[0].AddAmount(2);
                Destroy(this.gameObject, 1.5f);
            }
            isDead = true;

        }
    }


    public void attackMiniSlime(GameObject player)
    {
        GameObject slimeFace = GameObject.Find("MiniSlime/SlimeFace");

        Collider[] playerHit = Physics.OverlapSphere(slimeFace.transform.position, attackRadius, whatIsPlayer);

        for (int i = 0; i < playerHit.Length; i++)
        {
            Debug.Log("The player has been hit by the Mini Slime!");
            playerHit[i].GetComponent<Player>().health -= dealDamageToPlayer(minDamage, maxDamage);
            //Attack twice
        }
        audioSource.PlayOneShot(slime);

        transform.Translate(-Vector3.forward * Time.deltaTime * speed);   
    }


    void crawlToSurface()
    {
        if (transform.position.y <= 1)
        {
            transform.Translate(Vector3.up * Time.deltaTime * 2);
        }
        else
        {
            this.GetComponent<Animator>().enabled = true;
            this.GetComponent<Collider>().enabled = true;
            this.GetComponent<Rigidbody>().useGravity = true;
            this.GetComponent<NavMeshAgent>().enabled = true;
            spawned = true;
        }
    }

}
