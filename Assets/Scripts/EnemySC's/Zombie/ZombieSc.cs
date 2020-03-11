using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSc : EnemyAI
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

    public void attackZombie(GameObject player)
    {
        if (gameObject.name == "Zombie")
        {
            Collider[] playerHit = Physics.OverlapSphere(transform.position, attackRadius, whatIsPlayer);

            for (int i = 0; i < playerHit.Length; i++)
            {
                player.GetComponent<Player>().health -= dealDamageToPlayer(minDamage, maxDamage);
                Debug.Log("The player has been hit by the Zombie!");
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
        }
    }
}
