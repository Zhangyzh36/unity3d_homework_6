using System;
using System.Collections.Generic;
using UnityEngine;

public class CCMoveToAction: SSAction {
    public Vector3 targetPosition;
    public float speed;
    public bool runningAfter;    

    public static CCMoveToAction GetSSAction(Vector3 _targetPosition, float _speed, bool _runningAfter) {
        CCMoveToAction action = ScriptableObject.CreateInstance<CCMoveToAction>();
        action.targetPosition = _targetPosition;
        action.speed = _speed;
        action.runningAfter = _runningAfter;
        return action;
    }

    public override void Start() {
        
    }

    public override void Update() {
        this.transform.position = Vector3.MoveTowards(this.transform.position, targetPosition, speed);
        if (this.transform.position == targetPosition) {
            this.destroy = true;
            if (!runningAfter)    
                this.callBack.SSActionEvent(this);
            else
                this.callBack.SSActionEvent(this, SSActionEventType.Completed, SSActiontargetPositionType.Catching);
        }
    }
}
