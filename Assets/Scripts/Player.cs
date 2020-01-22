using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Game game;
    public float health;
    public float armour;
    public int speed;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void TakeDamage(float amount)
    {
        float healthDamage = amount;
        health -= healthDamage * armour;

        if (health <= 0)
        {
            game.GameOver();
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        transform.Translate(movement * speed * Time.deltaTime);
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Projectile")
        {
            Destroy(other.gameObject);
        }
    }
}
