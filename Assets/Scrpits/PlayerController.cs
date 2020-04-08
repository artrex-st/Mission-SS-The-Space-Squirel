using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.SocialPlatforms;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(BoxCollider2D))]

[System.Serializable]
public class PlayerStatus
{
    // float
    [Tooltip("Max Health value.")]
    public float maxHP;
    [Tooltip("Current Health value.")]
    public float currHP;
    [Range(0f, 50f), Tooltip("Speed of player will moved.")]
    public float speed = 10;
    [Range(0f, 50f), Tooltip("The more jumpforce it has, more higher and faster it will go.")]
    public float jumpForce = 20;
    [Range(0f, 10f), Tooltip("Building...")]
    public float gravity = 2;
    [Range(0f, 100f), Tooltip("How much fuel his has. (need fuel to fly)")]
    public float fuel = 50;
    [Range(0f, 50f), Tooltip("The higher is the value, more faster it will goes to up")]
    public float flyForce = 40f;
    [Range(-30f,30f), Tooltip("Axis of movement direction.")]
    public float moveTo;
    //
    [Range(-100f, 100f), Tooltip("Building...")]
    public float attack;
    [Range(-100f, 100f), Tooltip("Building...")]
    public float defence;
    [Range(-100f, 100f), Tooltip("Building...")]
    public float resist;
    [Range(-100f, 100f), Tooltip("Building...")]
    public float breath;
    [Range(-10f, 10f), Tooltip("Time to use next Attack.")]
    public float WeaponCooldown;
    // booleans
    [Tooltip("Indicate if Player Can Move.")]
    public bool canMove;
    [Tooltip("Indicates if Player has moving.")]
    public bool isMoving;
    [Tooltip("Indicates if Player has touche the ground layers.")]
    public bool isGround;
    [Tooltip("Indicates if Player has Crouch.")]
    public bool isCrouch;
    [Tooltip("Indicates if Player has Falling")]
    public bool isFalling;
    [Tooltip("Building...")]
    public bool isWall;
    [Tooltip("Indicates if Player can Fly.")]
    public bool fly;
    
}
[System.Serializable]
public class CacheMove
{
    public Rigidbody2D rb;
    public BoxCollider2D bc;
    [Tooltip("Cache of Layer check: Ground. (needed to jump and fly)")]
    public LayerMask layerOfGround;
    [Tooltip("Testing (building...)")]
    public Animator animator;
}
[System.Serializable]
public class ScriptsImport
{
    [Tooltip("Script Of Ranged Weapon.")]
    public WeaponRange WeaponScriptR;
    [Tooltip("Script Of Melee Weapon.")]
    public WeaponMelee WeaponScriptM;
}

public class PlayerController : MonoBehaviour
{
    [Tooltip("Player Variables and conditions")]
    public PlayerStatus playerStatus;
    [Tooltip("Player Cache Components")]
    public CacheMove cacheMove;
    [Tooltip("External Scripts Importations")]
    public ScriptsImport sI;
    [Range(-2f, 10f)]
    public float cooldown;
    private GameObject testeSword;

    // ##### Bases ##### //
    private void Awake()
    {
        cacheMove.rb = GetComponent<Rigidbody2D>();
        cacheMove.bc = GetComponent<BoxCollider2D>();
        cacheMove.animator = GetComponentInChildren<Animator>();
        sI.WeaponScriptR = GetComponentInChildren<WeaponRange>();
        sI.WeaponScriptM = FindObjectOfType<WeaponMelee>();
        testeSword = GameObject.Find("Sword");
    }
    void Start()
    {
        cacheMove.rb.gravityScale = GetComponent<Rigidbody2D>().gravityScale;
    }

    void Update()
    {
        WeaponSprite();
        if (playerStatus.WeaponCooldown < 5f)
            playerStatus.WeaponCooldown += Time.deltaTime;
        if (fIsGround())
        {
            playerStatus.canMove = true;
            playerStatus.fly = true;
        }
        else
            playerStatus.canMove = false;
        //
        GetAxis();
        if (playerStatus.canMove || playerStatus.fly)
            cacheMove.rb.velocity = new Vector2(playerStatus.moveTo, cacheMove.rb.velocity.y);

        //jump / fly
        if (Input.GetButtonDown("Jump") && fIsGround())
            Jump();
        if (Input.GetButton("Jump") && playerStatus.fly && !fIsGround())
        {
            Fly();
        }
        else if (Input.GetButton("Jump") && playerStatus.fuel <= 0)
        {
            Debug.LogWarning("Fuel is Empty!");
        }
        // End Jump / fly

        if (Input.GetButtonDown("Fire1") && playerStatus.WeaponCooldown > 1f)
        {
            attack();
        }
        // crouch
        if (Input.GetButtonDown("Fire3"))
        {
            cacheMove.bc.size = new Vector2(cacheMove.bc.size.x, cacheMove.bc.size.y / 2f);
            cacheMove.bc.offset = new Vector2(0, -0.25f);
            playerStatus.isCrouch = true;
        }
        if (Input.GetButtonUp("Fire3"))
        {
            cacheMove.bc.size = new Vector2(cacheMove.bc.size.x, cacheMove.bc.size.y * 2);
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
        if (WeaponSwith.SelectedWeapon == 0) // weapon selected
        {
            sI.WeaponScriptR.Shoot();
            playerStatus.WeaponCooldown = 0.5f;
        }
        if (WeaponSwith.SelectedWeapon == 1) // weapon selected
        {
            cacheMove.animator.SetTrigger("Attack");
            sI.WeaponScriptM.MeleeAttack();
            playerStatus.WeaponCooldown = 0.7f;
        }
    }
    void GetAxis()
    {
        playerStatus.moveTo = Input.GetAxis("Horizontal") * playerStatus.speed;
    }
    void Jump()
    {
        if (!fIsGround() && playerStatus.fuel <= 0)
        {
            playerStatus.fly = false;
        }
        cacheMove.rb.AddForce(new Vector2(0, playerStatus.jumpForce * 10), ForceMode2D.Impulse);
    }
    void Fly()
    {
        if (!fIsGround() && playerStatus.fuel <= 0)
        {
            playerStatus.fly = false;
        }
        cacheMove.rb.AddForce(new Vector2(0, playerStatus.flyForce * 25), ForceMode2D.Force);
        playerStatus.fuel -= 3 * Time.deltaTime;
        Debug.Log("Fuel in:" + playerStatus.fuel);
    }
    void WeaponSprite()
    {
        if (WeaponSwith.SelectedWeapon==1)
            testeSword.SetActive(true);
        else
            testeSword.SetActive(false);
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
            return Physics2D.OverlapBox(new Vector2(cacheMove.bc.transform.position.x, cacheMove.bc.transform.position.y- (cacheMove.bc.size.y / 2)), new Vector2(cacheMove.bc.size.x * 0.9f, cacheMove.bc.size.y * 1.01f), 0, cacheMove.layerOfGround, 0, 0.1f);
        }else
            return Physics2D.OverlapBox(cacheMove.bc.transform.position, new Vector2(cacheMove.bc.size.x * 0.9f, cacheMove.bc.size.y*1.01f), 0, cacheMove.layerOfGround, 0, 0.1f);
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
