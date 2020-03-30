﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeSc : EnemyAI
{

    public AudioClip slime;
    public AudioClip split;

    bool spawned = false;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!spawned)
            crawlToSurface();


        //audioSource.PlayOneShot(slime);


        anim.SetFloat("Distance", Vector3.Distance(transform.position, player.transform.position));

        if (health <= 0)
        {
            slimeSplit();
            Destroy(this.gameObject);
        }
    }

    public void attackSlime(GameObject player)
    {
        GameObject slimeFace = GameObject.Find("Slime/SlimeFace");

        Collider[] playerHit = Physics.OverlapSphere(slimeFace.transform.position, attackRadius, whatIsPlayer);

        for (int i = 0; i < playerHit.Length; i++)
        {
            Debug.Log("The player has been hit by the Slime!");
            player.GetComponent<Player>().health -= dealDamageToPlayer(minDamage, maxDamage);
            //Attack twice
        }

        audioSource.PlayOneShot(slime);

        transform.Translate(-Vector3.forward * Time.deltaTime * speed);
    }

    public void slimeSplit()
    {
        if (health <= 0)
        {
            int splitRand = Random.Range(2, 4);

            audioSource.PlayOneShot(split);

            for (int i = 0; i < splitRand; i++)
            {
                GameObject miniSlime;
                GameObject miniSlimePrefab = (GameObject)Resources.Load("miniSlime");
                Vector3 miniSlimePos = new Vector3(transform.position.x + Random.Range(-4, 4),
                                                    transform.position.y,
                                                    transform.position.z + Random.Range(-4, 4));

                miniSlime = Instantiate(miniSlimePrefab, miniSlimePos, Quaternion.identity);
            }
        }
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
            spawned = true;
        }
    }

}
