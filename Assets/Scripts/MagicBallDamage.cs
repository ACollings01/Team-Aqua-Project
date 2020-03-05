using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicBallDamage : MonoBehaviour
{
    Vector3 Target;
    GameObject staff;

    public float explosionRadius;

    public GameObject boomBoomRadius;

    public LayerMask whatIsEnemy;

    // Start is called before the first frame update
    void Start()
    {
        staff = GameObject.Find("Staff");

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        int layerMask = 1 << 9;
        layerMask = ~layerMask;

        if (Physics.Raycast(ray, out hit, 1000, layerMask))
        {
            Target = new Vector3(hit.point.x, 0, hit.point.z);
        }
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(this.transform.position, staff.transform.position);

        if (distance >= 50)
        {
            explode();
        }
    }

    void OnCollisionEnter(Collision enemy)
    {
        if (enemy.gameObject.tag == "Enemy")
        {
            explode();
        }
    }

    public int magicDamageDone()
    {
        int damageMagic = Random.Range(10, 16);
        return damageMagic;
    }

    void explode()
    {
        Instantiate(boomBoomRadius, transform.position, Quaternion.identity);

        Collider[] enemyHit = Physics.OverlapSphere(this.transform.position, explosionRadius, whatIsEnemy);

        Debug.Log(enemyHit.Length);

        for (int i = 0; i < enemyHit.Length; i++)
        {
            enemyHit[i].GetComponent<EnemyAI>().health -= magicDamageDone();
        }

        Destroy(gameObject);
    }
}
