using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class Bow : RangedWeapons
{
    private Animator bowAnimator;

    GameObject[] arrowProjectiles;

    bool AnimatorIsPlaying()
    {
        return bowAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1;
    }

    void Start()
    {
        GameObject bow = transform.gameObject;
        bowAnimator = bow.GetComponent<Animator>();

        startTime = 0.0f;

        Player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        int layerMask = 1 << 9;
        layerMask = ~layerMask;

        lengthOfTap();

        if (quickTap && AnimatorIsPlaying())
        {
            bowAnimator.SetBool("Quick Tap Bow", true);

            if (Physics.Raycast(ray, out hit, 1000, layerMask))
            {
                lookAtClick = new Vector3(hit.point.x, hit.point.y + 1.1f, hit.point.z);
            }

            Player.transform.LookAt(lookAtClick);

        }
        else if (!AnimatorIsPlaying())
        {
            bowAnimator.SetBool("Quick Tap Bow", false);

            if (quickTap)
            {
                Player.transform.LookAt(lookAtClick);
                if ((Time.time - lastFireTime) > fireRate)
                {
                    lastFireTime = Time.time;

                    FireArrow();
                }
            }

            quickTap = false;
        }

        arrowProjectiles = GameObject.FindGameObjectsWithTag("Arrow");

        foreach (GameObject arrowProjectile in arrowProjectiles)
        {
            float distance = Vector3.Distance(arrowProjectile.transform.position, Player.transform.position);

            if (distance > 50)
            {
                Destroy(arrowProjectile);
            }

            /*if (Time.time > startTime + 5.0f && startTime != 0.0f)
            {
                Destroy(arrowProjectile);
            }*/
        }

    }
}
