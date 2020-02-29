using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class Bow : RangedWeapons
{
    private Animator bowAnimator;

    Vector3 lookAtClick;

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

        if (UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
        {
            //Debug.Log("Touch");
        }
        else
        {
            lengthOfTap();
        }

        if (quickTap && AnimatorIsPlaying())
        {
            bowAnimator.SetBool("Quick Tap Bow", true);

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
        }

    }
}
