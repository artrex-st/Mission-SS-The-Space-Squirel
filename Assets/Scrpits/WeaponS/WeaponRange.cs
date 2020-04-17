using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponRange : MonoBehaviour
{
    public Transform FirePoint;
    public GameObject[] Bullet; // more that one bullet type

    private void Awake()
    {
        if (FirePoint==null)
        {
            FirePoint = transform;
        }
    }
    public void Shoot()
    {
        Instantiate(Bullet[0]/* Type of bullety */, FirePoint.position, FirePoint.rotation, transform);
    }

}
