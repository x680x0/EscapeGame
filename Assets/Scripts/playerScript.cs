using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerScript:objectBase {
    [System.NonSerialized]
    Animator animator;
    static int[] walk;
    static int[] stay;
    static int[] attack;
    static int[] damaged;
    static int[] through;
    static int dead;
    bool gameover;
    public itemMgr.itemID eItem;
    public itemMgr.weapnID eWeapon;
    GameObject pick;
    public GameObject weapon;
    weaponBase weaponScript;
    public bool isAttack, isDamaged;
    int knockX, knockY, knockCount;
    // Use this for initialization
    public override void Start() {
        gameover = false;
        base.Start();
        X = (int)(transform.localPosition.x / once);
        Y = (int)(transform.localPosition.y / once);
        X = (int)(transform.localPosition.x / once);
        Y = (int)(transform.localPosition.y / once);
        
        MAXHP = HP = 100;
        vector = 0;
        isAttack = false;
        isDamaged = false;
        knockCount = 0;
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
        pick = null;
    }
    public override void Damaged(int damage, typeOfDamage type, int _X, int _Y, GameObject Attacker, string tag) {
        if(this.gameObject == Attacker || this.gameObject.tag == tag) {


        } else {
            if(HP >= 0) {
                switch(type) {
                    case typeOfDamage.slip:
                        HP -= damage;
                        break;
                    default:
                        if(!isDamaged) {
                            isAttack = false;
                            HP -= damage;
                            animator.Play(damaged[vector]);
                            if(weapon != null) {
                                weaponScript.StopAnimation();
                            }
                            int dX = X - _X;
                            int dY = Y - _Y;
                            if(dX == 0 && dY == 0) {
                                dX = dY = 1;
                            }
                            float knockScale = 10000.0f / (dX * dX + dY * dY);
                            knockScale = Mathf.Sqrt(knockScale);
                            knockX = (int)(knockScale * dX);
                            knockY = (int)(knockScale * dY);

                            knockCount = 30;
                            isDamaged = true;
                        }
                        break;
                }
                if(HP <= 0) {
                    
                }
            }
        }
    }
    // Update is called once per frame
    public override void Update() {
        base.Update();
    }
    public override void FixedUpdate() {
        if(HP >= 0) {
            pick = null;
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
                        } else {
                            if(groundCheck.transform.parent.tag == "Item") {
                                pick = groundCheck.transform.parent.gameObject;
                            }
                        }
                    }

                }
            }
            if(!inLight) {
                Damaged(1, typeOfDamage.slip, 0, 0, null, "");
            }
            if(isDamaged) {
                X += 2 * knockX / 3;
                Y += 2 * knockY / 3;
                knockX /= 3;
                knockY /= 3;
                knockCount -= 1;
                if(knockCount <= 0) {
                    isDamaged = false;
                }
            } else if(!isAttack) {
                readyX = 0; readyY = 0;//初期化
                if(MGR.input[(int)mgrScript.keyUse.vectorlock] > 0) {
                    stop = true;
                    for(int i = 0; i < 4; i++) {
                        if(MGR.input[i] > 0) {
                            stop = false;
                            SetVector(i, 10);
                        }
                    }
                } else {
                    if(MGR.input[vector] > 0) {
                        stop = false;
                        SetVector(vector, 10);
                        if(MGR.input[(vector + 1) % 4] > 0) {
                            SetVector((vector + 1) % 4, 10);
                        }
                        if(MGR.input[(vector + 3) % 4] > 0) {
                            SetVector((vector + 3) % 4, 10);
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
                }
                if(stop) {
                    animator.Play(stay[vector]);
                } else {
                    animator.Play(walk[vector]);
                }
                Move();
                if(MGR.input[(int)mgrScript.keyUse.pick] == 1) {
                    if(pick != null) {
                        itemBase itembase = pick.GetComponent<itemBase>();
                        GameObject W;
                        if(itembase.ID != itemMgr.itemID.weapon) {
                            eItem = itembase.ID;
                        } else {
                            eWeapon = itembase.WID;
                            W = itembase.EquipWeapon();
                            weapon = W;
                            W.transform.parent = this.gameObject.transform;
                            W.transform.localPosition = new Vector3(0, 0, 0);
                            weaponScript = weapon.GetComponent<weaponBase>();
                        }

                        Destroy(pick);
                    }
                }
                if(MGR.input[(int)mgrScript.keyUse.weapon] == 1) {//武器攻撃
                    isAttack = true;
                    animator.Play(attack[vector]);
                    if(weapon != null) {
                        weaponScript.PlayAnimation(vector);
                    }
                } else
                if(MGR.input[(int)mgrScript.keyUse.item] == 0) {

                    if(MGR.prevInput[(int)mgrScript.keyUse.item] > 30) {
                        if(eItem != itemMgr.itemID.None) {
                            isAttack = true;
                            itemBase a = MGR.ItemInstantiate(eItem, itemMgr.weapnID.None).GetComponent<itemBase>();
                            if(a != null) {
                                a.Through(vector, this.gameObject, 20, X, Y);
                            }
                            eItem = itemMgr.itemID.None;
                            animator.Play(through[vector]);
                        }
                    } else if(MGR.prevInput[(int)mgrScript.keyUse.item] != 0) {
                        if(eItem != itemMgr.itemID.None) {
                            if(MGR.ItemUse(eItem, this) == 0) {
                                eItem = itemMgr.itemID.None;
                            } else if((MGR.ItemUse(eItem, this) == 2)) {
                                isAttack = true;
                                itemBase a = MGR.ItemInstantiate(eItem, itemMgr.weapnID.None).GetComponent<itemBase>();
                                if(a != null) {
                                    a.Through(vector, this.gameObject, 20, X, Y);
                                }
                                eItem = itemMgr.itemID.None;
                                animator.Play(through[vector]);
                            }
                        }
                    }
                }
            }
        }else if(!gameover){
            animator.Play(dead);
            gameover = true;
        }
    }

    public void WeaponAttack() {
        int count = 0;
        int da=0;
        switch(eWeapon) {
            case itemMgr.weapnID.None:
                da = 0;
                break;
            case itemMgr.weapnID.shortSord:
                da = 10;
                break;
            case itemMgr.weapnID.hardSord:
                da = 20;
                break;
        }
        foreach(Transform child in weaponScript.attackpivot[vector].transform) {
            Attack(child.gameObject, da, typeOfDamage.sord,this.gameObject.tag);
            count++;
        }

        Attack(pivot[vector], da, typeOfDamage.sord, this.gameObject.tag);
    }

    public void GameOver() {
        Application.LoadLevel("title");
    }
}
