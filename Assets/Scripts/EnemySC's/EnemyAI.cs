using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    Animator anim;
    public GameObject player;

    public float speed;

    public Sword dealDamage;

    public int health;


    //Variables for Bat
    public GameObject spitPrefab;
    int rand;

    //Variables for Bandit
    public LayerMask whatIsPlayer;
    public float attackRadius;


    public GameObject GetPlayer()
    {
        return player;
    }

    // Start is called before the first frame update
    void Start()
    {
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
            Destroy(this.gameObject);
        }
    }

    public void getPosition(GameObject Player)
    {

        if (gameObject.tag == "Bat")
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
        if (this.gameObject.tag == "Bat")
        {
            anim.SetBool("isAttacking", true);
            GameObject spit;
            Rigidbody rb;

            spit = Instantiate(spitPrefab, new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), Quaternion.identity);
            rb = spit.GetComponent<Rigidbody>();

            var direction = spit.transform.position - player.transform.position;
            spit.transform.LookAt(player.transform.position);
            rb.AddForce(spit.transform.forward * 500.0f);
            //spit.transform.Translate(0, 0, Time.deltaTime * 5);
        }
    }

    public void attackBandit(GameObject player)
    {
        if(this.gameObject.tag == "Bandit")
        {
            GameObject banditWeapon = GameObject.Find("Bandit/BanditWeapon");

            Collider[] playerHit = Physics.OverlapSphere(banditWeapon.transform.position, attackRadius, whatIsPlayer);

            for(int i = 0; i < playerHit.Length; i++)
            {
                //Debug.Log("The player has been hit by the Bandit!");
            }
        }
    }

    public void attackWolf(GameObject player)
    {
        if (this.gameObject.tag == "Wolf")
        {
            GameObject wolfHead = GameObject.Find("Wolf/WolfBody/Head");

            Collider[] playerHit = Physics.OverlapSphere(wolfHead.transform.position, attackRadius, whatIsPlayer);

            for (int i = 0; i < playerHit.Length; i++)
            {
                Debug.Log("The player has been hit by the Wolf!");
                //Attack twice
            }
            transform.Translate(-Vector3.forward * Time.deltaTime * speed);
        }
    }

    //void OnDrawGizmosSelected()
    //{
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawWireSphere(banditWeapon.transform.position, attackRadius);
    //}
    

    int damageRecieved()
    {
        int damageDone = dealDamage.swordDamageDone();
        health -= damageDone;
        return health;
    }

    void OnCollisionEnter(Collision weapon)
    {
        if (weapon.collider.tag == "Sword")
        {
            damageRecieved();
        }
    }

}
