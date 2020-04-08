using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrownSpearDamage : RangedWeapons
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
            enemy.gameObject.GetComponent<EnemyAI>().health -= spearDamageDone();
            Destroy(this.gameObject);
        }
    }

    public int spearDamageDone()
    {
        int damageThrownSpear = Random.Range(8, 10);
        return damageThrownSpear;
    }
}