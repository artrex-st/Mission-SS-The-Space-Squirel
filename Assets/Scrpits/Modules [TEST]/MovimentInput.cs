using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimentInput : MonoBehaviour
{
    void Update()
    {
        float move = Input.GetAxis("Horizontal");
        GetComponent<IMoveInterface>().SetMove(move,0,0);
    }
}
