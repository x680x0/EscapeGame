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
    bool inLight = true;

    public int Phase;
    // Use this for initialization
    public override void Start () {
        base.Start();
        Slow = 0;
        Phase = 0;
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
                    print(DY);
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
                        Phase = 0;
                        animator.Play(walk);
                    }
                    break;
            }
            if(!AttackNow) {
                /*  if(Mathf.Abs(tx - x) < 0.1f) {
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
              }*/
                rb2d.velocity = Speed;
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
}
