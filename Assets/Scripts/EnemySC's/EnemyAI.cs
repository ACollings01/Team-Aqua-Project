﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    protected Animator anim;
    protected GameObject player;

    public float speed;
    public int health;

    public int minDamage;
    public int maxDamage;
    private AudioSource audioSource;

    public LayerMask whatIsPlayer;
    public float attackRadius;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");

    }

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

       //////////////////////////////////////////////////////////////////////////////////////////////////////

        //void OnDrawGizmosSelected()
        //{
        //    GameObject slimeFace = GameObject.Find("Bandit/BanditWeapon");
        //    Gizmos.color = Color.red;
        //    Gizmos.DrawWireSphere(slimeFace.transform.position, attackRadius);
        //}


        public int dealDamageToPlayer(int min, int max)
    {
        int damage = Random.Range(min, max + 1);
        return damage;
    }
}
