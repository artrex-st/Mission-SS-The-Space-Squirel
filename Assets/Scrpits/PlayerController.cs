using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

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
    public float attack, defence, resist, breath, WeaponCooldown;
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
        sI.WeaponScriptM = FindObjectOfType<WeaponMelee>();
    }
    void Start()
    {
        cacheMove.rb.gravityScale = GetComponent<Rigidbody2D>().gravityScale;
    }

    void Update()
    {
        if (playerStatus.WeaponCooldown < 5f)
            playerStatus.WeaponCooldown += Time.deltaTime;
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
        if (Input.GetButtonDown("Fire1") && playerStatus.WeaponCooldown>1f)
        {
            attack();
        }
        // End Jump
        // crouch
        if (Input.GetButtonDown("Fire3"))
        {
            cacheMove.bc.size = new Vector2(cacheMove.bc.size.x,cacheMove.bc.size.y/2f);
            cacheMove.bc.offset = new Vector2(0, -0.25f);
            playerStatus.isCrouch = true;
        }
        if (Input.GetButtonUp("Fire3"))
        {
            cacheMove.bc.size = new Vector2(cacheMove.bc.size.x, cacheMove.bc.size.y*2);
            cacheMove.bc.offset = new Vector2(0, 0);
            playerStatus.isCrouch = false;
        }
        // end crouch


        //flip Sprite
        if (cacheMove.rb.velocity.x < 0)
            transform.rotation = new Quaternion(0, 180, 0, 0);//SpritePlayer.flipX = true;
        else
            if (cacheMove.rb.velocity.x > 0)
            transform.rotation = new Quaternion(0, 0, 0, 0); //SpritePlayer.flipX = false;
    }
    // ##### end Bases ##### //

    // ##### Precedure ##### //
    void attack()
    {
        if (WeaponSwith.SelectedWeapon == 0)
        {
            sI.WeaponScriptR.Shoot();
            playerStatus.WeaponCooldown = 0.5f;
            return;
        }
        if (WeaponSwith.SelectedWeapon == 0.75f)
        {
            sI.WeaponScriptM.MeleeAttack();
            playerStatus.WeaponCooldown = 1;
            return;
        }
        playerStatus.WeaponCooldown = 0;
    }
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

    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        
    }
    // ##### end Colliders ##### //

    // ##### Functions ##### //
    private bool fIsGround()
    {
        if (playerStatus.isCrouch)
        {
            return Physics2D.OverlapBox(new Vector2(cacheMove.bc.transform.position.x, cacheMove.bc.transform.position.y- (cacheMove.bc.size.y / 2)), new Vector2(cacheMove.bc.size.x * 0.9f, cacheMove.bc.size.y * 1.01f), 0, cacheMove.layer, 0, 0.1f);
        }else
            return Physics2D.OverlapBox(cacheMove.bc.transform.position, new Vector2(cacheMove.bc.size.x * 0.9f, cacheMove.bc.size.y*1.01f), 0, cacheMove.layer, 0, 0.1f);
        //return Physics2D.BoxCast(new Vector2(transform.localPosition.x, transform.localPosition.y), new Vector2(BodySize.size.x * 0.4f, BodySize.size.y * 0.9f), 0, Vector2.down, 0.1f, GroundLayer);
    }
    // ##### end Functions ##### //

    // ##### Gizmos ##### //
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        if (playerStatus.isCrouch)
        {
            Gizmos.DrawWireCube(new Vector2(cacheMove.bc.transform.position.x,cacheMove.bc.transform.position.y - (cacheMove.bc.size.y/2)), new Vector2(cacheMove.bc.size.x * 0.9f, cacheMove.bc.size.y * 1.01f));
        }
        else
            Gizmos.DrawWireCube(cacheMove.bc.transform.position, new Vector2(cacheMove.bc.size.x * 0.9f, cacheMove.bc.size.y * 1.01f));

    }
}
