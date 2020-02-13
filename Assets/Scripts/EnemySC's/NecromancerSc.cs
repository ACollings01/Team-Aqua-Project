﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NecromancerSc : EnemyAI
{
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void shootFireball(GameObject player)
    {
        anim.SetBool("shootFireball", true);  //Set animator to shootingFireball

        GameObject fireballPrefab = (GameObject)Resources.Load("Projectiles/Fireball");             //Find the Fireball projectile GameObject
        GameObject fireball = Instantiate(fireballPrefab, transform.position, Quaternion.identity); //Instantiate said GameObject

        Rigidbody rb;   //Launch the Fireball towards the player
        rb = fireball.GetComponent<Rigidbody>();

        fireball.transform.LookAt(player.transform.position);
        rb.AddForce(fireball.transform.forward * 500.0f);
    }

    public void summonZombies(GameObject player)
    {
        anim.SetBool("summonZombies", true);  //Set animator to summoningZombies

        int rand = Random.Range(2, 4); //Randomize the amount of zombies that will spawn

        Vector3 distance = new Vector3(transform.position.x + Random.Range(-4f, 4f), 0, transform.position.z + Random.Range(-4f, 4f));

        for (int i = 0; i < rand; i++)
        {
            GameObject zombiePrefab = (GameObject)Resources.Load("Zombie(Necromancer)");
            GameObject zombie = Instantiate(zombiePrefab, distance, Quaternion.identity);
            zombie.name = "Zombie";
        }

    }

    public void shootHomingFireballs(GameObject player)
    {
        anim.SetBool("shootHomingFireballs", true);  //Set animator to shootingHomingFireballs

        int rand = Random.Range(4, 9); //Set the number of homingFireBalls that will spawn

        for (int i = 1; i <= rand; i++)
        {
            
            GameObject homingPrefab = (GameObject)Resources.Load("Projectiles/HomingFireball"); //Find and retrieve the homingFireball prefab
            GameObject homingFireball = Instantiate(homingPrefab, transform.Find("fireballPos" + i.ToString()).transform.position, Quaternion.identity); //Instantiate the homingFireball prefab
        }
    }

}
