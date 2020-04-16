using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BanditMeleeSc : EnemyAI
{
    [SerializeField] public AudioClip bandit;
    [SerializeField] public AudioClip attack;
    [SerializeField] public AudioClip damage;
    [SerializeField] public AudioClip death;

    public GameObject inv;
    bool spawned = false;
    private bool isDead;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        inv = GameObject.Find("InventoryScreen");

        gameObject.name = "MeleeBandit";
    }

    // Update is called once per frame
    void Update()
    {
        if(!spawned)
            crawlToSurface();

        float DistToPlayer = Vector3.Distance(transform.position, player.transform.position);
        anim.SetFloat("Distance", DistToPlayer);
        audioSource = GetComponent<AudioSource>();

        // only play bandit sounds when they are within 10 ft
        if (!audioSource.isPlaying && DistToPlayer < 10)
        {
            audioSource.PlayOneShot(bandit, 0.5f);
        }

        damageCheck();

        if (this.health <= 0)
        {
            if (!isDead)
            {
                inv.GetComponent<DisplayInventory>().inventory.Container[0].AddAmount(12);
                Destroy(this.gameObject, 1.5f);
            }
            isDead = true;
        }
    }

    public void attackBandit(GameObject player)
    {
        GameObject banditWeapon = GameObject.Find("Bandit/BanditWeapon");

        Collider[] playerHit = Physics.OverlapSphere(banditWeapon.transform.position, attackRadius, whatIsPlayer);

        for (int i = 0; i < playerHit.Length; i++)
        {
            playerHit[i].GetComponent<Player>().health -= dealDamageToPlayer(minDamage, maxDamage);
            Debug.Log("The player has been hit by the Bandit!");
        }
        audioSource.PlayOneShot(attack);
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
