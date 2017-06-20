using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class object_base: MonoBehaviour {
    public float once;
    int fx, fy;
    public int x, y;
    SpriteRenderer spriteRenderer;
    MGRscript MGR;//入力や全体を通しての初期値などはここから
    public bool light;//光の当たる範囲
    public GameObject pivot;//光に入っているか判定する座標
    public int vector;//上から時計回りに0123
    public  bool stop;
    public Sprite[] arrow;
	// Use this for initialization
	void Start () {
        x =0;
        y = 0;
        light = false;
        spriteRenderer= GetComponent<SpriteRenderer>();
        MGR = GameObject.Find("MGRobject").GetComponent<MGRscript>();
        vector = 0;
        stop = true;
        once = MGR.once;
    }
	
	// Update is called once per frame
	void Update () {
        transform.localPosition = new Vector3(x *once, y * once, 0);
    }
    void FixedUpdate() {
        light = false;
        Collider2D[][] groundCheckCollider = new Collider2D[1][];
        groundCheckCollider[0] = Physics2D.OverlapPointAll(pivot.transform.position);
        foreach(Collider2D[] groundCheckList in groundCheckCollider) {
            foreach(Collider2D groundCheck in groundCheckList) {
                if(groundCheck != null) {
                    if(!groundCheck.isTrigger) {
                        if(groundCheck.tag == "Light") {
                            light = true; 
                        }
                    }
                }

            }
        }
        fx = 0;fy = 0;//初期化
        if(MGR.input[vector]>0) {
            stop = false;
            SetMove(vector);
            if(MGR.input[(vector + 1) % 4] > 0) {
                SetMove((vector + 1) % 4);
            }
            if(MGR.input[(vector + 3) % 4] > 0) {
                SetMove((vector + 3) % 4);
            }
        } else {
            stop = true;
            for(int i = 0; i < 4; i++) {
                if(MGR.input[i] > 0) {
                    vector = i;
                    stop = false;
                }
            }
            spriteRenderer.sprite = arrow[vector];
        }
        Move();
    }

    void Move() {//実際に動かす　SetMoveで事前にセットしておくこと
        if(fx * fy == 0) {
            x += fx;
            y += fy;
        }else {
            x += (int)(fx/1.4);
            y += (int)(fy/1.4);
        }
    }
    void SetMove(int _vector) {//これでSetしてMoveで実際に動かす。
        switch(_vector) {
            case 0:
                fy += 10;
                break;
            case 1:
                fx += 10;
                break;
            case 2:
                fy -= 10;
                break;
            case 3:
                fx -=10;
                break;
        }
    }
}
