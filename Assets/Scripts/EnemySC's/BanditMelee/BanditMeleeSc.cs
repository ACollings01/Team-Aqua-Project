using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BanditMeleeSc : EnemyAI
{

    private AudioSource audioSource;

    bool spawned = false;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!spawned)
            crawlToSurface();

        anim.SetFloat("Distance", Vector3.Distance(transform.position, player.transform.position));
        //SoundManager.Instance.PlayClip(audioSource);

        if (this.health <= 0)
        {
            Destroy(this.gameObject);
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
            spawned = true;
        }
    }
}
