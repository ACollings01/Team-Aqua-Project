﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShrinkBoomBoom : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale -= new Vector3(0.05f, 0.05f, 0.05f);

        if (transform.localScale.x <= 0)
        {
            Destroy(gameObject);
        }
    }
}
