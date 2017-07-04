using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spiderScript :objectBase {

    public int defY;
    static int[] attack;
    static int walk,die,slow;
    public int speed,a;
    int DX, DY;
    float time;
    public int Phase;
    Animator animator;
    public GameObject target;
    objectBase targetScript;
    public GameObject[] attackpivot;
    public bool setAttack;
    public GameObject needle;
    public override void Start() {
        base.Start();
        Phase = 0;
        HP = MAXHP = 10;
        setAttack = false;
        animator = GetComponent<Animator>();
        a = 1;
        walk = Animator.StringToHash("walk");
        slow = Animator.StringToHash("slowwalk");
        attack = new int[4];
        attack[0] = Animator.StringToHash("attackup");
        attack[1] = Animator.StringToHash("attackright");
        attack[2] = Animator.StringToHash("attackdown");
        attack[3] = Animator.StringToHash("attackleft");
        die = Animator.StringToHash("motionDie");
        time = 0;
        if(target != null) {
            targetScript = target.GetComponent<objectBase>();
        }
    }
    public override void Update() {
        spriteRenderer.sortingOrder = -Y;
        transform.localPosition = new Vector3(X * once, (Y +46) * once, transform.localPosition.z);
    }
    public override void FixedUpdate() {
        if(HP > 0) {
            inLight = false;
            Collider2D[][] groundCheckCollider = new Collider2D[1][];
            groundCheckCollider[0] = Physics2D.OverlapPointAll(pivot[vector].transform.position);
            foreach(Collider2D[] groundCheckList in groundCheckCollider) {
                foreach(Collider2D groundCheck in groundCheckList) {
                    if(groundCheck != null) {
                        if(groundCheck.isTrigger) {
                            if(groundCheck.tag == "Light") {
                                inLight = true;
                            }
                        }
                    }

                }
            }
            readyX = readyY = 0;
            switch(Phase) {
                case 0:
                    DX = X - targetScript.X;
                    DY = Y - targetScript.Y;
                    if(Mathf.Abs(DX) > Mathf.Abs(DY)) {
                        Phase = 1;
                        //Y座標が近い
                    }else {
                        Phase = 2;
                        //X座標が近い
                    }
                    break;
                case 1://Yを離してからXで軸合わせへ
                    DY = Y - targetScript.Y;
                    if(Mathf.Abs(DY) < 800&&inLight) {
                        if(DY < 0) {
                            SetVector(2, 30);
                        }else {
                            SetVector(0, 30);
                        }
                    }else {
                        Phase = 3;
                    }
                    break;
                case 2://Xを離してからYで軸合わせへ
                    DX = X - targetScript.X;
                    if(Mathf.Abs(DX) < 800 && inLight) {
                        if(DX < 0) {
                            SetVector(3, 30);
                        } else {
                            SetVector(1, 30);
                        }
                    } else {
                        Phase = 4;
                    }
                    break;
                case 3://X近づく
                    DX = X - targetScript.X;
                    if(Mathf.Abs(DX) < 30) {
                        if(DX < 0) {
                            SetVector(1, Mathf.Abs(DX));
                        } else {
                            SetVector(3, Mathf.Abs(DX));
                        }
                        DY = Y - targetScript.Y;
                        if(DY > 0) {
                            vector = 2;
                        } else {
                            vector = 0;
                        }
                        Phase = 5;
                    } else if(Mathf.Abs(DX) > 10) {
                        if(DX < 0) {
                            SetVector(1, 30);
                        } else {
                            SetVector(3, 30);
                        }
                    }
                    break;
                case 4:
                    DY = Y - targetScript.Y;
                    if(Mathf.Abs(DY) < 30) {
                        if(DY < 0) {
                            SetVector(0, Mathf.Abs(DY));
                        } else {
                            SetVector(2, Mathf.Abs(DY));
                        }
                        DX = X - targetScript.X;
                        if(DX > 0) {
                            vector = 3;
                        } else {
                            vector = 1;
                        }
                        Phase = 5;
                    } else if(Mathf.Abs(DY) > 10) {
                        if(DY < 0) {
                            SetVector(0, 30);
                        } else {
                            SetVector(2, 30);
                        }
                    }
                    break;
                case 5://攻撃
                    animator.Play(attack[vector]);
                    Phase = 6;
                    break;
                case 6:
                    time = 0;
                    break;
                case 7:
                    DX = X - targetScript.X;
                    DY = Y - targetScript.Y;
                    if(DX < 0) {
                        SetVector(3, 2);
                    } else {
                        SetVector(1, 2);
                    }
                    if(DY < 0) {
                        SetVector(2, 2);
                    } else {
                        SetVector(0, 2);
                    }
                    time += 1.0f;
                    vector = Random.Range(0, 4);
                    if(time > 180) {
                        time = 0;
                        Phase = 0;
                        animator.Play(walk);
                    }
                    break;
            }
            Move();
            //移動
           
        }
        
    }
    public void AttackFinish() {
        animator.Play(slow);
        Phase = 7;
    }
    public void SpiderAttack() {
        GameObject _needle = Instantiate(needle);
        _needle.GetComponent<bulletBase>().Fire(X, Y, vector, 30.0f, typeOfDamage.mid, 10, this.gameObject);
    }
}
