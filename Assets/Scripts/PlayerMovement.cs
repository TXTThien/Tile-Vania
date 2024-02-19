using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float speed= 10f;
    [SerializeField] float jump = 5f;
    [SerializeField] float climb = 5f;
    [SerializeField] Vector2 deathkick = new Vector2 (20f,20f);
    [SerializeField] GameObject bullet;
    [SerializeField] Transform gun;

    [SerializeField] int Alive = 3;
    BoxCollider2D boxCollider2D;
    CapsuleCollider2D capsuleCollider2D;
    Vector2 moveInput;
    Rigidbody2D rigidbody2D;
    Animator animator;

    float startGravity;
    bool isAlive=true;
    void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        capsuleCollider2D = GetComponent<CapsuleCollider2D>();
        boxCollider2D = GetComponent<BoxCollider2D>();
    }
    void Start() 
    {
        startGravity=rigidbody2D.gravityScale;
    }

    void Update()
    {
        if (!isAlive) 
        {
            return;
        }
        Run();
        FlipSprite();
        ClimbLadder();
        Die();
    }

    private void ClimbLadder()
    {
        if (capsuleCollider2D.IsTouchingLayers(LayerMask.GetMask("Ladder")))
        {
            Vector2 climbVelocity = new Vector2(rigidbody2D.velocity.x, moveInput.y* climb);
            rigidbody2D.velocity=climbVelocity;
            bool playerHasVerticalSpeed = Mathf.Abs(rigidbody2D.velocity.y)>Mathf.Epsilon;
            animator.SetBool("isClimbing",playerHasVerticalSpeed);
            rigidbody2D.gravityScale=0f;
        }
        else
        {
            animator.SetBool("isClimbing",false);
            rigidbody2D.gravityScale=startGravity;
        }
    }

    private void FlipSprite()
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(rigidbody2D.velocity.x)>Mathf.Epsilon;
        if (playerHasHorizontalSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(rigidbody2D.velocity.x),1f);

        }
    }
    void OnJump(InputValue value)
    {
        if (!isAlive) 
        {
            return;
        }
        if (value.isPressed && boxCollider2D.IsTouchingLayers(LayerMask.GetMask("Ground")) == true )
        {
            rigidbody2D.velocity += new Vector2 (0f,jump);
        }
    }
    void OnMove(InputValue value)
    {
        if (!isAlive) 
        {
            return;
        }
        moveInput = value.Get<Vector2>();
    }
    void OnFire (InputValue inputValue)
    {
        if (!isAlive) 
        {
            return;
        }
        Instantiate(bullet, gun.position, transform.rotation);     
    }
    void Run()
    {
        Vector2 playerVelocity = new Vector2(moveInput.x*speed, rigidbody2D.velocity.y);
        rigidbody2D.velocity=playerVelocity;
        bool playerHasHorizontalSpeed = Mathf.Abs(rigidbody2D.velocity.x)>Mathf.Epsilon;
        animator.SetBool("isRunning",playerHasHorizontalSpeed);
    }
    void Die()
    {
        if (rigidbody2D.IsTouchingLayers(LayerMask.GetMask("Enemies","Hazard")))
        {
            isAlive=false;
            animator.SetTrigger("isDie");
            rigidbody2D.velocity=deathkick;
            FindObjectOfType<GameSession>().ProcessPlayerDeath();
        }
    }

}
