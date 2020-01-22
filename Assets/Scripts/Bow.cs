using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : RangedWeapons
{
    void Update()
    {
        if(Input.GetMouseButtonDown(0) && (Time.time - lastFireTime) > fireRate)
        {
            lastFireTime = Time.time;
            Fire();
        }
    }
}
