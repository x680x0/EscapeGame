﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerScript:objectBase {
    [System.NonSerialized]
    Animator animator;
    static int[] walk;
    static int[] stay;
    static int[] attack;
    static int[] damaged;
    public bool[] itemTest;
    GameObject pick;
    public GameObject healBottle;
    public bool isAttack,isDamaged;
    int knockX, knockY,knockCount;
    // Use this for initialization
    public override void Start() {
        base.Start();
        itemTest = new bool[2];
        vector = 0;
        isAttack = false;
        isDamaged = false;
        knockCount = 0;
        animator = GetComponent<Animator>();
        walk = new int[4];
        walk[0] = Animator.StringToHash("up");
        walk[1] = Animator.StringToHash("right");
        walk[2] = Animator.StringToHash("down");
        walk[3] = Animator.StringToHash("left");
        stay = new int[4];
        stay[0] = Animator.StringToHash("upstop");
        stay[1] = Animator.StringToHash("rightstop");
        stay[2] = Animator.StringToHash("downstop");
        stay[3] = Animator.StringToHash("leftstop");
        attack = new int[4];
        attack[0] = Animator.StringToHash("attackup");
        attack[1] = Animator.StringToHash("attackright");
        attack[2] = Animator.StringToHash("attackdown");
        attack[3] = Animator.StringToHash("attackleft");
        damaged = new int[4];
        damaged[0] = Animator.StringToHash("damagedup");
        damaged[1] = Animator.StringToHash("damagedright");
        damaged[2] = Animator.StringToHash("damageddown");
        damaged[3] = Animator.StringToHash("damagedleft");
        itemTest[0] = false;
        itemTest[1] = false;
        pick = null;
    }
    public override void Damaged(int damage, typeOfDamage type, int _X, int _Y) {
        if(!isDamaged) {
            animator.Play(damaged[vector]);
            int dX = X - _X;
            int dY = Y - _Y;
            float knockScale = 10000.0f / (dX * dX + dY * dY);
            knockScale = Mathf.Sqrt(knockScale);
            knockX =(int)(knockScale*dX);
            knockY = (int)(knockScale * dY);

            knockCount = 30;
            isDamaged = true;
        }
    }
    // Update is called once per frame
    public override void Update() {
        base.Update();
    }
    public override void FixedUpdate() {
        pick = null;
        inLight = false;
        Collider2D[][] groundCheckCollider = new Collider2D[1][];
        groundCheckCollider[0] = Physics2D.OverlapPointAll(pivot[vector].transform.position);
        foreach(Collider2D[] groundCheckList in groundCheckCollider) {
            foreach(Collider2D groundCheck in groundCheckList) {
                if(groundCheck != null) {
                    if(!groundCheck.isTrigger) {
                        if(groundCheck.tag == "Light") {
                            inLight = true;
                        } else if(groundCheck.tag == "Item") {
                            pick = groundCheck.gameObject;
                        }
                    }
                }

            }
        }
        if(isDamaged) {
            X += 2*knockX / 3;
            Y += 2*knockY / 3;
            knockX /= 3;
            knockY /= 3;
            knockCount -= 1;
            if(knockCount <= 0) {
                isDamaged = false;
            }
        } else if(!isAttack) {
            readyX = 0; readyY = 0;//初期化
            if(MGR.input[(int)mgrScript.keyUse.vectorlock] > 0) {
                stop = true;
                for(int i = 0; i < 4; i++) {
                    if(MGR.input[i] > 0) {
                        stop = false;
                        SetVector(i, 10);
                    }
                }
            } else {
                if(MGR.input[vector] > 0) {
                    stop = false;
                    SetVector(vector, 10);
                    if(MGR.input[(vector + 1) % 4] > 0) {
                        SetVector((vector + 1) % 4, 10);
                    }
                    if(MGR.input[(vector + 3) % 4] > 0) {
                        SetVector((vector + 3) % 4, 10);
                    }
                } else {
                    stop = true;
                    for(int i = 0; i < 4; i++) {
                        if(MGR.input[i] > 0) {
                            vector = i;
                            stop = false;
                        }
                    }

                }
            }
            if(stop) {
                animator.Play(stay[vector]);
            } else {
                animator.Play(walk[vector]);
            }
            Move();
            //アイテム投げテスト+拾いテスト
            if(MGR.input[(int)mgrScript.keyUse.weapon] == 1) {
                isAttack = true;
                animator.Play(attack[vector]);
            } else
            if(MGR.input[(int)mgrScript.keyUse.item1] == 1) {
                if(itemTest[0]) {
                    itemTest[0] = false;
                    itemBase a = Instantiate(healBottle).GetComponent<itemBase>();
                    if(a != null) {
                        a.Through(vector, this.gameObject, 20, X, Y);
                    }
                } else if(pick != null) {
                    Destroy(pick);
                    itemTest[0] = true;
                }
            } else
            if(MGR.input[(int)mgrScript.keyUse.item2] == 1) {
                if(itemTest[1]) {
                    itemTest[1] = false;
                    itemBase a = Instantiate(healBottle).GetComponent<itemBase>();
                    if(a != null) {
                        a.Through(vector, this.gameObject, 20, X, Y);
                    }
                } else if(pick != null) {
                    Destroy(pick);
                    itemTest[1] = true;
                }
            }
        }
    }
}
