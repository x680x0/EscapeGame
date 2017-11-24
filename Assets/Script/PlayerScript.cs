using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript:Objects {
    [System.NonSerialized]
    Animator animator;
    GUIControl GUIcon;
    static int[] walk;
    static int[] stay;
    static int[] attack;
    static int[] damaged;
    static int[] through;
    static int dead;

    public float exist=0;
    bool existF = false;

    public float DamageTimer = 0,HealTimer=0;
    public bool AttackNow, DamageNow,ItemCharge;
    public int MAXHP,HP;
    public MGR INPUT;
    public PlayerMGR PMGR;
    public GameObject[] pickpivot;
    public WeaaponEquipment eWeapon;
    public ItemEquipment eItem;

    public GameObject HealEffect;

    Status ForGUI;

    Vector2 nock;
    float nocktime = 0;
    public int contlol;
    // Use this for initialization
    override public void Start() {
        base.Start();
        GUIcon = GameObject.Find("GUICanvas").GetComponent<GUIControl>();
        GUIcon.SetWeapon(WeaponType.None, contlol);
        GUIcon.SetItem(ItemType.None, contlol);
        MAXHP = 100;
        ItemCharge = false;
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
        if(!existF&&exist>30) {
            StartCoroutine(Extinction());
        }
        if(HealTimer > 0) {
            HealTimer -= 0.1f;
            if(HealTimer <= 0) {
                HealTimer = 0;
            }
        }
        if(DamageTimer <= 0) {
            if(HP > 0) {
                DamageTimer = 0;
                if(DamageNow) {
                    DamageNow = false;
                }
                if(!AttackNow) {
                    if(ItemCharge && INPUT.Inpad[contlol][6] == 0 && eItem != null) {
                        eItem.ItemUse();
                        ItemCharge = false;
                        eItem = null;
                        existF = true;
                        GUIcon.SetItem(ItemType.None, contlol);
                    } else if(INPUT.Inpad[contlol][6] > 0 && eItem != null) {//アイテム長押しなどの処理
                        eItem.ItemCharge();
                        ItemCharge = true;
                        move = false;
                        existF = true;
                    } else if(INPUT.Inpad[contlol][5] == 1 && eWeapon != null) {
                        animator.Play(attack[muki]);
                        eWeapon.GetComponent<WeaaponEquipment>().PlayAnimation(muki);
                        AttackNow = true;
                        existF = true;

                    } else if(INPUT.Inpad[contlol][muki] > 0) {
                        move = true;
                        SetSpeed(muki, 10);
                        if(INPUT.Inpad[contlol][(muki + 1) % 4] > 0) {
                            SetSpeed((muki + 1) % 4, 10);
                        }
                        if(INPUT.Inpad[contlol][(muki + 3) % 4] > 0) {
                            SetSpeed((muki + 3) % 4, 10);
                        }
                        existF = true;
                    } else {
                        move = false;
                        for(int i = 0; i < 4; i++) {
                            if(INPUT.Inpad[contlol][i] > 0) {
                                muki = i;
                                move = false;
                            }
                        }
                        if(!existF) {
                            exist += 0.1f;
                        }
                    }
                    MakeSpeed(ss);
                    rb2d.velocity = speed;
                    if(!move) {
                        animator.Play(stay[muki]);
                    } else {
                        animator.Play(walk[muki]);
                    }


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
                                                GameObject tmp = groundCheck.gameObject.GetComponent<ItemObject>().Pick(this.transform);
                                                eItem = tmp.GetComponent<ItemEquipment>();
                                                GUIcon.SetItem(eItem.GetItemType(), contlol);
                                            }
                                        }
                                        if(groundCheck.tag == "PickUpWeapon") {
                                            if(eWeapon == null) {
                                                //ItemObject=groundCheck.transform.parent.gameObject.GetComopnent<ItemObject>();
                                                //ItemObject.関数名().transform.parent=this.gameObject;
                                                //関数内でInstansとアイテムの初期化、そしてそれをreturnする
                                                GameObject tmp= groundCheck.gameObject.GetComponent<ItemObject>().Pick(this.transform);
                                                eWeapon = tmp.GetComponent<WeaaponEquipment>();
                                                tmp.GetComponent<CNum>().ini(contlol);
                                                GUIcon.SetWeapon(eWeapon.GetWeaponType(), contlol);
                                            }
                                        }
                                    }
                                }
                            }

                        }

                    }

                }
            }
            SetOrder(0);
            inLight = false;
            Collider2D[][] CheckDamage = new Collider2D[1][];
            CheckDamage[0] = Physics2D.OverlapPointAll(pivot[muki].transform.position);

            foreach(Collider2D[] CheckList in CheckDamage) {

                foreach(Collider2D groundCheck in CheckList) {
                    if(groundCheck != null) {
                        if(groundCheck.isTrigger) {

                            if(groundCheck.tag=="Poison"||(groundCheck.tag == "EnemyAttack" && DamageTimer <= 0f)) {
                                Damaged(groundCheck.gameObject,false);
                            }
                            if(groundCheck.tag == "Heal" && HealTimer <= 0) {
                                Heal(10);
                            }
                            if(groundCheck.tag == "PlayArea") {
                                inLight = true;
                            }
                        }
                    }
                }

            }
            if(!inLight) {
                Damaged(null, true);
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
    public void Damaged(GameObject obj,bool Light) {
        if(Light) {
            GUIcon.SetDamage(2, contlol);
            if(HP <=1) {
                HP = 0;
                animator.Play(dead);
            } else {
                HP -= 2;
                DamageTimer = 0.1f;
            }
            return;
        }
        DamageInf dmi = obj.GetComponent<DamageInf>();
        int damage = 0;

        if(dmi != null) {
            if(dmi.GetPoison()) {
                damage=dmi.GetDamage();
                if(damage == 0) { }
                else if(HP <= damage) {
                    HP = 0;
                    GUIcon.SetDamage(damage, contlol);
                    animator.Play(dead);
                } else {
                    animator.Play(damaged[muki]);
                    HP -= damage;
                    GUIcon.SetDamage(damage, contlol);
                    DamageTimer = 0.1f;

                }
            } else {
                dmi.GetInf(ref damage, ref nock, ref nocktime, pivot[muki].transform.position);
                if(HP <= damage) {
                    HP = 0;
                    GUIcon.SetDamage(damage, contlol);
                    animator.Play(dead);
                } else {
                    DamageNow = true;
                    animator.Play(damaged[muki]);
                    HP -= damage;
                    GUIcon.SetDamage(damage, contlol);
                    DamageTimer = 1.5f;

                }
            }
        }
    }

    public void Heal(int num) {
        Instantiate(HealEffect, transform);
        HealTimer = 0.5f;
        HP += num;

        GUIcon.SetDamage(-num, contlol);
        if(MAXHP < HP) {
            HP = MAXHP;
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
    void SetStatus() {
        ForGUI.hp = HP;
    }
    IEnumerator Extinction() {
        for(float i =0; i < 255; i += 1) {
            sr.color = new Color(1, 1, 1, (float)((255 - i) / 255));
            yield return null;
            if(existF) {
                sr.color = new Color(1, 1, 1, 1);
                break;
            }
        }
        if(!existF) {
            HP = -1;
            PMGR.setHP(contlol, HP);
            GUIcon.SetDamage(100, contlol);
            Destroy(this.gameObject);
        }
    }
}
