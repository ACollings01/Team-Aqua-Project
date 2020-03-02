using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeSc : EnemyAI
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
            slimeSplit();
            Destroy(this.gameObject);
        }
    }

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
        if (gameObject.name == "Slime")
        {
            if (health <= 0)
            {
                int splitRand = Random.Range(2, 4);

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

}
