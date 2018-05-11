using System;
using System.Collections.Generic;
using UnityEngine;

public class CCSequeneActions : SSAction, ISSActionCallback {
    public List<SSAction> sequence;
    public int repeat = -1;           
    public int start = 0;         

    public static CCSequeneActions GetSSAction(List<SSAction> _sequence, int _repeat = 0) {
        CCSequeneActions action = ScriptableObject.CreateInstance<CCSequeneActions>();
        action.repeat = _repeat;
        action.sequence = _sequence;
        return action;
    }

    public override void Start() {
        foreach (SSAction action in sequence) {
            action.gameObject = this.gameObject;
            action.transform = this.transform;
            action.callBack = this;
            action.Start();
        }
    }

    public override void Update() {
        if (sequence.Count == 0)
            return;
        else if (start < sequence.Count) {
            sequence[start].Update();
        }
    }

    public void SSActionEvent(SSAction source, 
        SSActionEventType eventType = SSActionEventType.Completed,
        SSActiontargetPositionType intParam = SSActiontargetPositionType.Normal, string strParam = null, object objParam = null) {

        source.destroy = false;
        this.start++;
        if (this.start >= sequence.Count) {
            this.start = 0;
            if (repeat > 0)
                repeat--;
            if (repeat == 0) {
                this.destroy = true;
                this.callBack.SSActionEvent(this);
            }
        }
    }

    void OnDestroy() {

    }
}
