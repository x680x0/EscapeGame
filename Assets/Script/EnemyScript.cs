using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : Objects {
    

    public float DamageTimer = 0;
    public Objects Target;
    public int HP;
    // Use this for initialization
    override public void Start () {
        base.Start();
    }
    public virtual void FixedUpdate() {
        if(DamageTimer <= 0) {
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
        }
    }

    public override void Damaged(GameObject obj) {
        
            DamageInf dmi = obj.GetComponent<DamageInf>();
            int damage;
            if(dmi != null) {
                damage = dmi.GetDamage();
                if(HP <= damage) {
                    HP = 0;
            } else {
                    HP -= damage;
                    DamageTimer = 1.5f;
                }
            }
    }
    public void Kill() {
        Destroy(this.gameObject);
    } 
}
