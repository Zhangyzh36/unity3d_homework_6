using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameStatusText : MonoBehaviour {
    private int score = 0;
    private int status;  //0为score，1为gameover

	void Start () {
       if (gameObject.name.Contains("Score"))
            status = 0;
        else
            status = 1;
	}
	
	void Update () {
		
	}

    void OnEnable() {
        GameEventManager.zyzScoreAction += gameScore;
        GameEventManager.zyzOverAction += gameOver;
    }

    void OnDisable() {
        GameEventManager.zyzScoreAction -= gameScore;
        GameEventManager.zyzOverAction -= gameOver;
    }

    void gameScore() {
        if (status == 0) {
            score++;
            this.gameObject.GetComponent<Text>().text = "Score: " + score;
        }
    } 

    void gameOver() {
        if (status == 1)
            this.gameObject.GetComponent<Text>().text = "You lose!";
    }
}
