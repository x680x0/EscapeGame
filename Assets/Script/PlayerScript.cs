using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript:Objects {
    [System.NonSerialized]
    Animator animator;
    static int[] walk;
    static int[] stay;
    static int[] attack;
    static int[] damaged;
    static int[] through;
    static int dead;
    public float DamageTimer = 0;
    public bool AttackNow, DamageNow;
    public int HP;
    public MGR INPUT;
    public PlayerMGR PMGR;
    public GameObject[] pickpivot;
    public WeaaponEquipment eWeapon;
    public ItemEquipment eItem;
    Vector2 nock;
    float nocktime = 0;
    public int contlol;
    // Use this for initialization
    override public void Start() {
        base.Start();
        nock = Vector2.zero;
        AttackNow = false;
        DamageNow = false;
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
        through = new int[4];
        through[0] = Animator.StringToHash("throughup");
        through[1] = Animator.StringToHash("throughright");
        through[2] = Animator.StringToHash("throughdown");
        through[3] = Animator.StringToHash("throughleft");
        dead = Animator.StringToHash("dead");
        animator.Play(dead);
    }

    // Update is called once per frame
    void Update() {
        PMGR.setHP(contlol, HP);
    }
    void WeaponAttack() {

    }
    void FixedUpdate() {
        speed = Vector2.zero;
        if(DamageTimer <= 0 && HP > 0) {
            DamageTimer = 0;
            if(DamageNow) {
                DamageNow = false;
            }
            if(!AttackNow) {
                if(INPUT.Inpad[contlol][5] == 1 && eWeapon != null) {
                    animator.Play(attack[muki]);
                    eWeapon.GetComponent<WeaaponEquipment>().PlayAnimation(muki);
                    AttackNow = true;

                } else if(INPUT.Inpad[contlol][6] == 1 && eItem != null) {
                    PMGR.ItemApply(eItem.GetComponent<ItemEquipment>().ItemUse(), contlol);
                    Destroy(eItem.gameObject);
                    eItem = null;
                } else if(INPUT.Inpad[contlol][muki] > 0) {
                    move = true;
                    SetSpeed(muki, 10);
                    if(INPUT.Inpad[contlol][(muki + 1) % 4] > 0) {
                        SetSpeed((muki + 1) % 4, 10);
                    }
                    if(INPUT.Inpad[contlol][(muki + 3) % 4] > 0) {
                        SetSpeed((muki + 3) % 4, 10);
                    }
                } else {
                    move = false;
                    for(int i = 0; i < 4; i++) {
                        if(INPUT.Inpad[contlol][i] > 0) {
                            muki = i;
                            move = false;
                        }
                    }

                }
                MakeSpeed(ss);
                rb2d.velocity = speed;
                if(!move) {
                    animator.Play(stay[muki]);
                } else {
                    animator.Play(walk[muki]);
                }
                SetOrder(0);

                if(INPUT.Inpad[contlol][(int)MGR.keyUse.pick] == 1) {
                    Collider2D[][] CheckCollider = new Collider2D[1][];
                    CheckCollider[0] = Physics2D.OverlapPointAll(pickpivot[muki].transform.position);

                    foreach(Collider2D[] CheckList in CheckCollider) {

                        foreach(Collider2D groundCheck in CheckList) {
                            if(groundCheck != null) {
                                if(groundCheck.isTrigger) {
                                    if(groundCheck.tag == "PickUpItem") {
                                        if(eItem == null) {
                                            //ItemObject=groundCheck.transform.parent.gameObject.GetComopnent<ItemObject>();
                                            //ItemObject.関数名().transform.parent=this.gameObject;
                                            //関数内でInstansとアイテムの初期化、そしてそれをreturnする
                                            eItem = groundCheck.gameObject.GetComponent<ItemObject>().Pick(this.transform).GetComponent<ItemEquipment>();
                                        }
                                    }
                                    if(groundCheck.tag == "PickUpWeapon") {
                                        if(eWeapon == null) {
                                            //ItemObject=groundCheck.transform.parent.gameObject.GetComopnent<ItemObject>();
                                            //ItemObject.関数名().transform.parent=this.gameObject;
                                            //関数内でInstansとアイテムの初期化、そしてそれをreturnする
                                            eWeapon = groundCheck.gameObject.GetComponent<ItemObject>().Pick(this.transform).GetComponent<WeaaponEquipment>();
                                        }
                                    }
                                }
                            }
                        }

                    }

                }

            }

            Collider2D[][] CheckDamage = new Collider2D[1][];
            CheckDamage[0] = Physics2D.OverlapPointAll(pivot[muki].transform.position);

            foreach(Collider2D[] CheckList in CheckDamage) {

                foreach(Collider2D groundCheck in CheckList) {
                    if(groundCheck != null) {
                        if(groundCheck.isTrigger) {

                            if(groundCheck.tag=="Poison"||(groundCheck.tag == "EnemyAttack" && DamageTimer <= 0f)) {
                                Damaged(groundCheck.gameObject);
                            }

                        }
                    }
                }

            }

        } else {
            if(HP > 0) {
                DamageTimer -= 0.1f;
                if(nocktime > 0) {
                    rb2d.velocity = speed + nock;
                    nocktime -= 0.1f;
                } else {
                    rb2d.velocity = speed;
                }
            } else {
                rb2d.velocity = Vector3.zero;
            }
        }

    }
    public override void Damaged(GameObject obj) {
        DamageInf dmi = obj.GetComponent<DamageInf>();
        int damage = 0;

        if(dmi != null) {
            if(dmi.GetPoison()) {
                damage=dmi.GetDamage();
                if(damage == 0) { }
                else if(HP <= damage) {
                    HP = 0;
                    animator.Play(dead);
                } else {
                    animator.Play(damaged[muki]);
                    HP -= damage;
                    DamageTimer = 0.1f;

                }
            } else {
                dmi.GetInf(ref damage, ref nock, ref nocktime, pivot[muki].transform.position);
                if(HP <= damage) {
                    HP = 0;
                    animator.Play(dead);
                } else {
                    DamageNow = true;
                    animator.Play(damaged[muki]);
                    HP -= damage;
                    DamageTimer = 1.5f;

                }
            }
        }
    }

    void SetSpeed(int vect, float s) {
        switch(vect) {
            case 0:
                speed += Vector2.up * s;
                break;
            case 1:
                speed += Vector2.right * s;
                break;
            case 2:
                speed += Vector2.down * s;
                break;
            case 3:
                speed += Vector2.left * s;
                break;
        }
    }
    void MakeSpeed(float s) {
        speed.Normalize();
        speed *= s;
        return;
    }
}
