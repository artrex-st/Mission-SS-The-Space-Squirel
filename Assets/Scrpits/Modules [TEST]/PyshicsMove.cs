using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PyshicsMove : MonoBehaviour, IMoveInterface
{
    [SerializeField] private float moveSpeed=0;
    private Vector2 moveVector;
    private Rigidbody2D rb2D;

    private void Awake()
    {        
        rb2D = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        rb2D.velocity = new Vector2(moveVector.x * moveSpeed, rb2D.velocity.y);
    }
    //
    public void SetMove(float moveVectorX, float moveVectorY, float moveVectorZ)
    {
        moveVector = new Vector2(moveVectorX,rb2D.velocity.y);
    }
}
