using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class Staff : RangedWeapons
{
    private Animator staffAnimator;

    private AudioSource audioSource;
    GameObject[] staffProjectiles;

    bool AnimatorIsPlaying()
    {
        return staffAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1;
    }

    void Start()
    {
        GameObject bow = transform.gameObject;
        staffAnimator = bow.GetComponent<Animator>();

        audioSource = GetComponent<AudioSource>();

        startTime = 0.0f;

        Player = GameObject.FindGameObjectWithTag("Player");
        layerMask = LayerMask.GetMask("Player", "Enemy");
        ignoreLayerMask = LayerMask.GetMask("Ignore Tap");
    }

    void Update()
    {
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

        if (quickTap && AnimatorIsPlaying())
        {
            staffAnimator.SetBool("Quick Tap Staff", true);

            Player.transform.LookAt(lookAtClick);

        }
        else if (!AnimatorIsPlaying())
        {
            staffAnimator.SetBool("Quick Tap Staff", false);

            if (quickTap)
            {
                Player.transform.LookAt(lookAtClick);
                if ((Time.time - lastFireTime) > fireRate)
                {
                    lastFireTime = Time.time;

                    FireMagic();
                }
            }

            quickTap = false;
        }

        staffProjectiles = GameObject.FindGameObjectsWithTag("Staff");

        foreach (GameObject staffProjectile in staffProjectiles)
        {
            float distance = Vector3.Distance(staffProjectile.transform.position, Player.transform.position);

            if (Physics.Raycast(ray, out hit, 1000))
            {
                lookAtClickProjectile = new Vector3(hit.point.x, hit.point.y + 1, hit.point.z);
            }

            if (!AnimatorIsPlaying())
            {
                staffProjectile.transform.LookAt(lookAtClickProjectile);
            }

            if (distance > 50)
            {
                Destroy(staffProjectile);
            }

            /*if (Time.time > startTime + 5.0f && startTime != 0.0f)
            {
                Destroy(staffProjectile);
            }*/
        }

    }
}
