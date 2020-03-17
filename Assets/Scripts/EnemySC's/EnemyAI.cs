using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    Animator anim;
    GameObject player;

    public float speed;
    public int health;

    public int minDamage;
    public int maxDamage;
    private AudioSource audioSource;

    //Variables for Bat
    int rand;
    GameObject[] spitProjectiles;

    //Variables for Bandit
    public LayerMask whatIsPlayer;
    public float attackRadius;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
   
    }

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
        rand = Random.Range(0, 2);
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetFloat("Distance", Vector3.Distance(transform.position, player.transform.position));

        //Debug.Log("health = " + health);

        if (health <= 0)
        {
            slimeSplit();
            Destroy(this.gameObject);
        }

        spitProjectiles = GameObject.FindGameObjectsWithTag("Projectile");
        foreach(GameObject spitProjectile in spitProjectiles)
        {
            float distance = Vector3.Distance(spitProjectile.transform.position, spitProjectile.transform.parent.gameObject.transform.position);

            if(distance > 50)
            {
                Destroy(spitProjectile);
            }
        }
    }

    ////////////////////////////////////////////////// BAT /////////////////////////////////////////////////

    public void getPosition(GameObject Player)
    {

        if (gameObject.name == "Bat")
        {
            if (rand == 0)
                this.transform.Translate(Vector3.left * Time.deltaTime * speed);
            else
                this.transform.Translate(Vector3.right * Time.deltaTime * speed);


            anim.SetBool("isAttacking", false);
        }
    }

    public void attackBat(GameObject player)
    {
        if (gameObject.name == "Bat")
        {
            anim.SetBool("isAttacking", true);
            GameObject spitPrefab = (GameObject)Resources.Load("Projectiles/BatProjectile");
            GameObject spit;
            Rigidbody rb;

            spit = Instantiate(spitPrefab, new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), Quaternion.identity);
            spit.transform.parent = gameObject.transform;
            rb = spit.GetComponent<Rigidbody>();

            var direction = spit.transform.position - player.transform.position;
            spit.transform.LookAt(player.transform.position);
            rb.AddForce(spit.transform.forward * 500.0f);
            //spit.transform.Translate(0, 0, Time.deltaTime * 5);
        }
    }

    ///////////////////////////////////// BANDIT //////////////////////////////////////////////////////

    public void attackBandit(GameObject player)
    {
        if(gameObject.name == "Bandit")
        {
            GameObject banditWeapon = GameObject.Find("Bandit/BanditWeapon");

            Collider[] playerHit = Physics.OverlapSphere(banditWeapon.transform.position, attackRadius, whatIsPlayer);

            for(int i = 0; i < playerHit.Length; i++)
            {
                player.GetComponent<Player>().health -= dealDamageToPlayer(minDamage, maxDamage);
                Debug.Log("The player has been hit by the Bandit!");
            }
        }
    }

    //////////////////////////////////// WOLF ///////////////////////////////////////////////

    public void attackWolf(GameObject player)
    {
        if (gameObject.name == "Wolf")
        {
            SoundManager.Instance.PlayClip(this.GetComponent<AudioSource>());
            GameObject wolfHead = GameObject.Find("Wolf/WolfBody/Head");

            Collider[] playerHit = Physics.OverlapSphere(wolfHead.transform.position, attackRadius, whatIsPlayer);

            for (int i = 0; i < playerHit.Length; i++)
            {
                Debug.Log("The player has been hit by the Wolf!");
                player.GetComponent<Player>().health -= dealDamageToPlayer(minDamage, maxDamage);
                //Attack twice
                SoundManager.Instance.PlayClip(this.GetComponent<AudioSource>());
            }
            transform.Translate(-Vector3.forward * Time.deltaTime * speed);
        }
    }

    /////////////////////////////////// SLIME /////////////////////////////////////////////////

    public void attackSlime(GameObject player)
    {
        if (gameObject.name == "Slime")
        {

            GameObject slimeFace = GameObject.Find("Slime/SlimeFace");

            Collider[] playerHit = Physics.OverlapSphere(slimeFace.transform.position, attackRadius, whatIsPlayer);

            for (int i = 0; i < playerHit.Length; i++)
            {
                Debug.Log("The player has been hit by the Slime!");
                player.GetComponent<Player>().health -= dealDamageToPlayer(minDamage, maxDamage);
                //Attack twice
            }
            transform.Translate(-Vector3.forward * Time.deltaTime * speed);
        }
    }

    public void slimeSplit()
    {
        if(gameObject.name == "Slime")
        {
            if(health <= 0)
            {
                int splitRand = Random.Range(2,4);

                for (int i = 0; i < splitRand; i++)
                {
                    GameObject miniSlime;
                    GameObject miniSlimePrefab = (GameObject)Resources.Load("miniSlime");
                    Vector3 miniSlimePos = new Vector3(transform.position.x + Random.Range(-4, 4),
                                                        transform.position.y,
                                                        transform.position.z + Random.Range(-4, 4));

                    miniSlime = Instantiate(miniSlimePrefab, miniSlimePos, Quaternion.identity);
                }
            }
        }
    }

    public void attackMiniSlime(GameObject player)
    {
        if (gameObject.name == "miniSlime")
        {
            GameObject slimeFace = GameObject.Find("miniSlime/SlimeFace");

            Collider[] playerHit = Physics.OverlapSphere(slimeFace.transform.position, attackRadius, whatIsPlayer);

            for (int i = 0; i < playerHit.Length; i++)
            {
                Debug.Log("The player has been hit by the Mini Slime!");
                player.GetComponent<Player>().health -= dealDamageToPlayer(minDamage, maxDamage);
                //Attack twice
            }
            transform.Translate(-Vector3.forward * Time.deltaTime * speed);
        }
    }

    //////////////////////////////////////////////////////////////////////////////////////////////////////

    //void OnDrawGizmosSelected()
    //{
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawWireSphere(banditWeapon.transform.position, attackRadius);
    //}


    int dealDamageToPlayer(int min, int max)
    {
        int damage = Random.Range(min, max + 1);
        return damage;
    }
}
