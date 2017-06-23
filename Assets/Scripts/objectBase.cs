﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objectBase: MonoBehaviour {
    [System.NonSerialized]
    public SpriteRenderer spriteRenderer;
   // [System.NonSerialized]
    public float once;
    public int X, Y;
    public int MAXHP, HP;
    [System.NonSerialized]
    public int readyX,readyY;
    public GameObject[] pivot;//光に入っているか判定する座標
    [System.NonSerialized]
    public mgrScript MGR;//入力や全体を通しての初期値などはここから
    public bool inLight;//光の当たる範囲
    public int vector;//上から時計回りに0123
    public  bool stop;
    public enum typeOfDamage {
        cross=0,
        mid
    }
	// Use this for initialization
	public virtual void Start () {
        inLight = false;
        MGR = GameObject.Find("mgrObject").GetComponent<mgrScript>();
        stop = true;
        once = MGR.once;
        spriteRenderer = GetComponent<SpriteRenderer>();
        X = (int)(transform.localPosition.x / once);
        Y = (int)(transform.localPosition.y / once);
    }
	
	// Update is called once per frame
	public virtual void Update () {
        spriteRenderer.sortingOrder = -Y;
        transform.localPosition = new Vector3(X *once, Y * once, transform.localPosition.z);
    }
    public virtual void FixedUpdate() {
       
    }

    public virtual void Damaged(int damage,typeOfDamage type) {
        Destroy(this.gameObject);
        HP -= damage;
        if(HP < 0) {
            Death();
        }
    }

   public virtual void Move() {//実際に動かす　SetMoveで事前にセットしておくこと
        if(readyX * readyY == 0) {
            X += readyX;
            Y += readyY;
        }else {
            X += (int)(readyX/1.4);
            Y += (int)(readyY/1.4);
        }
    }
    public virtual void SetVector(int _vector,int speed) {//これでSetしてMoveで実際に動かす。
        switch(_vector) {
            case 0:
                readyY +=speed;
                break;
            case 1:
                readyX += speed;
                break;
            case 2:
                readyY -= speed;
                break;
            case 3:
                readyX -=speed;
                break;
        }
    }
    public virtual void Death() { }
    public virtual void SetPosition(int _X,int _Y) {
       
        X = _X;
        Y = _Y;
        transform.localPosition = new Vector3(X * once, Y * once, transform.localPosition.z);

    }
}
