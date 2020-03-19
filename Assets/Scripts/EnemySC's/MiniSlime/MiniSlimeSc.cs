using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniSlimeSc : EnemyAI
{
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetFloat("Distance", Vector3.Distance(transform.position, player.transform.position));

        if (health <= 0)
        {
            Destroy(this.gameObject);
        }
    }


    public void attackMiniSlime(GameObject player)
    {
        GameObject slimeFace = GameObject.Find("miniSlime/SlimeFace");

        Collider[] playerHit = Physics.OverlapSphere(slimeFace.transform.position, attackRadius, whatIsPlayer);

        for (int i = 0; i < playerHit.Length; i++)
        {
            Debug.Log("The player has been hit by the Mini Slime!");
            playerHit[i].GetComponent<Player>().health -= dealDamageToPlayer(minDamage, maxDamage);
            //Attack twice
        }
        transform.Translate(-Vector3.forward * Time.deltaTime * speed);   
    }

}
