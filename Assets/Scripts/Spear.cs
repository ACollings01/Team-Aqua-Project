using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spear : RangedWeapons
{
    private Animator spearAnimator;
    private AudioSource audioSource;

    [SerializeField]
    private AudioClip lightAttackSound;

    [SerializeField]
    private AudioClip heavyAttackSound;

    GameObject spearDirection;
    GameObject[] spearProjectiles;

    private Quaternion spearRotation;

    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        spearAnimator = Player.GetComponent<Animator>();

        audioSource = GetComponent<AudioSource>();

        startTime = 0.0f;

        spearDirection = GameObject.FindGameObjectWithTag("Projectile Look At");

        lastFireTime = Time.time - 10;
        lastFireTimeHeavy = Time.time - 10;

#if UNITY_ANDROID && !UNITY_EDITOR
        layerMask = LayerMask.GetMask("Player", "Enemy");
        ignoreLayerMask = LayerMask.GetMask("Ignore Tap");
#endif
    }

    void Update()
    {
#if UNITY_EDITOR
        if (Input.GetKeyDown("space"))
        {
            fireRate = 0.4f;
            if ((Time.time - lastFireTime) > fireRate)
            {
                lastFireTime = Time.time;
                spearAnimator.SetTrigger("Quick Tap Spear");

                if (attackOnce == false)
                {
                    audioSource.PlayOneShot(lightAttackSound);
                    spearAttack();
                }
                quickTap = false;
            }
        }

        if (Input.GetKeyDown("v"))
        {
            fireRate = 5f;
            if ((Time.time - lastFireTimeHeavy) > fireRate)
            {
                lastFireTimeHeavy = Time.time;

                spearAnimator.SetTrigger("Long Tap Spear");
                Player.GetComponent<Player>().stopMoving = true;
                spearRotation = spearDirection.transform.rotation;

                StartCoroutine(WaitToFireSpear());

                spearDirectionOnce = false;
            }
        }

        if (!quickTap)
        {
            attackOnce = false;
        }
#endif

#if UNITY_ANDROID && !UNITY_EDITOR
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        layerMask = ~layerMask;

        lengthOfTap();

        if (Physics.Raycast(ray, out hit, 1000, ignoreLayerMask))
        {
            lookAtClick = lookAtClick;
        }
        else if (quickTap == false)
        {
            if (Physics.Raycast(ray, out hit, 1000, layerMask))
            {
                lookAtClick = new Vector3(hit.point.x, hit.point.y + 1.1f, hit.point.z);
            }
        }

        if (quickTap)
        {
            fireRate = 0.4f;
            if ((Time.time - lastFireTime) > fireRate)
            {
                lastFireTime = Time.time;
                spearAnimator.SetTrigger("Quick Tap Spear");

                if (attackOnce == false)
                {
                    audioSource.PlayOneShot(lightAttackSound);
                    spearAttack();
                }
            }
            quickTap = false;
        }

        if (longTap)
        {
            fireRate = 5f;
            if ((Time.time - lastFireTimeHeavy) > fireRate)
            {
                lastFireTimeHeavy = Time.time;

                spearAnimator.SetTrigger("Long Tap Spear");
                Player.GetComponent<Player>().stopMoving = true;

                Player.transform.LookAt(lookAtClick);
                spearRotation = spearDirection.transform.rotation;

                StartCoroutine(WaitToFireSpear());

                spearDirectionOnce = false;
            }

            longTap = false;
        }

        if (!quickTap)
        {
            attackOnce = false;
        }
#endif

        spearProjectiles = GameObject.FindGameObjectsWithTag("Thrown Spear");

        foreach (GameObject spearProjectile in spearProjectiles)
        {
            float distance = Vector3.Distance(spearProjectile.transform.position, Player.transform.position);

            Quaternion spearRotation = spearDirection.transform.rotation;

            if (!spearDirectionOnce)
            {
                spearProjectile.transform.rotation = spearRotation;
                spearDirectionOnce = true;
            }

            if (distance > 30)
            {
                Destroy(spearProjectile);
            }
            else
            {
                Destroy(spearProjectile, 5.0f);
            }
        }

    }

    IEnumerator WaitToFireSpear()
    {
        yield return new WaitForSeconds(.1f);
        audioSource.PlayOneShot(heavyAttackSound);
        ThrowSpear();
        Player.GetComponent<Player>().stopMoving = false;
    }
}