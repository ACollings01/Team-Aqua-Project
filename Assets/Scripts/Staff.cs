using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class Staff : RangedWeapons
{
    private Animator staffAnimator;
    private int damageStaff;

    GameObject[] magicProjectiles;

    bool AnimatorIsPlaying()
    {
        return staffAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1;
    }

    // Start is called before the first frame update
    void Start()
    {
        GameObject staff = transform.gameObject;
        staffAnimator = staff.GetComponent<Animator>();

        startTime = 0.0f;

        Player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        lengthOfTap();

        if (quickTap && AnimatorIsPlaying())
        {
            staffAnimator.SetBool("Quick Tap Staff", true);

            if (Physics.Raycast(ray, out hit, 1000))
            {
                lookAtClick = hit.point;
            }

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

        //for PC controls
        if (Input.GetKeyDown(KeyCode.Space))
        {
            startTime = Time.time;
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            startTime = 0.0f;
        }

        magicProjectiles = GameObject.FindGameObjectsWithTag("Magic");

        foreach (GameObject arrowProjectile in magicProjectiles)
        {
            float distance = Vector3.Distance(arrowProjectile.transform.position, Player.transform.position);

            if (distance > 20)
            {
                Destroy(arrowProjectile);
            }
        }

    }

    void OnCollisionEnter(Collision enemy)
    {
        if (enemy.gameObject.tag == "Enemy")
        {
            enemy.gameObject.GetComponent<EnemyAI>().health -= staffDamageDone();
        }
    }

    public int staffDamageDone()
    {
        damageStaff = Random.Range(12, 16);
        return damageStaff;
    }
}
