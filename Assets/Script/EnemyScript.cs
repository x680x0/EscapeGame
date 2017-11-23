using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : Objects {
    public Objects Target;
    public int HP;
    PlayerMGR PMGR;
    // Use this for initialization
    override public void Start () {
        base.Start();
        muki = 0;
        PMGR=GameObject.Find("PlayerMGR").GetComponent<PlayerMGR>();
    }
    public virtual void FixedUpdate() {
     /*   if(DamageTimer <= 0) {
            DamageTimer = 0;
        } else {
            DamageTimer -= 0.1f;
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
        }*/
    }

    public virtual void Damaged(GameObject obj,int num) {
        
            DamageInf dmi = obj.GetComponent<DamageInf>();
            int damage;
            if(dmi != null) {
                damage = dmi.GetDamage();
                if(HP <= damage) {
                    HP = 0;
            } else {
                    HP -= damage;
                }
            }
    }
    public void Kill() {
        Destroy(this.gameObject);
    }
    public void ReTarget() {
        Target = PMGR.ReTarget().GetComponent<Objects>();
    }
}
