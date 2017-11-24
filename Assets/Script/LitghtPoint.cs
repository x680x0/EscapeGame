using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LitghtPoint:MonoBehaviour {
    public bool LM = true;
    public GameObject next;
    public GameObject EnemyPack;
    PlayerMGR PMGR;
    public float timer=-1;
    public float Time;
    void Start() {
        PMGR = GameObject.Find("PlayerMGR").GetComponent<PlayerMGR>();
    }
    public void Switch(bool _LM) {
        LM = _LM;
    }
    public bool GetSwitch() {
        if(EnemyPack == null) {
            timer = 0;
        }
        if(timer == -1) {
            PMGR.ReBirth();
            Switch(false);
            if(EnemyPack != null) {
                EnemyPack.SetActive(true);
            }
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
