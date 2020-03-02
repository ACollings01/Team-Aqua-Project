using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfSc : EnemyAI
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
    }


    public void attackWolf(GameObject player)
    {
        if (gameObject.name == "Wolf")
        {
            GameObject wolfHead = GameObject.Find("Wolf/WolfBody/Head");

            Collider[] playerHit = Physics.OverlapSphere(wolfHead.transform.position, attackRadius, whatIsPlayer);

            for (int i = 0; i < playerHit.Length; i++)
            {
                Debug.Log("The player has been hit by the Wolf!");
                player.GetComponent<Player>().health -= dealDamageToPlayer(minDamage, maxDamage);
                //Attack twice
            }
            transform.Translate(-Vector3.forward * Time.deltaTime * speed);
        }
    }
}
