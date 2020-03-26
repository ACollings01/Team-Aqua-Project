﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BanditRangedSc : EnemyAI
{
    bool spawned = false;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!spawned)
            crawlToSurface();

        anim.SetFloat("Distance", Vector3.Distance(transform.position, player.transform.position));

        if (this.health <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    public void attackBandit(GameObject player)
    {
        anim.SetBool("isAttacking", true);
        GameObject arrowPrefab = (GameObject)Resources.Load("Projectiles/BanditArrow");
        GameObject arrow;
        Rigidbody rb;

        arrow = Instantiate(arrowPrefab, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
        //spit.transform.parent = gameObject.transform;
        rb = arrow.GetComponent<Rigidbody>();

        arrow.transform.LookAt(player.transform.position);
        rb.AddForce(arrow.transform.forward * 500.0f);
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