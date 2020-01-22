using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    Animator anim;
    public GameObject player;
    public GameObject spitPrefab;

    public GameObject GetPlayer()
    {
        return player;
    }

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

    public void getPosition(GameObject Player)
    {
        Vector3 newPos = new Vector3(Random.Range(-10, 10), Random.Range(-10, 10), Random.Range(-10, 10));

        if(Player.transform.position.x < 0)
        {
            newPos.x -= Player.transform.position.x;
        }
        else
        {
            newPos.x += Player.transform.position.x;
        }

        if (Player.transform.position.y < 0)
        {
            newPos.y -= Player.transform.position.y;
        }
        else
        {
            newPos.y += Player.transform.position.y;
        }

        if (Player.transform.position.z < 0)
        {
            newPos.z -= Player.transform.position.z;
        }
        else
        {
            newPos.z += Player.transform.position.z;
        }

        var direction = newPos - this.transform.position;
        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(direction), 2 * Time.deltaTime);
        this.transform.Translate(Vector3.left * Time.deltaTime * 5f);
    }

    public bool attackBat(GameObject player)
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

            return true;
        }
        else
        {
            return false;
        }
    }
}
