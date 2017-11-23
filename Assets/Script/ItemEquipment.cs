using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemEquipment : MonoBehaviour {
    protected ItemType itemType;
    public float charge=0;
    void Start() {
        charge = 0;
    }
    public enum ItemAction {
        heal=0,
    };
    public virtual void ItemUse() {
        charge = 0;
    }
    public virtual float ItemCharge() {
        return charge;
    }
    public ItemType GetItemType() {
        return itemType;
    }
}
