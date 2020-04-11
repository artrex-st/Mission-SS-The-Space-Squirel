using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;


[System.Serializable]
public class EnemyGround
{
    public Vector3 scanRange = new Vector3(2,2,0);
    public bool Hunt = false;


}
[System.Serializable]
public class CacheEnemyGround
{
    public Rigidbody2D rb;
    public LayerMask layerTarget;
    public Vector3 HuntingPosition;
    [Tooltip("Script Of Ranged Weapon.")]
    public WeaponRange WeaponScriptR;
}



//ENEMY
public class GroundEnemy : MonoBehaviour
{
    public EnemyGround enemy;
    public CacheEnemyGround cacheEnemy;
    public Collider2D[] teste;
    public float CD;

    private void Awake()
    {
        cacheEnemy.rb = GetComponentInChildren<Rigidbody2D>();
        cacheEnemy.layerTarget = LayerMask.GetMask("Player");
        InvokeRepeating("RangeScan", 0f, 0.5f); // RE-Scan area
    }
    private void Start()
    {
        cacheEnemy.HuntingPosition = cacheEnemy.rb.position;
    }
    private void Update()
    {
        CD += Time.deltaTime;
        if (enemy.Hunt)
        {
            cacheEnemy.rb.transform.position = cacheEnemy.HuntingPosition;
            Invoke("fire", 0.3f);
        }else
            cacheEnemy.rb.transform.position = new Vector3(cacheEnemy.HuntingPosition.x, cacheEnemy.HuntingPosition.y - 0.5f);
    }
    IEnumerator WaitAseconds()
    {
        //invoke with StartCoroutine("WaitAseconds");
        yield return new WaitForSeconds(1f);
        cacheEnemy.WeaponScriptR.Shoot();
    }
    void fire()
    {
        if (CD>=1)
        {
            cacheEnemy.WeaponScriptR.Shoot();
            CD = 0;
        }
    }

    // Range of hunt
    public void RangeScan()
    {
        Collider2D[] picTarget = Physics2D.OverlapBoxAll(cacheEnemy.rb.transform.position, enemy.scanRange, 0, cacheEnemy.layerTarget);
        teste = Physics2D.OverlapBoxAll(transform.position, enemy.scanRange, 0, cacheEnemy.layerTarget);
        if (picTarget.Length <= 0)
        {
            Debug.Log("Scan not found target. Ground Now!");
            enemy.Hunt = false;
            
        }
        else
        {
            foreach (Collider2D targetOnRange in picTarget)
            {
                Debug.Log("Scan Result is:" + targetOnRange.name + " Start hunt!");
                enemy.Hunt = true;
                if (targetOnRange.transform.position.x < cacheEnemy.rb.position.x)
                    cacheEnemy.rb.transform.rotation = new Quaternion(0,180f,0,0);
                else
                    cacheEnemy.rb.transform.rotation = new Quaternion(0, 0, 0, 0);
            }
        }

    }


    private void OnDrawGizmos()
    {
        if (cacheEnemy.rb.transform.position == null)
            return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(cacheEnemy.rb.transform.position, enemy.scanRange);
    }
}
