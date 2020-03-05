using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrownSpearDamage : MonoBehaviour
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
            enemy.gameObject.GetComponent<EnemyAI>().health -= spearDamageDone();
        }
    }

    public int spearDamageDone()
    {
        int damageThrownSpear = Random.Range(8, 10);
        return damageThrownSpear;
    }
}