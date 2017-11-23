using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealBottle:ItemEquipment {
    public GameObject Circle;
    public CircleCollider2D CC2D;
    public override float ItemCharge() {
        if(charge == 0) {
            Circle.SetActive(true);
            charge = 1;
        }
        charge += 0.09f;
        Circle.transform.localScale = Vector3.one * charge;
        return charge;
    }
    public override void ItemUse() {
        charge = 0;
        StartCoroutine(ItemEffect());
    }
    public IEnumerator ItemEffect() {
        CC2D.enabled = true;
        yield return null;
        Destroy(gameObject);
    }
}
