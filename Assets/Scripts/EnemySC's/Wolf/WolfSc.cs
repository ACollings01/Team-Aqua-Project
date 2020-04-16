using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WolfSc : EnemyAI
{
    public GameObject inv;
    public AudioClip howl;
    public AudioClip attack;
    //public AudioSource damagetaken;
    public AudioClip death;
    
    bool spawned = false;
    bool playSound = false;
    private bool isDead;

    // Start is called before the first frame update
    void Start()
    {
        lasthp = health;
        this.name = "Wolf";
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        inv = GameObject.Find("InventoryScreen");
    }

    // Update is called once per frame
    void Update()
    {
        if(!spawned) //Checks to see if the wolf has spawned or not
            crawlToSurface(); //If it already hasn't, then run the spawning function

        float DistToPlayer = Vector3.Distance(transform.position, player.transform.position);
        anim.SetFloat("Distance", DistToPlayer); //Determines the distance between the player and the wolf
        audioSource = GetComponent<AudioSource>();

        //Only play howl sounds when they are within 10 ft
        if (!audioSource.isPlaying && DistToPlayer < 10)
        {
            audioSource.PlayOneShot(howl, 0.5f);
        }

        damageCheck();

        if (this.health <= 0) //If the wolf's health is less than 0 (dead)
        {
            if (playSound == false) //Have a check to make sure the death sound only plays once
            {
                playSound = true;
                audioSource.PlayOneShot(death); //Play the death sound
            }
            if (!isDead)
            {
                inv.GetComponent<DisplayInventory>().inventory.Container[0].AddAmount(6);
                Destroy(this.gameObject, 3f);
            }
            isDead = true;
        }
    }


    public void attackWolf(GameObject player)
    {
        GameObject wolfHead = GameObject.Find("Wolf/Head");


        //SoundManager.Instance.PlayClip(howl);

        //SoundManager.Instance.PlayClip(audioSource);


        Collider[] playerHit = Physics.OverlapSphere(wolfHead.transform.position, attackRadius, whatIsPlayer);

        for (int i = 0; i < playerHit.Length; i++)
        {
            Debug.Log("The player has been hit by the Wolf!");
            playerHit[i].GetComponent<Player>().health -= dealDamageToPlayer(minDamage, maxDamage);
            //Attack twice
            //SoundManager.Instance.PlayClip(audioSource);

        }

        //SoundManager.Instance.PlayClip(attack);
        audioSource.PlayOneShot(attack);

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
