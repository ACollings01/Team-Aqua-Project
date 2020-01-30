using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitcher : MonoBehaviour
{
    public static string activeWeaponType;

    public GameObject Sword;
    public GameObject Bow;
    public GameObject Staff;

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
    }

    private void loadWeapon(GameObject weapon)
    {
        Sword.SetActive(false);
        Bow.SetActive(false);
        Staff.SetActive(false);
        weapon.SetActive(true);
        activeWeapon = weapon;
    }
}
