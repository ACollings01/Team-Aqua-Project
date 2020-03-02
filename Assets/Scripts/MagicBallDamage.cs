using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicBallDamage : MonoBehaviour
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
            enemy.gameObject.GetComponent<EnemyAI>().health -= magicDamageDone();
        }
    }

    public int magicDamageDone()
    {
        int damageMagic = Random.Range(10, 16);
        return damageMagic;
    }
}
