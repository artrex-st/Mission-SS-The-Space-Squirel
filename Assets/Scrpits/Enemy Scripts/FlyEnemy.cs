using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class FlyEnemy : MonoBehaviour
{
    public Vector2 target,spawnEnemy;
    public float speed = 200f;
    public float nextWaypointDistance = 3f;
    public float scanRange=5;
    public LayerMask targetLayer;
    Path path;
    int currentWaypoint = 0;
    //bool reachedEndOfPath = false;
    Seeker seeker;
    Rigidbody2D rb;
    public Collider2D[] teste;

    void Start()
    {
        spawnEnemy = transform.position;
        target = GameObject.FindWithTag("Player").transform.position;
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        InvokeRepeating("UpdatePath", 0f, 0.2f);
        InvokeRepeating("RangeScan", 0f, 1f);
    }
    void UpdatePath()
    {
        if (seeker.IsDone())
        {
            seeker.StartPath(rb.position, target, OnPathComplete);
        }
    }
    void FixedUpdate()
    {
        if (path == null)
        {
            return;
        }
        if (currentWaypoint >= path.vectorPath.Count)
        {
            //reachedEndOfPath = true;
            rb.gravityScale = 0;
            return;
        }
        else
        {
            //reachedEndOfPath = false;
            rb.gravityScale = 1;
        }
        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 force = direction * speed;
        rb.AddForce(force);
        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);
        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }
    }
    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }
    public void RangeScan()
    {
        Collider2D[] picTarget = Physics2D.OverlapCircleAll(transform.position, scanRange, targetLayer);
        teste = Physics2D.OverlapCircleAll(transform.position, scanRange);
        if (picTarget.Length <=0)
        {
            Debug.Log("Scan not found target. back to respaw point");
            target = spawnEnemy;
        }
        else
        {
            foreach (Collider2D targetOnRange in picTarget)
            {
                Debug.Log("Scan Result is:" + targetOnRange.name+" Start hunt!");
                target = targetOnRange.transform.position; //use the transform in this range to target
            }   
        }

    }
    private void OnDrawGizmos()
    {
        if (transform.position == null)
            return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, scanRange);
    }
}
