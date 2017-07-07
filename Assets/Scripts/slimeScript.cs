using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slimeScript:objectBase {
    static int[] walk, attack;
    static int die;
    float time;
    Animator animator;
    public GameObject target;
    objectBase targetScript;
    public GameObject[] attackpivot;
    public GameObject tourch;
    objectBase tourchS;
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
        die = Animator.StringToHash("motionDie");
        time = 0;
        if(tourch != null) {
            tourchS = tourch.GetComponent<objectBase>();
        }
    }
    public override void Update() {
        spriteRenderer.sortingOrder = -Y;
        transform.localPosition = new Vector3(X * once, (Y + 22) * once, transform.localPosition.z);
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

            readyX = 0; readyY = 0;//初期化
            int DX;
            int DY;
            if(!setAttack) {


                if(target == null) {
                    
                    DX = X - tourchS.X;
                    DY = Y - tourchS.Y;
                    if(DX > 0) {
                        SetVector(3, 10);
                    } else {
                        SetVector(1, 10);
                    }
                    if(DY > 0) {
                        SetVector(2, 10);
                    } else {
                        SetVector(0, 10);
                    }
                    Move();
                    if(inLight) {
                        target = MGR.player[0].gameObject;
                        if(target != null) {
                            targetScript = target.GetComponent<objectBase>();
                        }
                    }
                } else {

                    DX = X - targetScript.X;
                    DY = Y - targetScript.Y;

                    time += Random.Range(0.05f, 0.1f);
                    if(time > 1f) {
                        int V1, V2;
                        float A = Mathf.Atan2(DY, DX);
                        A *= Mathf.Rad2Deg;
                        if(0 == Random.Range(0, 4) && (Mathf.Abs(DX) < 120) && (Mathf.Abs(DY) < 120)) {
                            if(-45 < A && A < 45) {
                                vector = 3;
                            } else if(45 < A && A < 135) {
                                vector = 2;
                            } else if(-135 < A && A < -45) {
                                vector = 0;
                            } else {
                                vector = 1;
                            }
                            setAttack = true;
                        }
                        if(!setAttack) {
                            if(0 == Random.Range(0, 2) && (Mathf.Abs(DX) > 30 || Mathf.Abs(DY) > 30)) {
                                if(DX < 0) {
                                    V1 = 1;
                                } else {
                                    V1 = 3;
                                }
                                if(DY < 0) {
                                    V2 = 0;
                                } else {
                                    V2 = 2;
                                }
                                if(Mathf.Abs(DX) > Mathf.Abs(DY)) {
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
                        SetVector(vector, Random.Range(4, 7));
                        if(4 > Random.Range(0, 10)) {

                        } else {
                            Move();

                        }

                    }
                }
            }
        }
    }

    public void SlimeAttack() {
        int count = 0;
        foreach(Transform child in attackpivot[vector].transform) {
            //child is your child transform
            Attack(child.gameObject, 20, typeOfDamage.cross,this.gameObject.tag);
            count++;
        }

        Attack(pivot[vector], 20, typeOfDamage.cross, this.gameObject.tag);
    }

    public override void Damaged(int damage, typeOfDamage type, int _X, int _Y, GameObject Attacker,string tag) {

        // 
        if(this.gameObject == Attacker||this.gameObject.tag==tag) {


        } else {
            HP -= damage;
            if(HP <= 0) {
                animator.Play(die);
            }
        }

    }
    public override void SetTourch(GameObject _tourch) {
        tourch = _tourch;
    }
}
