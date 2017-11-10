using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LitghtPoint:MonoBehaviour {
    public bool LM = true;
    public GameObject next;
    public void Switch(bool _LM) {
        LM = _LM;
    }
    bool GetSwitch() {
        return LM;
    }
    public Vector2 ToNext() {
        if(next == null) {
            return Vector2.zero;
        } else {
            return -(Vector2)(transform.position - next.transform.position).normalized;
        }
    }
}
