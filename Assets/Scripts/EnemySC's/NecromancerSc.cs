using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NecromancerSc : EnemyAI
{
    // Start is called before the first frame update
    void Start()
    {

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
    }

    public void shootHomingFireballs(GameObject player)
    {
        anim.SetBool("shootHomingFireballs", true);  //Set animator to shootingHomingFireballs

        GameObject homingPrefab = (GameObject)Resources.Load("Projectiles/HomingFireball"); //Find and retrieve the homingFireball prefab
        GameObject homingFireball = Instantiate(homingPrefab, transform.position, Quaternion.identity); //Instantiate the homingFireball prefab
    }

}
