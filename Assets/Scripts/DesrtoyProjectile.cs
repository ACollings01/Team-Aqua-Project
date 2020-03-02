using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesrtoyProjectile : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
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
