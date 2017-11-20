using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal :EnemyScript {
    public GoalScript GS;
    public GameObject Attack;
    public override void Damaged(GameObject obj) {

        DamageInf dmi = obj.GetComponent<DamageInf>();
        int damage;
        if(dmi != null) {
            damage = dmi.GetDamage();
            if(HP <= damage) {
                HP = 0;
                End();
            } else {
                HP -= damage;
                DamageTimer = 1.5f;
            }
        }
    }
    void End() {
        Attack.SetActive(true);
        StartCoroutine(GS.GameClear());
    }
}
