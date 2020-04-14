using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpDown : MonoBehaviour
{
    public Vector3 initionPos;
    public float CD;
    public float maxTimeToDown=7;
    public float minTimeToDown=1;
    public bool getDown= true;
    void Start()
    {
        initionPos = transform.position ;
    }

    // Update is called once per frame
    void Update()
    {
        CD += Time.deltaTime;
        if (CD >= Random.Range(minTimeToDown, maxTimeToDown))
            getDown = false;
        if (initionPos.y <= transform.position.y)
        {
            getDown = true;
            CD = 0;
        }
        //
        if (getDown)
        {
            print("Down");
            transform.position = Vector2.Lerp(transform.position, new Vector2(transform.position.x, transform.position.y - 10 * Time.deltaTime), 20 * Time.deltaTime);
        }

        if (!getDown)
        {
            transform.position = Vector2.Lerp(transform.position, new Vector2(transform.position.x, transform.position.y + 10 * Time.deltaTime), 20 * Time.deltaTime);
        }


    }
}
