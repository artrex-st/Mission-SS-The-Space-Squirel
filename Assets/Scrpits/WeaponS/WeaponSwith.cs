using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwith : MonoBehaviour
{
    public static int SelectedWeapon = 0;
    void Start()
    {
        SelectWeapons();
    }

    void Update()
    {
        int previusWeapon = SelectedWeapon;
        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            if (SelectedWeapon >= transform.childCount - 1)
                SelectedWeapon = 0;
            else
                SelectedWeapon++;
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            if (SelectedWeapon <= 0)
                SelectedWeapon = transform.childCount - 1;
            else
                SelectedWeapon--;
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SelectedWeapon = 0;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SelectedWeapon = 1;
        }
        if (previusWeapon != SelectedWeapon)
        {
            SelectWeapons();
        }
    }
    void SelectWeapons()
    {
        int i = 0;
        foreach (Transform weapon in transform)
        {
            if (i == SelectedWeapon)
                weapon.gameObject.SetActive(true);
            else
                weapon.gameObject.SetActive(false);
            i++;
        }
    }
}
