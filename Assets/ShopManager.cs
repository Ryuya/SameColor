﻿using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    public bool isBaughtAddToge = false;
    public Button attackBuyButton,TrueBallButton,TogeButton;
    public void OnEnable()
    {
        if (GameManager.Instance.player.isAttack == false)
        {
            attackBuyButton.interactable = GameManager.Instance.money > attackBuyButton.GetComponent<Place>().place ? true : false;    
        }
        TrueBallButton.interactable = GameManager.Instance.money > TrueBallButton.GetComponent<Place>().place ? true : false;
        TogeButton.interactable = GameManager.Instance.money > TogeButton.GetComponent<Place>().place ? true : false;
        if (GameManager.Instance.trueBallNum >= 3)
        {
            TrueBallButton.interactable = false;            
        }
        if (isBaughtAddToge == true)
        {
            TogeButton.interactable = false;
        }
        

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Return))
        {
            this.Go();
        }
    }

    //一個目のアイテム　プレイヤーがTrueボール以外のボールにあたるとボールを消す
    public void BuyAttack(Button button)
    {
        var place = button.GetComponent<Place>().place;
        if (GameManager.Instance.money >= place)
        {
            GameManager.Instance.player.isAttack = true;
            GameManager.Instance.money -= place;
            GameManager.Instance.moneyText.text = GameManager.Instance.money.ToString("F2");
            button.transform.Find("PlaceText").GetComponent<Text>().text = "購入済み";
            button.interactable = false;
        }
    }

    public void BuyTrueBall(Button button)
    {
        int[] placeAry = {10, 50, 200};
        var place = placeAry[GameManager.Instance.trueBallNum - 1];
        if (GameManager.Instance.money >= place)
        {
            if (GameManager.Instance.trueBallNum < 3)
            {
                GameManager.Instance.money -= place;
                GameManager.Instance.trueBallNum += 1;
                GameManager.Instance.moneyText.text = GameManager.Instance.money.ToString("F2");
                button.transform.Find("PlaceText").GetComponent<Text>().text = placeAry[GameManager.Instance.trueBallNum-1].ToString() + " Gold";
                TrueBallButton.GetComponent<Place>().place = place;
            }
            else
            {
                button.transform.Find("PlaceText").GetComponent<Text>().text = "購入済み";
                button.interactable = false;
            }
            
        }
    }

    public void BuyTrueBallAddToge(Button button)
    {
        var place = button.GetComponent<Place>().place;
        if (GameManager.Instance.money >= place)
        {
            if (!isBaughtAddToge)
            {
                isBaughtAddToge = true;
                GameManager.Instance.money -= place;
                GameManager.Instance.moneyText.text = GameManager.Instance.money.ToString("F2");
                button.transform.Find("PlaceText").GetComponent<Text>().text = "購入済み";
                button.interactable = false;
            }
            
        }
    }
    
    public void Go()
    {
        gameObject.SetActive(false);
        GameManager.Instance.homeLevelText.text = GameManager.Instance.level.ToString();
        Time.timeScale = 1.0f;
        GameManager.Instance.damageText.text = GameObject.Find("ball(Clone)").GetComponent<Ball>().damage.ToString();
        GameManager.Instance._gameState = GameState.Play;
    }
}
