﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(BoxCollider2D))]

[System.Serializable]
public class PlayerStatus
{
    public float maxHP, currHP;
    [Range(0f, 50f)]
    public float speed = 10, jumpForce = 20, gravity = 2;
    [Range(-30f,30f)]
    public float moveTo;
    public bool canMove, isMoving, isGround, isCrouch, isFalling, isWall, doubleJump;
    [Range(-100f, 100f)]
    public float attack, defence, resist, breath;
}
[System.Serializable]
public class CacheMove
{
    public Rigidbody2D rb;
    public BoxCollider2D bc;
    public LayerMask layer;
}
[System.Serializable]
public class ScriptsImport
{
    public WeaponRange WeaponScriptR;
    public WeaponMelee WeaponScriptM;
}

public class PlayerController : MonoBehaviour
{
    public PlayerStatus playerStatus;
    public CacheMove cacheMove;
    public ScriptsImport sI;
    [Range(-2f,10f)]
    public float cooldown;

    // ##### Bases ##### //
    private void Awake()
    {
        cacheMove.rb = GetComponent<Rigidbody2D>();
        cacheMove.bc = GetComponent<BoxCollider2D>();
        sI.WeaponScriptR = GetComponentInChildren<WeaponRange>();
        sI.WeaponScriptM = GetComponent<WeaponMelee>();
    }
    void Start()
    {
        cacheMove.rb.gravityScale = GetComponent<Rigidbody2D>().gravityScale;
    }

    void Update()
    {
        if (fIsGround())
        {
            playerStatus.canMove = true;
            playerStatus.doubleJump = true;
        }
        else
            playerStatus.canMove = false;
        //
        GetAxis();
        if(playerStatus.canMove)
            cacheMove.rb.velocity = new Vector2(playerStatus.moveTo,cacheMove.rb.velocity.y);
        //jump
        if (Input.GetButtonDown("Jump") && fIsGround())
            Jump();
        else if (Input.GetButtonDown("Jump") && playerStatus.doubleJump)
        {
            Jump();
        }
        if (Input.GetButtonDown("Fire1"))
        {
            sI.WeaponScriptR.Shoot();
        }
    }
    // ##### end Bases ##### //

    // ##### Precedure ##### //
    void GetAxis()
    {
        playerStatus.moveTo = Input.GetAxis("Horizontal") * playerStatus.speed;
    }
    void Jump()
    {
        if (!fIsGround())
        {
            playerStatus.doubleJump = false;
        }
        cacheMove.rb.AddForce(new Vector2(0, playerStatus.jumpForce * 10), ForceMode2D.Impulse);
    }
    // ##### end Procedures ##### //

    // ##### Colliders ##### //
    private void OnCollisionEnter2D(Collision2D coll)
    {
        
    }
    private void OnCollisionStay2D(Collision2D coll)
    {
        //if (coll.gameObject.name.Equals("Ground"))
            //Debug.Log("Grounded");

    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        
    }
    // ##### end Colliders ##### //

    // ##### Functions ##### //
    private bool fIsGround()
    {
        return Physics2D.OverlapBox(cacheMove.bc.transform.position, new Vector2(cacheMove.bc.size.x - 0.3f, cacheMove.bc.size.y+0.03f), 0, cacheMove.layer, 0, 0.1f);
        //return Physics2D.BoxCast(new Vector2(transform.localPosition.x, transform.localPosition.y), new Vector2(BodySize.size.x * 0.4f, BodySize.size.y * 0.9f), 0, Vector2.down, 0.1f, GroundLayer);
    }
    // ##### end Functions ##### //

    // ##### Gizmos ##### //
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(cacheMove.bc.transform.position, new Vector2(cacheMove.bc.size.x - 0.1f, cacheMove.bc.size.y+0.01f));

    }
}
