using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallProjectile : MonoBehaviour
{
    GameObject Necromancer;
    

    // Start is called before the first frame update
    void Start()
    {
        Necromancer = GameObject.Find("Necromancer");
    }

    // Update is called once per frame
    void Update()
    {
        Destroy(this.gameObject, 7f);
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<Player>().health -= Necromancer.GetComponent<NecromancerSc>().dealFireballDamage(Necromancer.GetComponent<NecromancerSc>().minDamage, Necromancer.GetComponent<NecromancerSc>().maxDamage);
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
