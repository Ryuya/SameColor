using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public enum ColorState{
    white,
    blue,
    yellow,
    red,
}
public class Ball : MonoBehaviour
{
    private ColorState _colorState;

    public Color _color;

    public bool isTrueBall = false;
    // Start is called before the first frame update
    void Start()
    {
        if(isTrueBall == false) this.RandomColorSet();
    }
    public void RandomColorSet()
    {
        int rand = Random.Range(1,5);
        switch (rand)
        {
            case 1:
                _colorState = ColorState.white;
                GetComponent<SpriteRenderer>().color = Color.white;
                _color = Color.white;
                break;
            case 2:
                _colorState = ColorState.blue;
                GetComponent<SpriteRenderer>().color = Color.blue;
                _color = Color.blue;
                break;
            case 3:
                _colorState = ColorState.yellow;
                GetComponent<SpriteRenderer>().color = Color.yellow;
                _color = Color.yellow;
                break;
            case 4:
                _colorState = ColorState.red;
                GetComponent<SpriteRenderer>().color = Color.red;
                _color = Color.red;
                break;
        }

        if (_color == GameManager.Instance.currentColor)
        {
            RandomColorSet();
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("Player"))
        {
            if (other.collider.gameObject.GetComponent<SpriteRenderer>().color == _color)
            {
                GameManager.Instance.NextStage();
                Destroy(gameObject);
            }
            else
            {
                GameManager.Instance.GameOver();
            }
        }
    }
}
