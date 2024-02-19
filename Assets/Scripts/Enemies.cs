using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemies : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1f;
    Rigidbody2D rigidbody2D;
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        rigidbody2D.velocity = new Vector2(moveSpeed,0f);
    }
    void filp()
    {

        transform.localScale=new Vector2 (-(Mathf.Sign(rigidbody2D.velocity.x)),1f);
    }
    private void OnTriggerExit2D(Collider2D other) {
        if (other.tag != "Enemy")
        {
            moveSpeed= -moveSpeed;
            filp();  
        }
  
    }
}
