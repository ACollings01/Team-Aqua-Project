using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class RangedWeapons : MonoBehaviour
{

    public float fireRate;
    //public int range;
    //public int damage;
    protected float lastFireTime;
    public GameObject arrowPrefab;
    public Transform launchPosition;
    public int speed;
    public GameObject arrow;


    // Start is called before the first frame update
    void Start()
    {
        lastFireTime = Time.time - 1;

        arrow = Instantiate(arrowPrefab) as GameObject;
    }

    // Update is called once per frame
    void Update()
    {

    }

    protected void Fire()
    {
        Vector3 rotation = new Vector3(launchPosition.position.x, launchPosition.position.y, launchPosition.position.z);

        arrow = Instantiate(arrowPrefab) as GameObject;

        arrow.transform.position = new Vector3(launchPosition.position.x, launchPosition.position.y, launchPosition.position.z + 0.5f);
        arrow.transform.rotation = Quaternion.LookRotation(rotation);

        arrow.GetComponent<Rigidbody>().velocity = transform.parent.forward * speed;


    }



    private void processHit(GameObject hitObject)
    {
        //if (hitObject.GetComponent<Player>() != null)
        //{
        //    hitObject.GetComponent<Player>().TakeDamage(damage);
        //}
        //if (hitObject.GetComponent<Bat>() != null)
        //{
        //    hitObject.GetComponent<Robot>().TakeDamage(damage);
        //}
    }
}
