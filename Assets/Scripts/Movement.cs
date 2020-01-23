using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    public Joystick joystick;
    public float speed = 3.0f;

    private float startTime;

    // Start is called before the first frame update
    void Start()
    {
        joystick = FindObjectOfType<Joystick>();

        startTime = 0.0f;

    }

    // Update is called once per frame
    void Update()
    {
        var rigidbody = GetComponent<Rigidbody>();

        if (Input.GetMouseButtonDown(0))
        {
            startTime = Time.time;
        }

        if (Input.GetMouseButtonUp(0))
        {
            startTime = 0.0f;
           // Debug.Log("startTime" + startTime);
        }

        if (Time.time > startTime + 0.25f && startTime != 0.0f)
        {
            //Debug.Log("startTime" + startTime);
            rigidbody.velocity = new Vector3(joystick.Horizontal * speed, rigidbody.velocity.y, joystick.Vertical * speed);

        }
        else
        {
            rigidbody.velocity = new Vector3(0,0,0);
        }

        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        transform.Translate(movement * speed * Time.deltaTime);
    }
}
