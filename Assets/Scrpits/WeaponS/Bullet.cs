using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CircleCollider2D))]
public class Bullet : MonoBehaviour
{
    public float speed = 20f, Dmg = 15f;
    public Rigidbody2D BulletBody;
    public bool StartSelfDestruction;
    void Start()
    {
        BulletBody = GetComponent<Rigidbody2D>();
        BulletBody.AddForce(new Vector2(transform.right.x * speed, 0), ForceMode2D.Impulse);
    }
    private void Update()
    {
        if (StartSelfDestruction)
            transform.localScale = Vector3.Lerp(transform.localScale, transform.localScale * 1.4f, Time.deltaTime * 2);
        Destroy(gameObject, 5);
    }
    private void OnCollisionEnter2D(Collision2D coll)
    {
        StartSelfDestruction = true;
        SpriteRenderer colorBullet = GetComponent<SpriteRenderer>();
        colorBullet.color = Color.red;
        Destroy(gameObject, 0.1f);
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, GetComponent<CircleCollider2D>().radius * 1.1f);
        if (coll.gameObject.tag == "Enemy")
            coll.gameObject.GetComponent<PlayerController>().ApplyDamage(Dmg);
    }
    public void GetTagOf()
    {

    }
}
