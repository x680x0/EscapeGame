using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime:EnemyScript {
    static int[] walk, attack;
    static int die;

    public GameObject[] attackCol;
    public bool AttackNow;
    float Attacktime = 0;
    Animator animator;
    BoxCollider2D bc2d;
    // Use this for initialization
    override public void Start() {
        base.Start();
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
    }
    public override void FixedUpdate() {
        if(DamageTimer <= 0) {
            DamageTimer = 0;
        } else {
            DamageTimer -= 0.1f;
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
                    walkvect *= Random.Range(0f, 3f);
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
        if(DamageTimer <= 0f) {
            Collider2D[][] CheckCollider = new Collider2D[1][];
            CheckCollider[0] = Physics2D.OverlapPointAll(pivot[muki].transform.position);

            foreach(Collider2D[] CheckList in CheckCollider) {

                foreach(Collider2D groundCheck in CheckList) {
                    if(groundCheck != null) {
                        if(groundCheck.isTrigger) {
                            if(groundCheck.tag == "PlayerAttack") {
                                Damaged(groundCheck.gameObject);
                            }
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
    }
    public void StopAttack() {
        foreach(GameObject attackcol in attackCol) {
            attackcol.SetActive(false);
        }
    }

    public void SlimeAttack(int vect) {
        attackCol[vect].SetActive(true);
    }

    public override void Damaged(GameObject obj) {

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
                DamageTimer = 1.5f;
            }
        }
    }
}
