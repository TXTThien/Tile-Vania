using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float speedBullet = 20f;
    Rigidbody2D rigidbody2D;
    float xSpeed;
    PlayerMovement playerMovement;
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        playerMovement = FindObjectOfType<PlayerMovement>();
        xSpeed =  playerMovement.transform.localScale.x * speedBullet;
    }

    void Update()
    {
        rigidbody2D.velocity = new Vector2(xSpeed,0f);
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Enemy")
        {
            Destroy (other.gameObject);
        }
        Destroy (gameObject);

    }
    void OnCollisionEnter2D (Collision2D other)
    {
        Destroy (gameObject);
    }
}
