using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    Animator anim;
    public GameObject player;
    public Sword dealDamage;

    public int health;

    //Variables for Bat
    public GameObject spitPrefab;
    int rand;

    //Variables for Bandit
    public LayerMask whatIsPlayer;
    public GameObject banditWeapon;
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
        Debug.Log(rand);
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
                this.transform.Translate(Vector3.left * Time.deltaTime * 5f);
            else
                this.transform.Translate(Vector3.right * Time.deltaTime * 5f);
        }
    }

    public void attackBat(GameObject player)
    {
        if (this.gameObject.tag == "Bat")
        {
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
            Collider[] playerHit = Physics.OverlapSphere(banditWeapon.transform.position, attackRadius, whatIsPlayer);

            for(int i = 0; i < playerHit.Length; i++)
            {
                Debug.Log("The player has been hit by the Bandit!");
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(banditWeapon.transform.position, attackRadius);
    }

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
            Debug.Log("Whap");
            damageRecieved();
        }
    }

}
