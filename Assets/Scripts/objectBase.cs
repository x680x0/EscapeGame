using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objectBase: MonoBehaviour {
    [System.NonSerialized]
    public SpriteRenderer spriteRenderer;
    [System.NonSerialized]
    public float once;
    public int X, Y;
    public int MAXHP, HP;
    [System.NonSerialized]
    public int readyX,readyY;
    public GameObject pivot;//光に入っているか判定する座標
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
    }
	
	// Update is called once per frame
	public virtual void Update () {
        spriteRenderer.sortingOrder = Y;
        transform.localPosition = new Vector3(X *once, Y * once, 0);
    }
    public virtual void FixedUpdate() {
        inLight = false;
        Collider2D[][] groundCheckCollider = new Collider2D[1][];
        groundCheckCollider[0] = Physics2D.OverlapPointAll(pivot.transform.position);
        foreach(Collider2D[] groundCheckList in groundCheckCollider) {
            foreach(Collider2D groundCheck in groundCheckList) {
                if(groundCheck != null) {
                    if(!groundCheck.isTrigger) {
                        if(groundCheck.tag == "Light") {
                           inLight = true;
                        }
                    }
                }

            }
        }
    }

    public virtual void Damaged(int damage,typeOfDamage type) {
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
    }
}
