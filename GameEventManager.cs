using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Com.Patrols;

//实现发布订阅模式
public class GameEventManager : MonoBehaviour {
    public delegate void ScoreAction();
    public static event ScoreAction zyzScoreAction;

    public delegate void OverAction();
    public static event OverAction zyzOverAction;

    private SceneController scene;

    void Start () {
        scene = SceneController.getInstance();
        scene.setGameEventManager(this);
    }
	
	void Update () {
		
	}
	
	//玩家逃离追捕并得分
    public void playerScore() {
        if (zyzScoreAction != null)
            zyzScoreAction();
    }
	//巡逻兵追到玩家，游戏结束
    public void gameOver() {
        if (zyzOverAction != null)
            zyzOverAction();
    }
}
