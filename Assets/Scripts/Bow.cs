using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class Bow : RangedWeapons
{
    Collider bowCollider;

    private int damageBow;

    void Start()
    {
        bowCollider = GetComponent<Collider>();
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0) && (Time.time - lastFireTime) > fireRate)
        {
            lastFireTime = Time.time;
            
            Fire();
        }
    }

    void OnCollisionEnter(Collision enemy)
    {
        if (enemy.gameObject.tag == "Bandit")
        {
            bowDamageDone();
        }
    }

    public int bowDamageDone()
    {
        damageBow = Random.Range(12, 15);
        return damageBow;
    }
}
