using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdjustForce : MonoBehaviour
{
    //Referenced this post for the forces https://forum.unity.com/threads/ignore-force-from-certain-rigidbodies.505973/

    Rigidbody rb;
    Vector3 position, velocity, angularVelocity;
    bool isColliding;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isColliding)
        {
            position = rb.position;
            velocity = rb.velocity;
            angularVelocity = rb.angularVelocity;
        }
    }

    void LateUpdate()
    {
        if (isColliding)
        {
            rb.position = position;
            rb.velocity = velocity;
            rb.angularVelocity = angularVelocity;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "Player")
        {
            isColliding = true;
        }

        if(other.gameObject.tag == "Enemy")
        {
            isColliding = true;
        }

        if(other.gameObject.tag == "Projectile")
        {
            isColliding = true;
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            isColliding = false;
        }

        if (other.gameObject.tag == "Enemy")
        {
            isColliding = false;
        }

        if (other.gameObject.tag == "Projectile")
        {
            isColliding = false;
        }
    }

}
