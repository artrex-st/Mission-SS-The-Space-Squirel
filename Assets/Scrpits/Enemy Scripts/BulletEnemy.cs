using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CircleCollider2D))]
public class BulletEnemy : MonoBehaviour, ICombatController
{
    public float speed = 20f, Dmg = 5f;
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
        Destroy(gameObject, 3f);
    }
    private void OnCollisionEnter2D(Collision2D coll)
    {
        StartSelfDestruction = true;
        SpriteRenderer colorBullet = GetComponent<SpriteRenderer>();
        colorBullet.color = Color.red;
        if (coll.gameObject.tag == "Player")
            coll.gameObject.GetComponent<ICombatController>().ApplyDmg(Dmg);
        Destroy(gameObject, 0.1f);
    }
    public void ApplyDmg(float dmg) //deflect effect
    {
        BulletBody.AddForce(new Vector2((transform.right.x * speed)*-3, dmg), ForceMode2D.Impulse);
        Destroy(gameObject,2f);
        StartSelfDestruction = true;

    }
}
