using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BanditRangedSc : EnemyAI
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
        gameObject.name = "RangedBandit";
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

        if (this.health <= 0)
        {
            if (!isDead)
            {
                inv.GetComponent<DisplayInventory>().inventory.Container[0].AddAmount(2);
                Destroy(this.gameObject, 1.5f);
            }
            isDead = true;
        }
    }

    public void attackBandit(GameObject player)
    {
        anim.SetBool("isAttacking", true);
        GameObject arrowPrefab = (GameObject)Resources.Load("Projectiles/BanditArrow");
        GameObject arrow;
        Rigidbody rb;

        GameObject arrowShot = GameObject.Find("RangedBandit/Fire");

        arrow = Instantiate(arrowPrefab, new Vector3(arrowShot.transform.position.x, arrowShot.transform.position.y, arrowShot.transform.position.z), Quaternion.identity);
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
            this.GetComponent<NavMeshAgent>().enabled = true;
            spawned = true;
        }
    }
}
