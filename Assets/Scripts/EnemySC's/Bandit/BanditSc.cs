using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BanditSc : EnemyAI
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

    public void attackBandit(GameObject player)
    {
        if (gameObject.name == "Bandit")
        {
            GameObject banditWeapon = GameObject.Find("Bandit/BanditWeapon");

            Collider[] playerHit = Physics.OverlapSphere(banditWeapon.transform.position, attackRadius, whatIsPlayer);

            for (int i = 0; i < playerHit.Length; i++)
            {
                player.GetComponent<Player>().health -= dealDamageToPlayer(minDamage, maxDamage);
                Debug.Log("The player has been hit by the Bandit!");
            }
        }
    }
}
