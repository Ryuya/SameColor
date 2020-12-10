using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UpdateCard : MonoBehaviour
{
    GameManager gameManager;
    int placeValue;
    public Text placeText;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        
    }

    // Update is called once per frame
    void Update()
    {
        placeText = transform.GetChild(1).GetChild(0).GetComponent<Text>();
        Debug.Log(placeText);
        int placeValue = gameManager.buyCount * gameManager.buyCount * 100;
        placeText.text = placeValue.ToString();
        transform.GetChild(1).GetComponent<Place>().place = placeValue;
    }
}
