using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    protected Animator anim;
    protected GameObject player;

    public float speed;
    public int health;
    public int lasthp;

    public int minDamage;
    public int maxDamage;
    public AudioSource audioSource;

    public LayerMask whatIsPlayer;
    public float attackRadius;

    public ParticleSystem blood;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");

    }

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        lasthp = health;
    }

       //////////////////////////////////////////////////////////////////////////////////////////////////////

        //void OnDrawGizmosSelected()
        //{
        //    GameObject slimeFace = GameObject.Find("Bandit/BanditWeapon");
        //    Gizmos.color = Color.red;
        //    Gizmos.DrawWireSphere(slimeFace.transform.position, attackRadius);
        //}


    public int dealDamageToPlayer(int min, int max)
    {
        int damage = Random.Range(min, max + 1);
        return damage;
    }

    public void damageCheck()
    {
        if (health < lasthp) //Checks to see if the enemy has recently taken damage
        {
            lasthp = health; //If so, reset it
            var bloodSystem = Instantiate(blood, transform.position, Quaternion.identity); //Spawn the blood particle system
            Destroy(bloodSystem.gameObject, 1f); //Destroy that system after 1 second
        }
    }
}
