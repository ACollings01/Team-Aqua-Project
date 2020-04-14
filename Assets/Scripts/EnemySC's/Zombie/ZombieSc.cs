using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieSc : EnemyAI
{
    [SerializeField] public AudioClip zombie;
    [SerializeField] public AudioClip death;

    public GameObject inv;
    bool spawned = false;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();

        gameObject.name = "Zombie";
    }

    // Update is called once per frame
    void Update()
    {
        if(!spawned)
            crawlToSurface();

        float DistToPlayer = Vector3.Distance(transform.position, player.transform.position);
        anim.SetFloat("Distance", DistToPlayer);
        audioSource = GetComponent<AudioSource>();

        //only play zombie sounds when they are within 10ft
        if (!audioSource.isPlaying && DistToPlayer < 10)
        {
            audioSource.PlayOneShot(zombie, 0.5f);
        }

        if (this.health <= 0)
        {
            inv.GetComponent<DisplayInventory>().inventory.Container[0].AddAmount(12);
            Destroy(this.gameObject);
        }
    }

    public void attackZombie(GameObject player)
    {
        Collider[] playerHit = Physics.OverlapSphere(transform.position, attackRadius, whatIsPlayer);

        for (int i = 0; i < playerHit.Length; i++)
        {
            playerHit[i].GetComponent<Player>().health -= dealDamageToPlayer(minDamage, maxDamage);
            Debug.Log("The player has been hit by the Zombie!");
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
            this.GetComponent<NavMeshAgent>().enabled = true;
            spawned = true;
        }
    }
}
