using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfSc : EnemyAI
{

    //public AudioSource howl;
    public AudioClip attack;
    //public AudioSource damagetaken;
    public AudioClip death;
    
    private AudioSource wolfAudioSource;
    bool spawned = false;


    // Start is called before the first frame update
    void Start()
    {
        this.name = "Wolf";
        anim = GetComponent<Animator>();
        wolfAudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!spawned)
            crawlToSurface();

        anim.SetFloat("Distance", Vector3.Distance(transform.position, player.transform.position));

        if (this.health <= 0)
        {
            wolfAudioSource.PlayOneShot(death);

            Destroy(this.gameObject);
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
        wolfAudioSource.PlayOneShot(attack);

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
