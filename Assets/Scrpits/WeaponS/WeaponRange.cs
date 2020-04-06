using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponRange : MonoBehaviour
{
    public Transform FirePoint;
    public GameObject[] Bullet; // more that one bullet type

    public void Shoot()
    {
        Instantiate(Bullet[0]/* Tipy of bullety */, FirePoint.position, FirePoint.rotation);
    }
}
