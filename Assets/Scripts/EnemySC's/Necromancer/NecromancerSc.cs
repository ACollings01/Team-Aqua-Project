using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NecromancerSc : EnemyAI
{
    private AudioSource EpicFightAudioSource;
    private bool canRunAudio = true;
    public GameObject Necromancer;
    public AudioClip EpicFight;
    [SerializeField] public AudioClip fireballshot;
    [SerializeField] public AudioClip fireballhit;

    public GameObject inv;
    bool spawned = false;
    private bool isDead;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        EpicFightAudioSource = GetComponent<AudioSource>();
        Necromancer.SetActive(false);
        inv = GameObject.Find("InventoryScreen");
    }

    // Update is called once per frame
    void Update()
    {
        if(!spawned)
            crawlToSurface();

        float DistToPlayer = Vector3.Distance(transform.position, player.transform.position);
        anim.SetFloat("Distance", DistToPlayer);
        audioSource = GetComponent<AudioSource>();

        // only play EpicFight sounds when they are within 15ft
        if (!audioSource.isPlaying && DistToPlayer < 15)
        {
            audioSource.PlayOneShot(EpicFight, 0.5f);
        }

        if (this.health <= 0)
        {
            if (!isDead)
            {
                inv.GetComponent<DisplayInventory>().inventory.Container[0].AddAmount(100);
                Destroy(this.gameObject, 2f);
            }
            isDead = true;
        }

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Necromancer" && canRunAudio)
        {
            Necromancer.SetActive(true);
            canRunAudio = false;
            EpicFightAudioSource.Play();
        }
    }

    public void shootFireball(GameObject player)
    {
        anim.SetTrigger("shootFireball");  //Set animator to shootingFireball

        GameObject fireballPrefab = (GameObject)Resources.Load("Projectiles/Fireball");             //Find the Fireball projectile GameObject
        GameObject fireball = Instantiate(fireballPrefab, transform.position, Quaternion.identity); //Instantiate said GameObject

        Rigidbody rb;   //Launch the Fireball towards the player
        rb = fireball.GetComponent<Rigidbody>();

        fireball.transform.LookAt(player.transform.position);
        rb.AddForce(fireball.transform.forward * 500.0f);
        audioSource.PlayOneShot(fireballshot, 0.5f);


    }

    public void summonZombies(GameObject player)
    {
        anim.SetTrigger("summonZombies");  //Set animator to summoningZombies

        int rand = Random.Range(2, 4); //Randomize the amount of zombies that will spawn

        for (int i = 0; i < rand; i++)
        {
            Vector3 distance = new Vector3(transform.position.x + Random.Range(-4f, 4f), -2, transform.position.z + Random.Range(-4f, 4f));

            GameObject zombiePrefab = (GameObject)Resources.Load("Zombie(Necromancer)");
            GameObject zombie = Instantiate(zombiePrefab, distance, Quaternion.identity);
            zombie.name = "Zombie";
        }

 
    }

    public void shootHomingFireballs(GameObject player)
    {
        anim.SetTrigger("shootHomingFireballs");  //Set animator to shootingHomingFireballs

        StartCoroutine(spawnHomingFireballs());

    }

    IEnumerator spawnHomingFireballs()
    {
        int rand = Random.Range(4, 9); //Set the number of homingFireBalls that will spawn

        for (int i = 1; i <= rand; i++)
        {
            GameObject homingPrefab = (GameObject)Resources.Load("Projectiles/HomingFireball"); //Find and retrieve the homingFireball prefab
            GameObject homingFireball = Instantiate(homingPrefab, transform.Find("fireballPos" + i.ToString()).transform.position, Quaternion.identity); //Instantiate the homingFireball prefab
            homingFireball.name = "homingFireball";

            yield return new WaitForSeconds(0.3f);
        }

        GameObject[] homingFireballs = GameObject.FindGameObjectsWithTag("HomingFireball");

        for (int i = 0; i < homingFireballs.Length; i++)
        {
            homingFireballs[i].GetComponent<HomingFireball>().enabled = true;
        }
    }

    public int dealHomingDamage(int min, int max)
    {
        int damage = Random.Range(min, max);
        return damage;
    }

    public int dealFireballDamage(int min, int max)
    {
        int damage = Random.Range(min + 30, max + 40);
        return damage;
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
