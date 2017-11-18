using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : EnemyScript {
    static int walk;
    static int slow;
    static int[] attack;
    static int die;
    Animator animator;
    Vector2 Speed;
    float A = 0;
    public bool AttackNow;
    public int a;
    public float Slow;
    public GameObject Needle;
   public  float r = 10;
    
    // Use this for initialization
    public override void Start () {
        base.Start();
        Slow = 0;
         AttackNow = false;
        Speed = Vector2.zero;
        HP = 20;
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        walk = Animator.StringToHash("walk");
        slow = Animator.StringToHash("slowwalk");
        attack = new int[4];
        attack[0] = Animator.StringToHash("attackup");
        attack[1] = Animator.StringToHash("attackright");
        attack[2] = Animator.StringToHash("attackdown");
        attack[3] = Animator.StringToHash("attackleft");
        die = Animator.StringToHash("motionDie");
        muki = 0;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    public override void FixedUpdate() {
        float tx, ty, x, y, dx, dy;
        a = muki;
        if(Target != null) {
            tx = Target.transform.position.x;
            ty = Target.transform.position.y;
            x = transform.position.x;
            y = transform.position.y;
            if(Slow > 0) {
                Slow -= 0.1f;
               // rb2d.velocity
            }else
            if(!AttackNow) {
                if(Mathf.Abs(tx - x) < 0.1f) {
                    Speed = Vector2.zero;
                    animator.Play(attack[(muki + 1) % 4]);
                    rb2d.velocity = Speed;
                    AttackNow = true;
                    muki = (muki + 1) % 4;
                } else if(Mathf.Abs(ty - y) < 0.1f) {
                    animator.Play(attack[(muki + 1) % 4]);
                    Speed = Vector2.zero;
                    rb2d.velocity = Speed;
                    AttackNow = true;
                    muki = (muki + 1) % 4;
                } else {
                    switch(muki) {
                        case 0:
                            dy = (ty + r) - y;
                            if(dy > 0) {
                                if(dy < 1) {
                                    Speed.Set(0, 10);
                                } else {
                                    Speed.Set(0, 20);
                                }
                            } else {
                                Speed.Set(0, 0);
                                muki++;
                            }
                            break;
                        case 1:
                            dx = (tx + r) - x;
                            if(dx > 0) {
                                if(dx < 1) {
                                    Speed.Set(10, 0);
                                } else {
                                    Speed.Set(20, 0);
                                }
                            } else {
                                Speed.Set(0, 0);
                                muki++;
                            }
                            break;
                        case 2:
                            dy = (ty - r) - y;
                            if(dy < 0) {
                                if(dy > -1) {
                                    Speed.Set(0, -10);
                                } else {
                                    Speed.Set(0, -20);
                                }
                            } else {
                                Speed.Set(0, 0);
                                muki++;
                            }
                            break;
                        case 3:
                            dx = (tx - r) - x;
                            if(dx < 0) {
                                if(dx > -1) {
                                    Speed.Set(-10, 0);
                                } else {
                                    Speed.Set(-20, 0);
                                }
                            } else {
                                Speed.Set(0, 0);
                                muki = 0;
                            }
                            break;

                    }
                }
            }
            rb2d.velocity = Speed;
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
}
