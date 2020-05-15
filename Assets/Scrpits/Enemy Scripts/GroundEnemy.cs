using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;


[System.Serializable]
public class EnemyGround
{
    public Vector3 scanRange = new Vector3(2,2,0);
    public bool Hunt = false;
    public float health=100;
    public float dmg=10;
    public GameObject keyDrop;
}
[System.Serializable]
public class CacheEnemyGround
{
    public Rigidbody2D rb;
    public BoxCollider2D bC;
    public LayerMask layerTarget;
    public Vector3 HuntingPosition;
    [Tooltip("Script Of Ranged Weapon.")]
    public WeaponRange WeaponScriptR;
    public Canvas canvasEnemy;
    public Material material;
    [ColorUsage(true,true)]
    public Color colorOutLineShield;
    public float outLineShieldStrengh;
}
[System.Serializable]
public class ScriptImport
{
    public UiBar healthBar;
}
//ENEMY
public class GroundEnemy : MonoBehaviour, ICombatController
{
    public EnemyGround enemy;
    public CacheEnemyGround cacheEnemy;
    public ScriptImport sI;
    //public Collider2D[] teste;
    public float coolDown;
    public quaternion testeUi;


    private void Awake()
    {
        cacheEnemy.rb = GetComponent<Rigidbody2D>();
        cacheEnemy.bC = GetComponent<BoxCollider2D>();
        cacheEnemy.layerTarget = LayerMask.GetMask("Player");
        cacheEnemy.canvasEnemy = GetComponentInChildren<Canvas>();
        cacheEnemy.material = GetComponent<SpriteRenderer>().material;
        cacheEnemy.outLineShieldStrengh = cacheEnemy.material.GetFloat("_OutLineStrengh");
        
        sI.healthBar = cacheEnemy.canvasEnemy.GetComponentInChildren<UiBar>();
        sI.healthBar.SetMaxBarValue(enemy.health);
        sI.healthBar.SetBarValue(enemy.health);



        InvokeRepeating("RangeScan", 0f, 0.5f); // RE-Scan area

    }
    private void Start()
    {
        cacheEnemy.HuntingPosition = cacheEnemy.rb.position;
    }
    private void Update()
    {
        GrondIn(enemy.Hunt);
    }
    public void RangeScan()
    {
        Collider2D[] picTarget = Physics2D.OverlapBoxAll(cacheEnemy.rb.transform.position, enemy.scanRange, 0, cacheEnemy.layerTarget);
        if (picTarget.Length <= 0)
            enemy.Hunt = false;
        else
        {
            foreach (Collider2D targetOnRange in picTarget)
            {
                enemy.Hunt = true;
                if (targetOnRange.transform.position.x < cacheEnemy.rb.position.x)
                    cacheEnemy.rb.transform.rotation = new Quaternion(0,180f,0,0);
                else
                    cacheEnemy.rb.transform.rotation = new Quaternion(0, 0, 0, 0);

            }
        }
    }
    void fire()
    {
        if (coolDown>=1)
        {
            cacheEnemy.WeaponScriptR.Shoot();
            coolDown = 0;
        }
    }
    public void ApplyDmg(float dmg)
    {
        if (enemy.Hunt)
        {
            enemy.health -= dmg;
            sI.healthBar.SetBarValue(enemy.health);
            if (enemy.health <= 0)
            {
                Instantiate(enemy.keyDrop, transform.position, transform.rotation);
                Destroy(gameObject);
            }
        }
        else
            Debug.Log("He has Protection! (don't receive damage)");
    }

    void GrondIn(bool isHunt)
    {
        if (isHunt)
        {
            cacheEnemy.bC.size = Vector2.Lerp(cacheEnemy.bC.size, new Vector2(cacheEnemy.bC.size.x, 1), 5 * Time.deltaTime);
            cacheEnemy.bC.offset = Vector2.Lerp(cacheEnemy.bC.offset, new Vector2(0, 0), 2 * Time.deltaTime);
            //Invoke("fire", 1f);
            coolDown += Time.deltaTime;
            ActiveShield(false);
            fire();
            Debug.Log("SHIELD OFF");
        }
        if (!isHunt)
        {
            cacheEnemy.bC.size = Vector2.Lerp(cacheEnemy.bC.size, new Vector2(cacheEnemy.bC.size.x, 0.25f), 3 * Time.deltaTime);
            cacheEnemy.bC.offset = Vector2.Lerp(cacheEnemy.bC.offset, new Vector2(0, 0.3f), 3 * Time.deltaTime);
            coolDown = 0;
            ActiveShield(true);
            Debug.Log("SHIELD UP");
        }
    }

    private void OnDrawGizmos()
    {
        if (cacheEnemy.rb.transform.position == null)
            return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(cacheEnemy.rb.transform.position, enemy.scanRange);
    }

    void ActiveShield(bool haveShield)
    {
        if (haveShield)
        {
            cacheEnemy.material.SetFloat("_OutLineStrengh", 4);
            cacheEnemy.material.SetColor("_Color", cacheEnemy.colorOutLineShield);
        }
        if (!haveShield)
        {
            cacheEnemy.material.SetFloat("_OutLineStrengh", 0);
            cacheEnemy.material.SetColor("_Color", new Color(0, 0, 0, 0));
        }
    }
}
