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

    [SerializeField]
    [Range(0, 50)]
    float radius = 5;

    LineRenderer line;

    //https://gamedev.stackexchange.com/questions/126427/draw-circle-around-gameobject-to-indicate-radius <-- Example 1 code used for drawing a circle using Line Renderer

    private void Start()
    {
        line = gameObject.GetComponent<LineRenderer>();

        line.SetVertexCount(50 + 1);
        line.useWorldSpace = false;
        line.SetWidth(0.2f, 0.2f);
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(this.transform.position, player.transform.position);
        //Check for distance between object and player
        if (distance <= radius)
        {
            Debug.Log("Enemies Spawned");
            //Once the player is within distance, spawn enemies
            if(enemiesSpawned == false)
                spawnEnemies();
        }

        Debug.Log(distance);

        //If that bool is true, destroy this object
        if(enemiesSpawned)
        {
            Destroy(gameObject);
        }

        DrawCircleRadius();
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

    void DrawCircleRadius()
    {
        float x;
        float z;

        float angle = 20f;

        for(int i = 0; i < (50 + 1); i++)
        {
            x = Mathf.Sin(Mathf.Deg2Rad * angle) * radius;
            z = Mathf.Cos(Mathf.Deg2Rad * angle) * radius;

            line.SetPosition(i, new Vector3(x, 0, z));

            angle += (360 / 50 + 1);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawCube(transform.position, transform.localScale);
    }
}
