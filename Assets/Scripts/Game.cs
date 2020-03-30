using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    bool isGameOver;

    // Start is called before the first frame update
    void Start()
    {
        isGameOver = false;
    }

    public void GameOver()
    {
        isGameOver = true;

        Debug.Log("I died");
        // disable player movement and control prefab here
    }
}
