using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CircleCollider2D))]
public class Bullet : MonoBehaviour
{
    public float speed = 20f, Dmg = 5f;
    public Rigidbody2D BulletBody;
    public bool StartSelfDestruction;
    public Collider2D[] hitEnemies;

    private void Awake()
    {
        BulletBody = GetComponent<Rigidbody2D>();
        //speed += GameObject.Find("Player").GetComponent<Rigidbody2D>().velocity.x;
        BulletBody.AddForce(new Vector2(transform.right.x * speed, 0), ForceMode2D.Impulse);
        Dmg = GameObject.Find("Player").GetComponent<PlayerController>().playerStatus.attack;
        
        
    }
    void Start()
    {
    }
    private void Update()
    {
        if (StartSelfDestruction)
            transform.localScale = Vector3.Lerp(transform.localScale, transform.localScale * 1.4f, Time.deltaTime * 2);
        Destroy(gameObject, 3f);
    }
    private void OnCollisionEnter2D(Collision2D coll)
    {
        SpriteRenderer colorBullet = GetComponent<SpriteRenderer>();
        colorBullet.color = Color.red;
        if (coll.gameObject.tag == "Enemy")
        {
            coll.gameObject.GetComponent<GroundEnemy>().ApplyDamage(Dmg);
            StartSelfDestruction = true;
        }
        Destroy(gameObject, 0.1f);
        //hitEnemies = Physics2D.OverlapCircleAll(transform.position, GetComponent<CircleCollider2D>().radius * 1.1f);
        //foreach (Collider2D enemy in hitEnemies)
        //{
        //    if (enemy.tag.Equals("Enemy"))
        //    {
        //        Debug.Log("Melee Hit in:" + enemy.name);
        //        enemy.transform.root.gameObject.GetComponent<GroundEnemy>().ApplyDamage(Dmg);
        //    }
        //}
    }
}
