using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : SingletonMonoBehaviour<GameManager>
{
    public Color currentColor;
    public GameObject ballPrefab;
    public GameObject result;
    //現在のゲームの難易度
    public int Level;

    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        Setting1stGame();
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
        Level++;
        var balls = GameObject.FindGameObjectsWithTag("Ball");
        foreach (var ball in balls)
        {
            Destroy(ball);
        }

        CurrentColorSet();
        InstantiateTrueBall();
        InstantiateFalseBall(Level);
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
        int rand = Random.Range(1,5);
        Debug.Log(rand);
        switch (rand)
        {
            case 1:
                currentColor = Color.white;
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
        player.GetComponent<SpriteRenderer>().color = currentColor;
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
}
