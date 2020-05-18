using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    
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
        if (GameManager.Instance.time >= 30f)
        {
            GameManager.Instance.player.isAttack = true;
            GameManager.Instance.time -= 30f;
            GameManager.Instance.timeText.text = GameManager.Instance.time.ToString("F2");
            button.transform.Find("Text").GetComponent<Text>().text = "購入済み";
            button.interactable = false;
        }
    }
    
    public void Go()
    {
        gameObject.SetActive(false);
        GameManager.Instance.homeLevelText.text = GameManager.Instance.level.ToString();
        Time.timeScale = 1.0f;
        GameManager.Instance._gameState = GameState.Play;
    }
}
