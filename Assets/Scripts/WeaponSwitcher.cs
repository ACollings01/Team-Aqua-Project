using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitcher : MonoBehaviour
{
    public static string activeWeaponType;

    public GameObject Sword;
    public GameObject Bow;
    //public GameObject Staff;
    public GameObject Spear;
    public GameObject Shield;

    GameObject activeWeapon;

    public GameObject GetActiveWeapon()
    {
        return activeWeapon;
    }

    void Start()
    {
        activeWeaponType = "Player_Sword";
        activeWeapon = Sword;
    }

    void Update()
    {
#if UNITY_EDITOR
        if (Input.GetKeyDown("1"))
        {
            loadWeapon(Sword);
            activeWeaponType = "Player_Sword";
        }
        else if (Input.GetKeyDown("2"))
        {
            loadWeapon(Bow);
            activeWeaponType = "Player_Bow";
        }
        //else if (Input.GetKeyDown("3"))
        //{
        //    loadWeapon(Staff);
        //    activeWeaponType = "Player_Staff";
        //}
        else if (Input.GetKeyDown("4"))
        {
            loadWeapon(Spear);
            activeWeaponType = "Player_Spear";
        }
        else if (Input.GetKeyDown("5"))
        {
            loadWeapon(Shield);
            activeWeaponType = "Player_Shield";
        }
#endif

#if UNITY_ANDROID && !UNITY_EDITOR
        if (Input.GetKeyDown("1"))
        {
            loadWeapon(Sword);
            activeWeaponType = "Player_Sword";
        }
        else if (Input.GetKeyDown("2"))
        {
            loadWeapon(Bow);
            activeWeaponType = "Player_Bow";
        }
        //else if (Input.GetKeyDown("3"))
        //{
        //    loadWeapon(Staff);
        //    activeWeaponType = "Player_Staff";
        //}
        else if (Input.GetKeyDown("4"))
        {
            loadWeapon(Spear);
            activeWeaponType = "Player_Spear";
        }
        else if (Input.GetKeyDown("5"))
        {
            loadWeapon(Shield);
            activeWeaponType = "Player_Shield";
        }
#endif
    }

    private void loadWeapon(GameObject weapon)
    {
        Sword.SetActive(false);
        Bow.SetActive(false);
        //Staff.SetActive(false);
        Spear.SetActive(false);
        Shield.SetActive(false);
        weapon.SetActive(true);
        activeWeapon = weapon;
    }
}
