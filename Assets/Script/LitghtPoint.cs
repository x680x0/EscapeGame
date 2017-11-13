using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LitghtPoint:MonoBehaviour {
    public bool LM = true;
    public GameObject next;
    public float timer=-1;
    public float Time;
    public void Switch(bool _LM) {
        LM = _LM;
    }
    public bool GetSwitch() {
        if(timer == -1) {
            Switch(false);
            timer = Time;
        } else if(timer > 0) {
            timer -= 0.1f;
        } else if(timer <= 0) {
            Switch(true);
        }
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
