using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Com.Patrols;

public class UserInterface : MonoBehaviour {
    private IUserAction action;

    void Start () {
        action = SceneController.getInstance() as IUserAction;
    }
	
	void Update () {
        detectKeyInput();
    }

    void detectKeyInput() {
		//检测玩家的键盘输入，上下左右的移动
        if (Input.GetKey(KeyCode.UpArrow)) {
            action.movePlayer(Diretion.UP);
        }
        if (Input.GetKey(KeyCode.DownArrow)) {
            action.movePlayer(Diretion.DOWN);
        }
        if (Input.GetKey(KeyCode.LeftArrow)) {
            action.movePlayer(Diretion.LEFT);
        }
        if (Input.GetKey(KeyCode.RightArrow)) {
            action.movePlayer(Diretion.RIGHT);
        }
    }
}
