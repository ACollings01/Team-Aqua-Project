using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class RangedWeapons : Weapons
{
    public GameObject arrowPrefab;
    public GameObject magicPrefab;
    public GameObject spearPrefab;
    public GameObject fireAtPrefab;
    public Transform arrowLaunchPosition;
    public Transform magicLaunchPosition;
    public Transform spearLaunchPosition;

    private GameObject arrow;
    private GameObject magic;
    private GameObject spear;

    public int speed;
    public float fireRate;

    protected float lastFireTime;
    protected GameObject fireAt;


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
        //Vector3 rotation = new Vector3(arrowLaunchPosition.position.x, arrowLaunchPosition.position.y, arrowLaunchPosition.position.z);

        arrow = Instantiate(arrowPrefab, transform.position ,Quaternion.identity);
        arrow.tag = "Arrow";

        arrow.transform.position = new Vector3(arrowLaunchPosition.position.x - 0.1f, arrowLaunchPosition.position.y, arrowLaunchPosition.position.z);
        //arrow.transform.rotation = Quaternion.LookRotation(rotation);

        arrow.GetComponent<Rigidbody>().velocity = transform.parent.forward * speed;

    }

    protected void FireMagic()
    {
        //Vector3 rotation = new Vector3(magicLaunchPosition.position.x, magicLaunchPosition.position.y, magicLaunchPosition.position.z);

        magic = Instantiate(magicPrefab, transform.position, Quaternion.identity);
        magic.tag = "Magic";

        magic.transform.position = new Vector3(magicLaunchPosition.position.x - 0.1f, magicLaunchPosition.position.y, magicLaunchPosition.position.z);
        //magic.transform.rotation = Quaternion.LookRotation(rotation);

        magic.GetComponent<Rigidbody>().velocity = transform.parent.forward * speed;

    }

    protected void ThrowSpear()
    {
        //Vector3 rotation = new Vector3(spearLaunchPosition.rotation.x, spearLaunchPosition.rotation.y, spearLaunchPosition.rotation.z);

        spear = Instantiate(spearPrefab, transform.position, Quaternion.identity);
        spear.tag = "Thrown Spear";

        //spear.transform.position = new Vector3(spearLaunchPosition.position.x, spearLaunchPosition.position.y, spearLaunchPosition.position.z);
        //spear.transform.rotation = Quaternion.LookRotation(rotation);

        spear.GetComponent<Rigidbody>().velocity = transform.parent.forward * speed;

    }
}
