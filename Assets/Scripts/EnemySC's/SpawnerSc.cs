using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerSc : MonoBehaviour
{
    //Hold a Gameobject
    public GameObject enemyToSpawn;
    public GameObject player;
    //Hold a number for the amount of enemies to spawn
    public int numberOfEnemies;

    bool enemiesSpawned = false;

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(this.transform.position, player.transform.position);
        //Check for distance between object and player
        if (distance <= 20)
        {
            //Once the player is within distance, spawn enemies
            if(enemiesSpawned == false)
                spawnEnemies();
        }

        //If that bool is true, destroy this object
        if(enemiesSpawned)
        {
            Destroy(gameObject);
        }
    }

    void spawnEnemies()
    {
        //Spawn all enemies that need to be spawned
        for (int i = 0; i < numberOfEnemies; i++)
        {
            Vector3 distance = new Vector3(transform.position.x + Random.Range(-4f, 4f), -2, transform.position.z + Random.Range(-4f, 4f));

            GameObject enemy = Instantiate(enemyToSpawn, distance, Quaternion.identity);
        }
        //Once all enemies have been spawned, set a bool to true
        enemiesSpawned = true;
    }
}
