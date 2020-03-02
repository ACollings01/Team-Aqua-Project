using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitcher : MonoBehaviour
{
    public static string activeWeaponType;

    public GameObject Sword;
    public GameObject Bow;
    public GameObject Staff;
    public GameObject Spear;
    public GameObject Shield;

    GameObject activeWeapon;

    public GameObject GetActiveWeapon()
    {
        return activeWeapon;
    }

    void Start()
    {
        activeWeaponType = "Sword";
        activeWeapon = Sword;
    }

    void Update()
    {
        if (Input.GetKeyDown("1"))
        {
            loadWeapon(Sword);
            activeWeaponType = "Sword";
        }
        else if (Input.GetKeyDown("2"))
        {
            loadWeapon(Bow);
            activeWeaponType = "Bow";
        }
        else if (Input.GetKeyDown("3"))
        {
            loadWeapon(Staff);
            activeWeaponType = "Staff";
        }
        else if (Input.GetKeyDown("4"))
        {
            loadWeapon(Spear);
            activeWeaponType = "Spear";
        }
        else if (Input.GetKeyDown("5"))
        {
            loadWeapon(Shield);
            activeWeaponType = "Shield";
        }
    }

    private void loadWeapon(GameObject weapon)
    {
        Sword.SetActive(false);
        Bow.SetActive(false);
        Staff.SetActive(false);
        Spear.SetActive(false);
        Shield.SetActive(false);
        weapon.SetActive(true);
        activeWeapon = weapon;
    }
}
