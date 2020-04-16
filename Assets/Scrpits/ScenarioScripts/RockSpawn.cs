using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class RockSpawn : MonoBehaviour
{
    // In this example we show how to invoke a coroutine and
    // continue executing the function in parallel.

    private IEnumerator coroutine;
    public GameObject respawnRock, rockDestroyPoint;
    public LayerMask layerOfRock;
    public float rangeToDestroy;
    [Range(1,10), Tooltip("Max Seconds to Randomize Rock respawn.(more than 1)")]
    public float maxTimeToRespawnRock=1;
    [Range(1,10), Tooltip("Min Seconds to Randomize Rock respawn.(more than 1)")]
    public float minTimeToRespawnRock=1;

    void Start()
    {
        coroutine = WaitUntilRespawnRock(Random.Range(minTimeToRespawnRock, maxTimeToRespawnRock));
        StartCoroutine(coroutine);
    }
    private void Update()
    {
        fRockTouth();
    }
    // every 2 seconds perform the print()
    private IEnumerator WaitUntilRespawnRock(float waitTime)
    {
        while (true)
        {
            yield return new WaitForSeconds(waitTime);
            RespawnRock();
        }
    }
    public void RespawnRock()
    {
        Instantiate(respawnRock,transform.position, transform.rotation);
    }
    private void fRockTouth()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(rockDestroyPoint.transform.position, rangeToDestroy, layerOfRock);
        foreach (Collider2D enemy in hitEnemies)
        {
            //Debug.Log(enemy.name+"Hiting to destroy.");
            Destroy(enemy.gameObject);
        }
    }
    private void OnDrawGizmos()
    {
        if (transform.position == null)
            return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(rockDestroyPoint.transform.position, rangeToDestroy);
    }
}
