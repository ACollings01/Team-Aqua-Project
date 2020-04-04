using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class Bow : RangedWeapons
{
    private Animator bowAnimator;
    private AudioSource audioSource;

    GameObject arrowDirection;
    GameObject[] arrowProjectiles;

    void Start()
    {
        GameObject bow = GameObject.Find("Player");
        bowAnimator = bow.GetComponent<Animator>();

        audioSource = GetComponent<AudioSource>();

        startTime = 0.0f;

        Player = GameObject.FindGameObjectWithTag("Player");
        arrowDirection = GameObject.FindGameObjectWithTag("Projectile Look At");

        layerMask = LayerMask.GetMask("Player", "Enemy");
        ignoreLayerMask = LayerMask.GetMask("Ignore Tap");
    }

    void Update()
    {
#if UNITY_EDITOR
        if (Input.GetKeyDown("space"))
        {
            bowAnimator.SetTrigger("Quick Tap Bow");

            //audioSource.Play();

            if ((Time.time - lastFireTime) > fireRate)
            {
                lastFireTime = Time.time;

                FireArrow();
            }
        }

        if (Input.GetKeyDown("v"))
        {
            bowAnimator.SetTrigger("Long Tap Bow");

            //audioSource.Play();

            if ((Time.time - lastFireTime) > fireRate)
            {
                lastFireTime = Time.time;

                FireArrow();
            }
        }
#endif
        //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //RaycastHit hit;

        //layerMask = ~layerMask;

        //lengthOfTap();

        //if (Physics.Raycast(ray, out hit, 1000, ignoreLayerMask))
        //{
        //    lookAtClick = lookAtClick;
        //}
        //else if (quickTap == false)
        //{
        //    if (Physics.Raycast(ray, out hit, 1000, layerMask))
        //    {
        //        lookAtClick = new Vector3(hit.point.x, hit.point.y + 1.1f, hit.point.z);
        //    }
        //}

        //if (quickTap && AnimatorIsPlaying())
        //{
        //    bowAnimator.SetBool("Quick Tap Bow", true);

        //    Player.transform.LookAt(lookAtClick);

        //}
        //else if (!AnimatorIsPlaying())
        //{
        //    bowAnimator.SetBool("Quick Tap Bow", false);

        //    if (quickTap)
        //    {
        //        Player.transform.LookAt(lookAtClick);
        //        if ((Time.time - lastFireTime) > fireRate)
        //        {
        //            lastFireTime = Time.time;

        //            FireArrow();
        //        }
        //    }

        //    quickTap = false;
        //}

        arrowProjectiles = GameObject.FindGameObjectsWithTag("Arrow");

        foreach (GameObject arrowProjectile in arrowProjectiles)
        {
            float distance = Vector3.Distance(arrowProjectile.transform.position, Player.transform.position);

            Quaternion arrowRotation = arrowDirection.transform.rotation;

            arrowProjectile.transform.rotation = arrowRotation;

            if (distance > 50)
            {
                Destroy(arrowProjectile);
            }
            else
            {
                Destroy(arrowProjectile, 5.0f);
            }
        }

    }
}