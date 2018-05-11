using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Com.Patrols;

//这个脚本挂在巡逻兵身上，用于移动、以及判断游戏状态
public class PatrolBehaviour : MonoBehaviour {
    private IAddAction addAction;
    private IGameStatus gameStatusOp;

    public int whichPatrol;
    public bool runningAfter;    

    private float CHASING_RANGE = 3.0f;

    void Start () {
        addAction = SceneController.getInstance() as IAddAction;
        gameStatusOp = SceneController.getInstance() as IGameStatus;

        whichPatrol = getwhichPatrol();
        runningAfter = false;
    }
	
	void Update () {
		//检测玩家是否走进自己巡逻的区域
       if (gameStatusOp.getPlayerArea() == whichPatrol) {    
	        //如果没有在追玩家，就去追玩家
            if (!runningAfter) {
                runningAfter = true;
                addAction.moveToPlayer(this.gameObject);
            }
        }
        else {
			//玩家走出自己巡逻的区域，停止追逐
            if (runningAfter) {    
                gameStatusOp.playerScore();
                runningAfter = false;
				//继续巡逻
                addAction.moveRandomly(this.gameObject, false);
            }
        }
    
	}
	
	//根据巡逻兵的名字确定其编号
    int getwhichPatrol() {
        string name = this.gameObject.name;
        return name[name.Length - 1] - '0';
    }
        
	//碰撞检测
    void OnCollisionStay(Collision e) {
        //撞击围栏，转动方向
        if (e.gameObject.name.Contains("Patrol")
            || e.gameObject.tag.Contains("FenceAround")) {
            runningAfter = false;
            addAction.moveRandomly(this.gameObject, false);
        }

        //撞击玩家，游戏结束
        if (e.gameObject.name.Contains("Player")) {
            gameStatusOp.gameOver();
        }
    }
}
