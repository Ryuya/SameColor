using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
    public bool isLeft=false, isRight=false;
    // Start is called before the first frame update
    public Text isRightVal, isLeftVal;
    public CameraShake shake;
    public GameObject colEffectPrefab;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //isRightVal.text = isRight.ToString();
        //isLeftVal.text = isLeft.ToString();
        if (GameManager.Instance._gameState == GameState.Play || GameManager.Instance._gameState == GameState.Start)
        {
            if (Input.GetKey(KeyCode.LeftArrow))
            {

                rb.AddForce(new Vector2(-1 * speed * 100 * Time.deltaTime, 0));
            } else if (Input.GetKey(KeyCode.RightArrow))
            {

                rb.AddForce(new Vector2(1 * speed * 100 * Time.deltaTime, 0));
            }

            if (Input.GetKey(KeyCode.Space))
            {
                rb.AddForce(new Vector2(0f, 5 * speed * Time.deltaTime), ForceMode2D.Impulse);
            }

            if (Input.touchCount > 0)
            {
                foreach (Touch touch in Input.touches)
                {
                    // 座標xがスクリーンの2分の1以上の場合
                    if (touch.position.x >= Screen.width / 2)
                    {
                        // 右側をタップしたら左のフリッパーが動く
                        if (touch.phase == TouchPhase.Began || touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
                        {
                            isRight = true;

                        }
                        if (touch.phase == TouchPhase.Canceled || touch.phase == TouchPhase.Ended )
                        {
                            isRight = false;
                        }
                    }
                    if (touch.position.x <= Screen.width / 2)
                    {

                        // 左側をタップしたら左のフリッパーが動く
                        if (touch.phase == TouchPhase.Began || touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
                        {
                            isLeft = true;

                        }
                        if (touch.phase == TouchPhase.Canceled || touch.phase == TouchPhase.Ended)
                        {
                            isLeft = false;
                        }
                    }
                }
            } else
            {
                isRight = false;
                isLeft = false;
            }

            if (isRight)rb.AddForce(new Vector2(3.5f * speed * 1330 * Time.deltaTime, 0),ForceMode2D.Impulse);
            if(isLeft)rb.AddForce(new Vector2(-3.5f * speed * 1330 * Time.deltaTime, 0), ForceMode2D.Impulse);
            if (isLeft && isRight)
            {
                rb.AddForce(new Vector2(0f, 5 * speed), ForceMode2D.Impulse);
            }
            rb.velocity = new Vector2(Mathf.Clamp(rb.velocity.x, -5.5f, 5.5f), Mathf.Clamp(rb.velocity.y, -3.5f, 3.5f));
        }
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        if (isAttack && other.collider.CompareTag("Ball"))
        {
            //GameManager.Instance.time += other.collider.GetComponent<Ball>().damage;
            //GameManager.Instance.InstantiateTakeTimeText(other.collider.GetComponent<Ball>().damage);
            Destroy(other.gameObject);
        } else
        {
            if (other.collider.CompareTag("TrueBall"))
            {
                Time.timeScale = 0.0f;
                var balls = GameObject.FindGameObjectsWithTag("Ball");
                foreach (var ball in balls)
                {
                    Destroy(ball);
                }
                var balls2 = GameObject.FindGameObjectsWithTag("TrueBall");
                foreach (var ball in balls2)
                {
                    Destroy(ball);
                }
                GameManager.Instance.NextStage();
            }
            if (other.gameObject.CompareTag("Ball"))
            {
                Instantiate(colEffectPrefab, other.contacts[0].point, Quaternion.FromToRotation(Vector3.down, other.contacts[0].normal));
                GameManager.Instance.SlowDown();
                shake.Shake(0.2f, 0.1f);
            }
        }
    }
}
