﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public enum SSActionEventType: int { Started, Completed }
public enum SSActiontargetPositionType : int { Normal, Catching }    

public interface ISSActionCallback {
    void SSActionEvent(SSAction source,
        SSActionEventType eventType = SSActionEventType.Completed,
        SSActiontargetPositionType intParam = SSActiontargetPositionType.Normal,     
        string strParam = null,
        Object objParam = null);
}
