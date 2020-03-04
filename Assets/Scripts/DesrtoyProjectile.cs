﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesrtoyProjectile : RangedWeapons
{
    // Start is called before the first frame update
    void Start()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 1000))
        {
            lookAtClickProjectile = new Vector3(hit.point.x, hit.point.y + 1, hit.point.z);
        }
        transform.LookAt(lookAtClickProjectile);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision enemy)
    {
        if (enemy.gameObject.tag == "Enemy")
        {
            Destroy(this.gameObject);
        }
    }
}
