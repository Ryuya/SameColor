using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Random = UnityEngine.Random;


public enum GameState
{
    Wait,
    Start,
    Play,
    Result,
}

public class GameManager : SingletonMonoBehaviour<GameManager>
{
    [System.NonSerialized]
    public int money = 0;
    public int trueBallNum = 0;
    public bool isAttack = false;
    private float retio = 1f;
    public Color currentColor;
    public GameObject ballPrefab,hissatsuBallPdefab;
    public GameObject result;
    //現在のゲームの難易度
    public int level;
    public Text levelText;
    public float time = 30f;
    [FormerlySerializedAs("player")] public GameObject playerObj;
    public Player player;
    public Text timeText;
    public Text homeLevelText;
    public Text moneyText;
    public bool isShowResultCanvas = false;
    //ゲームの状態
    public GameState _gameState;
    public GameObject resultCanvas;
    public GameObject shopPanel;

    public Text damageText;

    public GameObject StartUI, PlayUI;

    //購入回数
    [System.NonSerialized]
    public int buyCount = 1;
    public int place = 0;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(moneyText.text);
        moneyText.text = money.ToString("F2");
        player = GameObject.Find("Player").GetComponent<Player>();
        if(_gameState == GameState.Start)
        {
            PlayUI.SetActive(false);
        }
        Setting1stGame();
    }

    // Update is called once per frame
    void Update()
    {

        switch (_gameState)
        {
            case GameState.Wait:
                Time.timeScale = 0;
                shopPanel.SetActive(true);
                break;
            case GameState.Start:
                time -= Time.deltaTime;
                break;
            case GameState.Play:
                time -= Time.deltaTime;
                timeText.text = time.ToString("F2");
                if (time < 0)
                {
                    _gameState = GameState.Result;
                }
                break;
            case GameState.Result:
                if (!isShowResultCanvas)
                {
                    isShowResultCanvas = true;
                    resultCanvas.SetActive(true);
                    levelText.text = level.ToString();
                }
                break;
        }
    }


    public void Setting1stGame()
    {
        CurrentColorSet();
        InstantiateTrueBall(trueBallNum);
    }

    public void SettingStageStart(int Level)
    {
        InstantiateFalseBall(Level);
    }

    public void NextStage()
    {
        level++;
        //プレイヤーを初期位置へ
        player.transform.position = new Vector3(0,-4,0);
        player.tag = "Player";
        var reward = 0.0f;
        reward += level * retio;
        reward = reward + (10);
        money += (int)Mathf.Floor(reward);
        time += (int) Mathf.Floor(reward);
        InstantiateTakeTimeText(reward);
        InstantiateTakeMoneyText((int)Mathf.Floor(reward));
        timeText.text = time.ToString("F2");
        moneyText.text = money.ToString("F2");
        CurrentColorSet();
        InstantiateTrueBall(trueBallNum);

        InstantiateFalseBall(level);
        _gameState = GameState.Wait;
    }

    public void GameOver()
    {
        player.tag = "Muteki";
        ShowResult();
        Debug.Log("GameOver");
    }

    public void ShowResult()
    {
        //result.SetActive(true);
    }
    public void CurrentColorSet()
    {
        int rand = Random.Range(1,7);
        switch (rand)
        {
            case 1:
                currentColor = Color.blue;
                break;
            case 2:
                currentColor = Color.yellow;
                break;
            case 3:
                currentColor = Color.red;
                break;
                break;
            case 4:
                currentColor = Color.green;
                break;
            case 5:
                currentColor = Color.magenta;
                break;
            case 6:
                currentColor = Color.cyan;
                break;
        }
    }
    
    //正解のぼーるをだす
    public void InstantiateTrueBall(int trueBallNum)
    {
        for (int i = 0; i < trueBallNum; i++)
        {
            var x = Random.Range(-8f,9.43f);
            var y = Random.Range(4.96f,5.9f);
            var z = 0f;
            var obj = GameObject.Instantiate( this.ballPrefab,new Vector3(x,y,z),Quaternion.identity);
            obj.GetComponent<Ball>().isTrueBall = true;
            obj.tag = "TrueBall";
            obj.GetComponent<SpriteRenderer>().color = currentColor;
            obj.GetComponent<Ball>()._color = currentColor;
            obj.AddComponent<TrueBall>();
            //プレイヤーの色を変える
            playerObj.GetComponent<SpriteRenderer>().color = currentColor;
        }
        
    }
    
    public void InstantiateFalseBall(int Level)
    {
        if (Level > 20) Level = 20;  
        for (int i = 0; i < Level * 3; i++)
        {
            var x = Random.Range(-8f,9.43f);
            var y = Random.Range(4.96f,5.9f);
            var z = 0f;
            if (1 == Random.Range(1,15))
            {
                var obj = GameObject.Instantiate(this.hissatsuBallPdefab, new Vector3(x, y, z), Quaternion.identity);
                obj.GetComponent<Ball>().isTrueBall = false;
            } else
            {
                var obj = GameObject.Instantiate(this.ballPrefab, new Vector3(x, y, z), Quaternion.identity);
                obj.GetComponent<Ball>().isTrueBall = false;
            }
            
        }
    }

    public void InstantiateDamageText(float damageSec)
    {
        var obj = (GameObject)GameObject.Instantiate(Resources.Load("DamegeText"));
        obj.transform.SetParent(GameObject.Find("GameCanvas").transform,false);
        obj.GetComponent<Text>().text = "-" + damageSec.ToString("F1") + "sec";
    }
    
    public void InstantiateTakeMoneyText(float money)
    {
        var obj = (GameObject)GameObject.Instantiate(Resources.Load("TakeMoneyText"));
        obj.transform.SetParent(GameObject.Find("GameCanvas").transform,false);
        obj.GetComponent<Text>().text = "$" + money.ToString("F0");
    }
    
    public void InstantiateTakeTimeText(float takeTimeSec)
    {
        var obj = (GameObject)GameObject.Instantiate(Resources.Load("TakeTimeText"));
        obj.transform.SetParent(GameObject.Find("GameCanvas").transform,false);
        obj.GetComponent<Text>().text = "+" + takeTimeSec.ToString("F2") + "sec";
    }

    public void StartButton()
    {
        SceneManager.LoadScene("GAME");
        player = GameObject.Find("Player").GetComponent<Player>();
        PlayUI.SetActive(false);
    }

    public void GoStartScreen()
    {
        SceneManager.LoadScene("START");

    }

    public void Retry()
    {
        SceneManager.LoadScene("GAME");
        player = GameObject.Find("Player").GetComponent<Player>();
        PlayUI.SetActive(false);
    }
}
