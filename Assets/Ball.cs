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
    private float damage = 0.8f;
    private ColorState _colorState;

    public Color _color;

    public bool isTrueBall = false;
    // Start is called before the first frame update
    void Start()
    {
        if(isTrueBall == false) this.RandomColorSet();
        var retio = (GameManager.Instance.level / 10);
        damage += retio;
        
    }
    public void RandomColorSet()
    {
        int rand = Random.Range(1,8);
        switch (rand)
        {
            case 1:
                _colorState = ColorState.white;
                GetComponent<SpriteRenderer>().color = Color.black;
                _color = Color.black;
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
            case 5:
                GetComponent<SpriteRenderer>().color = Color.gray;
                _color = Color.gray;
                break;
            case 6:
                GetComponent<SpriteRenderer>().color = Color.green;
                _color = Color.green;
                break;
            case 7:
                GetComponent<SpriteRenderer>().color = Color.magenta;
                _color = Color.magenta;
                break;
            case 8:
                GetComponent<SpriteRenderer>().color = Color.cyan;
                _color = Color.cyan;
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
                if(GameManager.Instance._gameState == GameState.Play)
                GameManager.Instance.NextStage();
                Destroy(gameObject);
            }
            else
            {
                GameManager.Instance.time -= damage;
                GameManager.Instance.InstantiateDamageText(damage);
            }
        }
    }
}
