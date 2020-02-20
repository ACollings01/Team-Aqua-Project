using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class RangedWeapons : Weapons
{
    public GameObject arrowPrefab;
    public GameObject magicPrefab;
    public Transform arrowLaunchPosition;
    public Transform magicLaunchPosition;
    public GameObject arrow;
    public GameObject magic;
    public GameObject Player;

    public int speed;
    public float fireRate;

    protected float lastFireTime;


    // Start is called before the first frame update
    void Start()
    {
        lastFireTime = Time.time - 1;
    }

    // Update is called once per frame
    void Update()
    {
       

    }


    protected void FireArrow()
    {
        Vector3 rotation = new Vector3(arrowLaunchPosition.position.x, arrowLaunchPosition.position.y, arrowLaunchPosition.position.z);

        arrow = Instantiate(arrowPrefab, transform.position ,Quaternion.identity);
        arrow.tag = "Arrow";

        arrow.transform.position = new Vector3(arrowLaunchPosition.position.x, arrowLaunchPosition.position.y, arrowLaunchPosition.position.z + 0.5f);
        arrow.transform.rotation = Quaternion.LookRotation(rotation);

        arrow.GetComponent<Rigidbody>().velocity = transform.parent.forward * speed;


    }

    protected void FireMagic()
    {
        Vector3 rotation = new Vector3(magicLaunchPosition.position.x, magicLaunchPosition.position.y, magicLaunchPosition.position.z);

        magic = Instantiate(magicPrefab, transform.position, Quaternion.identity);
        magic.tag = "Magic";

        magic.transform.position = new Vector3(magicLaunchPosition.position.x, magicLaunchPosition.position.y, magicLaunchPosition.position.z + 0.5f);
        magic.transform.rotation = Quaternion.LookRotation(rotation);

        magic.GetComponent<Rigidbody>().velocity = transform.parent.forward * speed;


    }
}
