using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrueBall : MonoBehaviour
{
    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ball"))
        {
            if (other.gameObject.GetComponent<Ball>().isTrueBall == false)
            {
                Destroy(other.gameObject);
            }
        }
    }
}
