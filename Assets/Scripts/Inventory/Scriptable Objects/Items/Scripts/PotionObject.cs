using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Potion Object", menuName = "Inventory System/Items/Potions")]

public class PotionObject : ItemObject
{
    public int restoreHealthValue;
    public float armorClassValue;
    public void Awake()
    {
        type = ItemType.Potions;
    }
}