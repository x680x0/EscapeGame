using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : EnemyScript {
    static int walk;
    static int slow;
    static int[] attack;
    static int die;
    float[] DamageTimer;
    Animator animator;
    Vector2 Speed;
    BoxCollider2D bc2d;
     bool AttackNow;
    int a;
    float Slow;
    public GameObject Needle;
    float r = 10;
    int Phase;
    // Use this for initialization
    public override void Start () {
        base.Start();
        DamageTimer = new float[4];
        Slow = 0;
        Phase = 0;
        AttackNow = false;
        Speed = Vector2.zero;
        HP = 20;
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        bc2d = GetComponent<BoxCollider2D>();
        walk = Animator.StringToHash("walk");
        slow = Animator.StringToHash("slowwalk");
        attack = new int[4];
        attack[0] = Animator.StringToHash("attackup");
        attack[1] = Animator.StringToHash("attackright");
        attack[2] = Animator.StringToHash("attackdown");
        attack[3] = Animator.StringToHash("attackleft");
        die = Animator.StringToHash("motionDie");
        ReTarget();
        muki = 0;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    public override void FixedUpdate() {
        float tx, ty, x, y, dx, dy;
        a = muki;
        SetOrder(15);
        if(Target != null) {
            float DX, DY;
            tx = Target.transform.position.x;
            ty = Target.transform.position.y;
            x = transform.position.x;
            y = transform.position.y;
            switch(Phase) {
                case 0:
                    DX = x - tx;
                    DY = y - ty;
                    if(Mathf.Abs(DX) > Mathf.Abs(DY)) {
                        Phase = 1;
                        //Y座標が近い
                    } else {
                        Phase = 2;
                        //X座標が近い
                    }
                    break;
                case 1:
                    DY = y - ty;
                    if(Mathf.Abs(DY) < 10 && inLight) {
                        if(DY < 0) {
                            Speed.Set(0, -30);
                        } else {
                            Speed.Set(0, +30);
                        }
                    } else {
                        Phase = 3;
                    }
                    break;
                case 2:
                    DX = x - tx;
                    if(Mathf.Abs(DX) < 10 && inLight) {
                        if(DX < 0) {
                            Speed.Set(-30, 0);
                        } else {
                            Speed.Set(30, 0);
                        }
                    } else {
                        Phase = 4;
                    }
                    break;
                case 3://X近づく
                    DX = x - tx;
                    if(Mathf.Abs(DX) < 0.5) {
                        if(DX < 0) {
                            Speed.Set(Mathf.Abs(DX), 0);
                        } else {
                            Speed.Set(-Mathf.Abs(DX), 0);
                        }
                        DY = y - ty;
                        if(DY > 0) {
                            muki = 2;
                        } else {
                            muki = 0;
                        }
                        Phase = 5;
                    } else if(Mathf.Abs(DX) > 0.01) {
                        if(DX < 0) {
                            Speed.Set(30, 0);
                        } else {
                            Speed.Set(-30, 0);
                        }
                    }
                    break;
                case 4:
                    DY = y - ty;
                    if(Mathf.Abs(DY) < 0.5) {
                        if(DY < 0) {
                            Speed.Set(0, Mathf.Abs(DY));
                        } else {
                            Speed.Set(0, -Mathf.Abs(DY));
                        }
                        DX = x - tx;
                        if(DX > 0) {
                            muki = 3;
                        } else {
                            muki = 1;
                        }
                        Phase = 5;
                    } else if(Mathf.Abs(DY) > 0.01) {
                        if(DY < 0) {
                            Speed.Set(0, 30);
                        } else {
                            Speed.Set(0, -30);
                        }
                    }
                    break;
                case 5:
                    Speed = Vector2.zero;
                    animator.Play(attack[(muki)]);
                    rb2d.velocity = Speed;
                    AttackNow = true;
                    Phase = 6;
                    break;
                case 6:
                    ReTarget();
                    Slow = 0;
                    Phase =7;
                    break;
                case 7:
                    DX = x - tx;
                    DY = y - ty;
                    Slow += 1.0f;
                   // muki = Random.Range(0, 4);
                    if(Slow > 180) {
                        Slow = 0;
                        if(HP > 0) {
                            Phase = 0;
                        }else {
                            Phase = 8;
                        }
                        animator.Play(walk);
                    }
                    break;
                case 8:
                    Speed = Vector2.zero;
                    break;
            }
            if(!AttackNow) {
               
                rb2d.velocity = Speed;
            }

        }
            Collider2D[][] CheckCollider = new Collider2D[1][];
            CheckCollider[0] = Physics2D.OverlapPointAll(pivot[muki].transform.position);
        inLight = false;
        foreach(Collider2D[] CheckList in CheckCollider) {

            foreach(Collider2D groundCheck in CheckList) {
                if(groundCheck != null) {
                    if(groundCheck.isTrigger) {
                        if(groundCheck.tag == "PlayerAttack") {
                            int c = groundCheck.gameObject.transform.parent.GetComponent<CNum>().GetContlol();
                            Damaged(groundCheck.gameObject, c);
                        }
                        if(groundCheck.tag == "PlayArea") {
                            inLight = true;
                        }
                    }
                }
            }

        }
        int i = 0;
        for(i = 0; i < 4; i++) {
            if(DamageTimer[i] <= 0) {
                DamageTimer[i] = 0;
            } else {
                DamageTimer[i] -= 1f;
            }
        }
        
    }

    void Shot() {
        SpiderNeedle sn = Instantiate(Needle, transform.position, transform.localRotation).GetComponent<SpiderNeedle>();
        sn.ini(muki);
    }
    void AttackFinish() {
        AttackNow = false;
        animator.Play(slow);
    }

    public override void Damaged(GameObject obj, int num) {
        if(DamageTimer[num] <= 0) {
            DamageInf dmi = obj.GetComponent<DamageInf>();
            int damage;
            if(dmi != null) {
                damage = dmi.GetDamage();
                if(HP <= damage) {
                    animator.Play(die);
                    Phase = 8;
                    bc2d.enabled = false;
                    HP = 0;
                } else {
                    HP -= damage;
                    DamageTimer[num] = 1.5f;
                }
            }
        }
    }
}
