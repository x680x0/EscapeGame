using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerScript:objectBase {
    public int x, y;
    Animator animator;
    static int[] walk;
    static int[] stay;
    public bool itemTest;
    GameObject pick;
    public GameObject healBottle;
    // Use this for initialization
    public override void Start() {
        base.Start();
        x = 0;
        y = 0;
        vector = 0;
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
        itemTest = false;
        pick = null;
    }

    // Update is called once per frame
    public override void Update() {
        base.Update();
    }
    public override void FixedUpdate() {
        pick = null;
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
                        else if(groundCheck.tag == "Item") {
                            pick=groundCheck.gameObject;
                        }
                    }
                }

            }
        }

        readyX = 0; readyY = 0;//初期化

        if(MGR.input[vector] > 0) {
            stop = false;
            SetVector(vector,10 );
            if(MGR.input[(vector + 1) % 4] > 0) {
                SetVector((vector + 1) % 4,10);
            }
            if(MGR.input[(vector + 3) % 4] > 0) {
                SetVector((vector + 3) % 4,10);
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
        if(stop) {
            animator.Play(stay[vector]);
        } else {
            animator.Play(walk[vector]);
        }
        Move();
        //アイテム投げテスト+拾いテスト
        if(MGR.input[4] == 1) {
            if(itemTest) {
                itemTest = false;
                itemBase a = Instantiate(healBottle).GetComponent<itemBase>();
                if(a != null) {
                    a.SetPosition(X, Y);
                    a.Through(vector);
                }
            } else if(pick != null) {
                Destroy(pick);
                itemTest = true;
            }
        }  
    }
}
