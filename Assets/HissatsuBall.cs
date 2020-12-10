using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class HissatsuBall : Ball
{
    // Start is called before the first frame update
    void Start()
    {
        if(isTrueBall == false) this.RandomColorSet();
        damage = int.MaxValue;
        GetComponent<SpriteRenderer>().color = Color.black;
        _color = Color.black;
    }
}
