using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniSlimeSc : EnemyAI
{
    public AudioClip slime;

    private bool spawned = false;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

        name = "MiniSlime";
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
            Destroy(this.gameObject);
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
            spawned = true;
        }
    }

}
