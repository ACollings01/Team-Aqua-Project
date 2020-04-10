using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyHeavyProjectile : RangedWeapons
{

    void Start()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        layerMask = LayerMask.GetMask("Player", "Enemy");
        ignoreLayerMask = LayerMask.GetMask("Ignore Tap");
#endif
    }

    void Update()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        layerMask = ~layerMask;

        if (Physics.Raycast(ray, out hit, 1000, ignoreLayerMask))
        {
            lookAtClickProjectile = lookAtClickProjectile;
        }
        else if (quickTap == false)
        {
            if (Physics.Raycast(ray, out hit, 1000, layerMask))
            {
                lookAtClickProjectile = new Vector3(hit.point.x, hit.point.y + 1.1f, hit.point.z);
            }
        }

        if (!checkOnce)
        {
            LookAtClickProjectile();
            checkOnce = true;
        }
#endif
    }

    void LookAtClickProjectile()
    {
        transform.LookAt(lookAtClickProjectile);
        checkOnce = false;
    }

    void OnCollisionEnter(Collision enemy)
    {
        if (enemy.gameObject.tag == "Enemy")
        {
            Destroy(this.gameObject);
        }
    }
}
