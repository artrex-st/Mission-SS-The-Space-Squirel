using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillBoard : MonoBehaviour
{
    public Transform Target;
    private void Awake()
    {
        Target = GameObject.FindGameObjectWithTag("MainCamera").transform;
    }
    void LateUpdate()
    {
        transform.LookAt(transform.position + Target.forward);
    }
}
