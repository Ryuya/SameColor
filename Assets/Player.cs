using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody2D rb;
    float speed = 32f;
    Vector2 velocity;
    //public Vector2 Velocity
    //{
    //    set { this.velocity = new Vector2 (Mathf.Clamp(value.x,0f,7.5f), Mathf.Clamp(value.x, 0f, 7.5f)); }
    //    get { return this.velocity; }
    //}
    public bool isAttack = false;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance._gameState == GameState.Play)
        {
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                Debug.Log("left");
                rb.AddForce(new Vector2(-1 * speed,0));
            } else if (Input.GetKey(KeyCode.RightArrow))
            {
                Debug.Log("right");
                rb.AddForce(new Vector2(1 * speed, 0));
            }

            if (Input.GetKey(KeyCode.Space))
            {
                rb.AddForce(new Vector2(0f,5 * speed),ForceMode2D.Impulse);
            }
            rb.velocity = new Vector2(Mathf.Clamp(rb.velocity.x, -3.5f, 3.5f), Mathf.Clamp(rb.velocity.y, -3.5f, 3.5f));

        }
        
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        if (isAttack && other.collider.CompareTag("Ball"))
        {
            //GameManager.Instance.time += other.collider.GetComponent<Ball>().damage;
            //GameManager.Instance.InstantiateTakeTimeText(other.collider.GetComponent<Ball>().damage);
            Destroy(other.gameObject);
        }
    }
}
