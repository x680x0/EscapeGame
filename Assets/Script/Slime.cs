using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime:EnemyScript {
    static int[] walk, attack;
    static int die;
    public float[] DamageTimer;
    public GameObject[] attackCol;
    bool AttackNow;
    float Attacktime = 0;
    Animator animator;
    BoxCollider2D bc2d;
    // Use this for initialization
    override public void Start() {
        base.Start();
        DamageTimer = new float[4];
        bc2d = GetComponent<BoxCollider2D>();
        HP = 10;
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
        ReTarget();
    }
    public override void FixedUpdate() {
        int i = 0;
        for(i = 0; i < 4; i++) {
            if(DamageTimer[i] <= 0) {
                DamageTimer[i] = 0;
            } else {
                DamageTimer[i] -= 0.1f;
            }
        }
        if(Target == null){
            ReTarget();
        }
        Vector2 walkvect = new Vector2(0, 0);
        if(AttackNow) {
            rb2d.velocity = Vector2.zero;
            rb2d.mass = 10;

        } else {
            Attacktime -= 0.1f;
            if(HP > 0) {
                if(Target != null) {
                    walkvect = Target.GetPos() - (Vector2)transform.position;
                    walkvect.Normalize();
                    walkvect *= Random.Range(1f, 4f);
                }
                if(walkvect.x > 0f && (muki != 1)) {
                    animator.Play(walk[1]);
                    muki = 1;
                }
                if(walkvect.x < 0f && (muki != 3)) {
                    animator.Play(walk[3]);
                    muki = 3;
                }
            }

            rb2d.velocity = walkvect;
            SetOrder(0);
            if(Target != null && Attacktime <= 0) {
                Vector2 AttackRange = Target.GetPos() - (Vector2)transform.position;
                if(AttackRange.magnitude < 1.3f) {//ターゲットとの距離
                    AttackNow = true;
                    animator.Play(attack[Vector2int(AttackRange)]);
                    //boolをtrueにして攻撃、向きはAttackRangeから計算すること,boolをfalseにするのはモーション側でする
                }
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
                            int c= groundCheck.gameObject.transform.parent.gameObject.GetComponent<CNum>().GetContlol();
                            Damaged(groundCheck.gameObject,c);
                        }
                        if(groundCheck.tag == "PlayArea") {
                            inLight = true;
                        }
                    }
                }
            }

        }
        
    }

    public void reAttack() {
        AttackNow = false;
        rb2d.mass = 0.0001f;
        foreach(GameObject attackcol in attackCol) {
            attackcol.SetActive(false);
        }
        animator.Play(walk[muki]);
        Attacktime = 1;
        ReTarget();
    }
    public void StopAttack() {
        foreach(GameObject attackcol in attackCol) {
            attackcol.SetActive(false);
        }
    }

    public void SlimeAttack(int vect) {
        attackCol[vect].SetActive(true);
    }

    public override void Damaged(GameObject obj,int num) {
        if(DamageTimer[num] <= 0) {
            DamageInf dmi = obj.GetComponent<DamageInf>();
            int damage;
            if(dmi != null) {
                damage = dmi.GetDamage();
                if(HP <= damage) {
                    animator.Play(die);
                    bc2d.enabled = false;
                    HP = 0;
                } else {
                    HP -= damage;
                    DamageTimer[num] = 1.5f;
                    Instantiate(DamageParticle, transform);
                }
            }
        }else {
            print("muteki");
        }
    }
}
