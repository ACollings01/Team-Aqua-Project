using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeavyArrowDamage : RangedWeapons
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter(Collider enemy)
    {
        if (enemy.gameObject.tag == "Enemy")
        {
            enemy.gameObject.GetComponent<EnemyAI>().health -= bowDamageDone();
        }
    }

    public int bowDamageDone()
    {
        int damageBow = Random.Range(200, 500);
        return damageBow;
    }
}
