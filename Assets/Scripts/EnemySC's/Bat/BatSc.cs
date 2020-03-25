using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatSc : EnemyAI
{
    int rand;
    GameObject[] spitProjectiles;

    bool spawned = false;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rand = Random.Range(0, 2);
    }

    // Update is called once per frame
    void Update()
    {
        if(!spawned)
            crawlToSurface();

        anim.SetFloat("Distance", Vector3.Distance(transform.position, player.transform.position));

        spitProjectiles = GameObject.FindGameObjectsWithTag("Projectile");
        foreach (GameObject spitProjectile in spitProjectiles)
        {
            float distance = Vector3.Distance(spitProjectile.transform.position, GameObject.Find("Player").transform.position);

            if (distance > 50)
            {
                Destroy(spitProjectile);
            }
        }

        if (this.health <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    public void moveAroundPlayer(GameObject Player)
    {
        if (rand == 0)
            this.transform.Translate(Vector3.left * Time.deltaTime * speed);
        else
            this.transform.Translate(Vector3.right * Time.deltaTime * speed);


        anim.SetBool("isAttacking", false);
    }

    public void attackBat(GameObject player)
    {
        anim.SetBool("isAttacking", true);
        GameObject spitPrefab = (GameObject)Resources.Load("Projectiles/BatProjectile");
        GameObject spit;
        Rigidbody rb;

        spit = Instantiate(spitPrefab, new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), Quaternion.identity);
        //spit.transform.parent = gameObject.transform;
        rb = spit.GetComponent<Rigidbody>();

        spit.transform.LookAt(player.transform.position);
        rb.AddForce(spit.transform.forward * 500.0f);
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
            spawned = true;
        }
    }
}
