using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowDamage : RangedWeapons
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnCollisionEnter(Collision enemy)
    {
        if (enemy.gameObject.tag == "Enemy")
        {
            enemy.gameObject.GetComponent<EnemyAI>().health -= bowDamageDone();
        }
    }

    public int bowDamageDone()
    {
        int damageBow = Random.Range(4, 8);
        return damageBow;
    }
}
