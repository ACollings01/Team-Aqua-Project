using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class RangedWeapons : MonoBehaviour
{

    public float fireRate;
    public int range;
    public int damage;
    protected float lastFireTime;
    public GameObject arrowPrefab;
    public Transform launchPosition;

    // Start is called before the first frame update
    void Start()
    {
        lastFireTime = Time.time - 2;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected void Fire()
    {

        GameObject arrow = Instantiate(arrowPrefab) as GameObject;

        arrow.transform.position = launchPosition.position;

        arrow.GetComponent<Rigidbody>().velocity = transform.parent.forward * 20;

        //Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        //RaycastHit hit;
        //if (Physics.Raycast(ray, out hit, range))
        //{
        //    processHit(hit.collider.gameObject);
        //}
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
