using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyScript : MonoBehaviour
{
    public AudioClip purchase;
    public GameObject inv;

    public void buyItem()
    {
        if (inv.GetComponent<DisplayInventory>().inventory.Container[0].amount >= 50)
        {
            inv.GetComponent<DisplayInventory>().inventory.Container[0].AddAmount(-50);
            inv.GetComponent<DisplayInventory>().inventory.Container[1].AddAmount(1);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
