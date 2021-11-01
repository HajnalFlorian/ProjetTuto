using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMouvement : MonoBehaviour
{
    //Vitesse horizontale du joueur
    public float _speed = 5f;
    private Vector2 _movement;
    private Rigidbody2D rb;
    public float jumpForce = 6f;
    public float fallMultiplier = 5f;
    public float lowJumpMultiplier = 3f;
    bool inJump = false;
    public LayerMask WhatIsGround;
    //wall jump
    public Transform wallGrabPoint;
    private bool canGrab, isGrabbing;
    public float gravityStore;
   
    private bool isGrounded = false;
    public float wallJumpTime = .2f;
    private float wallJumpCounter;
    //dash
    public float DashSpeed;
    public float StartDashTimer;
    private float CurrentDashTimer;
    private bool isDashing;
    private float DashDirection;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        gravityStore = rb.gravityScale;
    }


    private void Update()
    {
        _movement.x = Input.GetAxisRaw("Horizontal");

        //Saut
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            inJump = true;
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
        if (Input.GetKeyDown(KeyCode.Space) && inJump && !isGrounded )
        {
            inJump = false;
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
        rb.velocity = new Vector2(_movement.x * _speed, rb.velocity.y);
        // flip character
        /* Vector3 characterScale = transform.localScale;
         if(Input.GetAxis("Horizontal") < 0)
         {
             characterScale.x = -1;
         }
         if (Input.GetAxis("Horizontal") > 0)
         {
             characterScale.x = 1;
         }
         transform.localScale = characterScale;*/

        if (Input.GetKeyDown(KeyCode.LeftShift) && _movement.x != 0)
        {
            isDashing = true;
            CurrentDashTimer = StartDashTimer;
            rb.velocity = Vector2.zero;
            DashDirection = (int)_movement.x;

            if (isDashing)
            {
                print("je dash connard");
                //rb.AddForce(transform.right * DashDirection * DashSpeed, ForceMode2D.Impulse);
                rb.velocity = transform.right * DashDirection * DashSpeed;
                CurrentDashTimer -= Time.deltaTime;
                if (CurrentDashTimer < 0)
                {
                    isDashing = false;

                }
            }
        }

        //handle walljimp
        /*canGrab = Physics2D.OverlapCircle(wallGrabPoint.position, 0.5f, WhatIsGround);
        isGrabbing = false;

        if(canGrab && !isGrabbing)
        {
            if(transform.localScale.x == 1f && Input.GetAxisRaw("Horizontal")>0  || transform.localScale.x == -1f && Input.GetAxisRaw("Horizontal") < 0)
            {
                isGrabbing = true;
            }
        }

        if (isGrabbing)
        {
            rb.gravityScale = 0f;
            rb.velocity = Vector2.zero;

            if (Input.GetButtonDown("Jump"))
            {
                wallJumpCounter = wallJumpTime;
                rb.velocity = new Vector2(-Input.GetAxisRaw("Horizontal")* _speed, jumpForce);
                rb.gravityScale = gravityStore;
                isGrabbing = false;
            }
        }
        else
        {
            rb.gravityScale = gravityStore;
        }*/

        //dash




        

        /*if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 2) * Time.deltaTime;
        }
        else if (rb.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 2) * Time.deltaTime;
        }*/
    }


    private void FixedUpdate()
    {
       // rb.velocity = new Vector2(_movement.x * _speed, rb.velocity.y);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Sol")
        {
            inJump = false;
            isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Sol")
        {
            isGrounded = false;
        }
    }


}
