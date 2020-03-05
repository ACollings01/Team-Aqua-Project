using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    //tutorial I used to help https://www.youtube.com/watch?v=zc8ac_qUXQY

    public void StartGame()
    {
        SceneManager.LoadScene("Prototype Level");
    }
    
    public void TownScene()
    {
        SceneManager.LoadScene("TownScene");
    }

    public void ShopUI()
    {
        SceneManager.LoadScene("ShopUI");
    }
}
