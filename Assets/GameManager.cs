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
    Play,
    Result,
}

public class GameManager : SingletonMonoBehaviour<GameManager>
{
    public Color currentColor;
    public GameObject ballPrefab;
    public GameObject result;
    //現在のゲームの難易度
    public int level;
    public Text levelText;
    public float time = 30f;
    [FormerlySerializedAs("player")] public GameObject playerObj;
    public Player player;
    public Text timeText;
    public Text homeLevelText;
    public bool isShowResultCanvas = false;
    //ゲームの状態
    public GameState _gameState;
    public GameObject resultCanvas;
    public GameObject shopPanel;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
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
        InstantiateTrueBall();
    }

    public void SettingStageStart(int Level)
    {
        InstantiateFalseBall(Level);
    }

    public void NextStage()
    {
        level++;
        
        var balls = GameObject.FindGameObjectsWithTag("Ball");
        foreach (var ball in balls)
        {
            Destroy(ball);
        }
        time += (level + 5);
        InstantiateTakeTimeText((level + 5));
        timeText.text = time.ToString("F2");
        CurrentColorSet();
        InstantiateTrueBall();
        InstantiateFalseBall(level);
        _gameState = GameState.Wait;
    }

    public void GameOver()
    {
        ShowResult();
        Debug.Log("GameOver");
    }

    public void ShowResult()
    {
        //result.SetActive(true);
    }
    public void CurrentColorSet()
    {
        int rand = Random.Range(1,9);
        Debug.Log(rand);
        switch (rand)
        {
            case 1:
                currentColor = Color.gray;
                break;
            case 2:
                currentColor = Color.blue;
                break;
            case 3:
                
                currentColor = Color.yellow;
                break;
            case 4:
                currentColor = Color.red;
                break;
            case 5:
                currentColor = Color.black;
                break;
            case 6:
                currentColor = Color.green;
                break;
            case 7:
                currentColor = Color.magenta;
                break;
            
            case 8:
                currentColor = Color.cyan;
                break;
        }
    }
    
    //正解のぼーるをだす
    public void InstantiateTrueBall()
    {
        var x = Random.Range(-8f,9.43f);
        var y = Random.Range(4.96f,5.9f);
        var z = 0f;
        var obj = GameObject.Instantiate( this.ballPrefab,new Vector3(x,y,z),Quaternion.identity);
        
        obj.GetComponent<SpriteRenderer>().color = currentColor;
        obj.GetComponent<Ball>()._color = currentColor;
        obj.GetComponent<Ball>().isTrueBall = true;
        //プレイヤーの色を変える
        playerObj.GetComponent<SpriteRenderer>().color = currentColor;
    }
    
    public void InstantiateFalseBall(int Level)
    {
        for (int i = 0; i < Level * 3; i++)
        {
            var x = Random.Range(-8f,9.43f);
            var y = Random.Range(4.96f,5.9f);
            var z = 0f;
            var obj = GameObject.Instantiate( this.ballPrefab,new Vector3(x,y,z),Quaternion.identity);
            obj.GetComponent<Ball>().isTrueBall = false;
        }
    }

    public void InstantiateDamageText(float damageSec)
    {
        var obj = (GameObject)GameObject.Instantiate(Resources.Load("DamegeText"));
        obj.transform.SetParent(GameObject.Find("Canvas").transform,false);
        //obj.transform.position = GameObject.Find("Canvas").transform.position;
        obj.GetComponent<Text>().text = "-" + damageSec.ToString() + "sec";
    }
    
    public void InstantiateTakeTimeText(float takeTimeSec)
    {
        var obj = (GameObject)GameObject.Instantiate(Resources.Load("TakeTimeText"));
        obj.transform.SetParent(GameObject.Find("Canvas").transform,false);
        //obj.transform.position = GameObject.Find("Canvas").transform.position;
        obj.GetComponent<Text>().text = "+" + takeTimeSec.ToString() + "sec";
    }

    public void Retry()
    {
        SceneManager.LoadScene("GAME");
    }
}
