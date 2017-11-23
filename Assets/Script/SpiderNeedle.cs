using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderNeedle :EnemyScript {
    public float nspeed;
    public override void FixedUpdate() {
            Collider2D[][] CheckCollider = new Collider2D[1][];
            CheckCollider[0] = Physics2D.OverlapPointAll(pivot[muki].transform.position);

            foreach(Collider2D[] CheckList in CheckCollider) {

                foreach(Collider2D groundCheck in CheckList) {
                    if(groundCheck != null) {
                        if(groundCheck.isTrigger) {
                            if(groundCheck.tag == "PlayerAttack") {
                                Damaged(groundCheck.gameObject,0);
                            }
                        }
                    }
                }

            }
        
    }

    public override void Damaged(GameObject obj,int num) {

        DamageInf dmi = obj.GetComponent<DamageInf>();
        int damage;
        if(dmi != null) {
            if(dmi.GetDamage() > 0) {
                Destroy(this.gameObject);
            }
            
        }
    }
    public void ini(int vect) {
        rb2d = GetComponent<Rigidbody2D>();
        transform.Rotate(0, 0, -90*vect);
        switch(vect) {
            case 0:
                rb2d.velocity = Vector2.up*nspeed;
                break;
            case 1:
                rb2d.velocity = Vector2.right * nspeed;
                break;
            case 2:
                rb2d.velocity = Vector2.down * nspeed;
                break;
            case 3:
                rb2d.velocity = Vector2.left * nspeed;
                break;

        }
    }
}
