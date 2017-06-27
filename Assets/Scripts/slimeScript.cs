using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slimeScript:objectBase {
    static int[] walk,attack;
    float time;
    Animator animator;
    public GameObject target;
    objectBase targetScript;
    public GameObject[] attackpivot;
    public bool setAttack;
    public override void Start() {
        base.Start();
        HP = MAXHP = 10;
        setAttack = false;
        animator = GetComponent<Animator>();
        walk = new int[4];
        walk[0] = Animator.StringToHash("up");
        walk[1] = Animator.StringToHash("right");
        walk[2] = Animator.StringToHash("down");
        walk[3] = Animator.StringToHash("left");
        attack = new int[4];
        attack[0] = Animator.StringToHash("attackup");
        attack[1] = Animator.StringToHash("attackright");
        attack[2] = Animator.StringToHash("attackdown");
        attack[3] = Animator.StringToHash("attackleft");
        time = 0;
        if(target != null) {
            targetScript = target.GetComponent<objectBase>();
        }
    }
    public override void Update() {
        spriteRenderer.sortingOrder = -Y;
        transform.localPosition = new Vector3(X * once, (Y+22) * once, transform.localPosition.z);
    }
    public override void FixedUpdate() {
        inLight = false;
        Collider2D[][] groundCheckCollider = new Collider2D[1][];
        groundCheckCollider[0] = Physics2D.OverlapPointAll(pivot[vector].transform.position);
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
        int D1 =X - targetScript.X;
        int D2 =Y - targetScript.Y;
        if(!setAttack) {
            if(target == null) {

            } else {
                readyX = 0; readyY = 0;//初期化
                time += Random.Range(0.05f, 0.1f);
                if(time > 1f) {
                    int V1, V2;
                    float A = Mathf.Atan2(D2, D1);
                    A *= Mathf.Rad2Deg;
                    if(0 == Random.Range(0, 4)&&(Mathf.Abs(D1) < 120)&& (Mathf.Abs(D2)<120)) {
                        if(-45<A&&A<45) {
                            vector = 3;
                        } else if(45<A&&A<135) {
                            vector = 2;
                        } else if(-135<A&&A<-45) {
                            vector = 0;
                        } else {
                            vector = 1;
                        }
                        setAttack = true;
                    }
                    if(!setAttack) {
                        if(0 == Random.Range(0, 2) && (Mathf.Abs(D1) > 30 || Mathf.Abs(D2) > 30)) {
                            if(D1 < 0) {
                                V1 = 1;
                            } else {
                                V1 = 3;
                            }
                            if(D2 < 0) {
                                V2 = 0;
                            } else {
                                V2 = 2;
                            }
                            if(Mathf.Abs(D1) > Mathf.Abs(D2)) {
                                vector = V1;
                            } else {
                                vector = V2;
                            }




                        } else {
                            vector = Random.Range(0, 4);
                        }
                    }
                    time = 0;
                }
                if(setAttack) {
                    animator.Play(attack[vector]);
                } else {
                    animator.Play(walk[vector]);
                    SetVector(vector, Random.Range(4,7));
                    if(4 > Random.Range(0, 10)) {
                  
                    }else {
                        Move();

                    }

                }
            }
        }
    }

    public void SlimeAttack() {
        int count=0;
        foreach(Transform child in attackpivot[vector].transform) {
            //child is your child transform
            Attack(child.gameObject, 20, typeOfDamage.cross);
            count++;
        }

        Attack(pivot[vector], 20, typeOfDamage.cross);
    }
}
