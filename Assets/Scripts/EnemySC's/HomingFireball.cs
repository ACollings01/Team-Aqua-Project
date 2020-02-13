using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingFireball : MonoBehaviour
{
    GameObject player;
    int rotSpeed = 2;
    int Speed = 7;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        var direction = player.transform.position - transform.position;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), rotSpeed * Time.deltaTime);
        transform.Translate(0, 0, Time.deltaTime * Speed);
    }
}
