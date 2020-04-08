using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class Bow : RangedWeapons
{
    private Animator bowAnimator;
    private AudioSource audioSource;
    private Quaternion lastHeavyArrowRotation;

    [SerializeField]
    private AudioClip lightAttackSound;

    [SerializeField]
    private AudioClip heavyAttackSound;

    GameObject arrowDirection;
    GameObject[] arrowProjectiles;
    GameObject[] heavyArrowProjectiles;

    private Quaternion arrowRotation;

    public ParticleSystem arrowParticle;

    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        bowAnimator = Player.GetComponent<Animator>();

        audioSource = GetComponent<AudioSource>();

        startTime = 0.0f;

        arrowDirection = GameObject.FindGameObjectWithTag("Projectile Look At");

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
            if (!heavyAttack && !lightAttack)
            {
                lightAttack = true;

                fireRate = 0.4f;
                projectileSpeed = 80;

                if ((Time.time - lastFireTime) > fireRate)
                {
                    lastFireTime = Time.time;

                    bowAnimator.SetTrigger("Quick Tap Bow");

                    audioSource.PlayOneShot(lightAttackSound);
                    FireArrow();
                    arrowDirectionOnce = false;
                    lightAttack = false;
                }
                else
                {
                    lightAttack = false;
                }
            }           
        }

        if (Input.GetKeyDown("v"))
        {
            if (!heavyAttack && !lightAttack)
            {
                heavyAttack = true;
                //audioSource.Play();
                fireRate = 5f;
                projectileSpeed = 35;

                if ((Time.time - lastFireTimeHeavy) > fireRate)
                {
                    lastFireTimeHeavy = Time.time;

                    bowAnimator.SetTrigger("Long Tap Bow");
                    Player.GetComponent<Player>().stopMoving = true;

                    StartCoroutine(WaitToFireArrow());

                    arrowDirectionOnce = false;
                }
                else
                {
                    heavyAttack = false;
                }
            }  
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
            projectileSpeed = 80;

            if ((Time.time - lastFireTime) > fireRate)
            {
                lastFireTime = Time.time;

                bowAnimator.SetTrigger("Quick Tap Bow");

                Player.transform.LookAt(lookAtClick);
                arrowRotation = arrowDirection.transform.rotation;

                audioSource.PlayOneShot(lightAttackSound);
                FireArrow();
                arrowDirectionOnce = false;
            }
            quickTap = false;
        }

        if (longTap)
        {
            fireRate = 5f;
            projectileSpeed = 35;

            if ((Time.time - lastFireTimeHeavy) > fireRate)
            {
                lastFireTimeHeavy = Time.time;

                bowAnimator.SetTrigger("Long Tap Bow");
                Player.GetComponent<Player>().stopMoving = true;

                StartCoroutine(WaitToFireArrow());

                arrowDirectionOnce = false;
            }
            longTap = false;
        }
#endif

        //if (quickTap)
        //{
        //    bowAnimator.SetTrigger("Quick Tap Bow");
        //    Player.transform.LookAt(lookAtClick);
        //    if ((Time.time - lastFireTime) > fireRate)
        //    {
        //        lastFireTime = Time.time;

        //        audioSource.PlayOneShot(lightAttackSound);
        //        FireArrow();
        //    }
        //    quickTap = false;
        //}



        arrowProjectiles = GameObject.FindGameObjectsWithTag("Arrow");

        foreach (GameObject arrowProjectile in arrowProjectiles)
        {
            float distance = Vector3.Distance(arrowProjectile.transform.position, Player.transform.position);

            Quaternion arrowRotation = arrowDirection.transform.rotation;

            if (!arrowDirectionOnce)
            {
                arrowProjectile.transform.rotation = arrowRotation;
                arrowDirectionOnce = true;
            }

            if (distance > 30)
            {
                Destroy(arrowProjectile);
            }
            else
            {
                Destroy(arrowProjectile, 5.0f);
            }
        }

        heavyArrowProjectiles = GameObject.FindGameObjectsWithTag("Heavy Arrow");

        foreach (GameObject heavyArrowProjectile in heavyArrowProjectiles)
        {
            float distance = Vector3.Distance(heavyArrowProjectile.transform.position, Player.transform.position);          

            if (!arrowDirectionOnce)
            {
                heavyArrowProjectile.transform.rotation = lastHeavyArrowRotation;
                arrowDirectionOnce = true;
            }

            var arrowSystem = Instantiate(arrowParticle, new Vector3(heavyArrowProjectile.transform.position.x, heavyArrowProjectile.transform.position.y, heavyArrowProjectile.transform.position.z), Quaternion.identity);
            arrowSystem.transform.rotation = lastHeavyArrowRotation;

            if (distance > 30)
            {
                Destroy(heavyArrowProjectile);
                Destroy(arrowSystem.gameObject);
            }
            else
            {
                Destroy(heavyArrowProjectile, 5.0f);
                Destroy(arrowSystem.gameObject, 1.0f);
            }
        }

    }

    IEnumerator WaitToFireArrow()
    {
        yield return new WaitForSeconds(1);
#if UNITY_ANDROID && !UNITY_EDITOR
        Player.transform.LookAt(lookAtClick);
#endif
        lastHeavyArrowRotation = arrowDirection.transform.rotation;
        audioSource.PlayOneShot(heavyAttackSound);
        FireHeavyArrow();
        Player.GetComponent<Player>().stopMoving = false;
        heavyAttack = false;
    }
}