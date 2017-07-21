using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class goalScript:objectBase {

    public int defY;
    public int speed, a;
    public override void Start() {
        base.Start();
     
        HP = MAXHP = 10;
    }
    public override void Update() {
        spriteRenderer.sortingOrder = -Y;
        transform.localPosition = new Vector3(X * once, (Y+0) * once, transform.localPosition.z);
    }
    public override void FixedUpdate() {
         
    }

    public override void SetTourch(GameObject _tourch) {
    }
    public override void Damaged(int damage, typeOfDamage type, int _X, int _Y, GameObject Attacker, string tag) {

        // 
        if(this.gameObject == Attacker || this.gameObject.tag == tag) {


        } else if(type==objectBase.typeOfDamage.sord){
            HP -= damage;
            if(HP <= 0) {
                MGR.END = true;
                Destroy(this.gameObject);
            }
        }

    }
}
