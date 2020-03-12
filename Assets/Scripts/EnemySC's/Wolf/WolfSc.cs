﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfSc : EnemyAI
{
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        crawlToSurface();

        anim.SetFloat("Distance", Vector3.Distance(transform.position, player.transform.position));

        if (this.health <= 0)
        {
            Destroy(this.gameObject);
        }
    }


    public void attackWolf(GameObject player)
    {
        GameObject wolfHead = GameObject.Find("Wolf/WolfBody/Head");

        Collider[] playerHit = Physics.OverlapSphere(wolfHead.transform.position, attackRadius, whatIsPlayer);

        for (int i = 0; i < playerHit.Length; i++)
        {
            Debug.Log("The player has been hit by the Wolf!");
            playerHit[i].GetComponent<Player>().health -= dealDamageToPlayer(minDamage, maxDamage);
            //Attack twice
        }
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
        }
    }
}
