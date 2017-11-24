using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : Objects {
    public Objects Target;
    public int HP;
    PlayerMGR PMGR;
    public GameObject DamageParticle;
    // Use this for initialization
    override public void Start () {
        base.Start();
        muki = 0;
        PMGR=GameObject.Find("PlayerMGR").GetComponent<PlayerMGR>();
    }
    public virtual void FixedUpdate() {

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
        Objects tmp;
        tmp = PMGR.ReTarget().GetComponent<Objects>();
        if(tmp != null) {
            Target = tmp;
        }
    }
}
