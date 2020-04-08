using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class Weapons : Player
{
    protected bool quickTap = false;
    protected bool longTap = false;
    protected bool lightAttack = false;
    protected bool heavyAttack = false;
    protected float startTime;
    protected int damage;
    protected int layerMask;
    protected int ignoreLayerMask;
    protected bool attackOnce = false;
    protected bool checkOnce = false;
    protected bool arrowDirectionOnce = false;
    protected bool spearDirectionOnce = false;
    protected Vector3 lookAtClick;
    protected Vector3 lookAtClickProjectile;
    protected GameObject Player;
    public float fireRate;
    protected float lastFireTime;
    protected float lastFireTimeHeavy;

    [SerializeField]
    protected GameObject SwordBlade;

    [SerializeField]
    protected GameObject playerSpear;

    [SerializeField]
    protected GameObject playerShield;

    protected LayerMask whatIsEnemy;
    float swordAttackRadius;
    float spearAttackRadius;
    float shieldAttackRadius;

   

    public void Start()
    {

    }

    void Update()
    {
        
    }

    protected void lengthOfTap()
    {

        if (Input.touchCount > 0)
        {
            foreach (Touch touch in Input.touches)
            {
                if (UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject(0))
                {               
                    // do nothing
                }

                if (!UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject(0) || (UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject(0) && Input.touchCount > 1))
                {
                    if (touch.phase == TouchPhase.Began)
                    {
                        startTime = Time.time;
                    }

                    if (touch.phase == TouchPhase.Ended)
                    {
                        if (Time.time > startTime + 0.5f && startTime != 0.0f)
                        {
                            longTap = true;
                            quickTap = false;
                        }
                        else if (Time.time < startTime + 0.5f && startTime != 0.0f)
                        {
                            quickTap = true;
                            longTap = false;
                        }

                        startTime = 0;
                    }
                }
            }
        }
    }

    protected void swordAttack()
    {
        whatIsEnemy = LayerMask.GetMask("Enemy");
        swordAttackRadius = 1;

       Collider[] enemyHit = Physics.OverlapSphere(SwordBlade.transform.position, swordAttackRadius, whatIsEnemy);

       for (int i = 0; i < enemyHit.Length; i++)
       {
            enemyHit[i].GetComponent<EnemyAI>().health -= swordDamageDone();
            attackOnce = true;
       }
    }

    protected void spearAttack()
    {
        whatIsEnemy = LayerMask.GetMask("Enemy");
        spearAttackRadius = 1;

        Collider[] enemyHit = Physics.OverlapSphere(playerSpear.transform.position, spearAttackRadius, whatIsEnemy);

        for (int i = 0; i < enemyHit.Length; i++)
        {
            enemyHit[i].GetComponent<EnemyAI>().health -= spearDamageDone();
            attackOnce = true;
        }
    }

    protected void shieldAttack()
    {
        whatIsEnemy = LayerMask.GetMask("Enemy");
        shieldAttackRadius = 1;

        Collider[] enemyHit = Physics.OverlapSphere(playerShield.transform.position, shieldAttackRadius, whatIsEnemy);

        for (int i = 0; i < enemyHit.Length; i++)
        {
            enemyHit[i].GetComponent<EnemyAI>().health -= shieldDamageDone();
            attackOnce = true;
        }
    }

    public int swordDamageDone()
    {
        damage = Random.Range(8, 11);
        return damage;
    }

    public int spearDamageDone()
    {
        damage = Random.Range(8, 11);
        return damage;
    }

    public int shieldDamageDone()
    {
        damage = Random.Range(8, 11);
        return damage;
    }

}
