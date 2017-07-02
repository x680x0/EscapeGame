﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spiderScript :objectBase {

    public int defY;
    static int[] attack;
    static int walk,die;
    public int speed,a;
    float time;
    Animator animator;
    public GameObject target;
    objectBase targetScript;
    public GameObject[] attackpivot;
    public bool setAttack;
    public GameObject needle;
    public override void Start() {
        base.Start();
        HP = MAXHP = 10;
        setAttack = false;
        animator = GetComponent<Animator>();
        a = 1;
        walk = Animator.StringToHash("walk");
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
        }
        
    }
    public void SpiderAttack() {
        GameObject _needle = Instantiate(needle);
        _needle.GetComponent<bulletBase>().Fire(X, Y, vector, 30.0f, typeOfDamage.mid, 10, this.gameObject);
    }
}
