﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class Weapons : MonoBehaviour
{
    protected bool quickTap = false;
    protected bool longTap = false;
    protected float startTime;
    protected int damage;

    LayerMask whatIsEnemy;
    float swordAttackRadius;
    float spearAttackRadius;
    float shieldAttackRadius;

    void Start()
    {
        whatIsEnemy = LayerMask.GetMask("Enemy");
        swordAttackRadius = 1;
    }

    void Update()
    {
        
    }

    protected void lengthOfTap()
    {
        if (UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
        {
            //Debug.Log("Tooch");
        }
        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                startTime = Time.time;
            }

            if (Input.GetMouseButtonUp(0))
            {
                if (Time.time > startTime + 1.0f && startTime != 0.0f)
                {
                    longTap = true;
                    quickTap = false;
                }
                else if (Time.time < startTime + 1.0f && startTime != 0.0f)
                {
                    quickTap = true;
                    longTap = false;
                }

                startTime = 0;
            }
        }
    }

    protected void swordAttack()
    {
       GameObject playerSword = GameObject.Find("Player/Player_Model/Sword");

       Collider[] enemyHit = Physics.OverlapSphere(playerSword.transform.position, swordAttackRadius, whatIsEnemy);

       for (int i = 0; i < enemyHit.Length; i++)
       {
           enemyHit[i].GetComponent<EnemyAI>().health -= swordDamageDone();
           Debug.Log("The player has hit the enemy!");
       }
    }

    public int swordDamageDone()
    {
        damage = Random.Range(8, 11);
        return damage;
    }
}
