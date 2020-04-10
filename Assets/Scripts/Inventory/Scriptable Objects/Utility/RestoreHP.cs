using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestoreHP : MonoBehaviour
{
    public GameObject player;
    public GameObject inv;

    public void RestoreHealth()
    {
        if(inv.GetComponent<DisplayInventory>().inventory.Container[1].amount > 0)
        {
            player.GetComponent<Player>().health += 25;

            if (player.GetComponent<Player>().health > 50)
            {
                player.GetComponent<Player>().health = 50;
                Debug.Log(player.GetComponent<Player>().health);
            }

            if (inv.GetComponent<DisplayInventory>().inventory.Container[1].amount > 0)
            {
                inv.GetComponent<DisplayInventory>().inventory.Container[1].AddAmount(-1);
            }
        }

    }

    public void Update()
    {

    }
}
