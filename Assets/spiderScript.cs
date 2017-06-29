using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spiderScript :objectBase {

    static int[] attack;
    static int walk,die;
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
        transform.localPosition = new Vector3(X * once, (Y ) * once, transform.localPosition.z);
    }
    public override void FixedUpdate() {
        if(HP > 0) {
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
        }
    }
}
