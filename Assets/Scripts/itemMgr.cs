using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemMgr : MonoBehaviour {
    public enum itemID{
        None=-1,
        healBottle,
        stone,
        weapon,
        count
    }
    public enum weapnID {
        None=-1,
        shortSord,
        hardSord,
        count
    }
    public GameObject[] items;
    public GameObject[] weapons;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public GameObject ItemInstantiate(itemID _item) {
            return Instantiate(items[(int)_item]);
    }
    public GameObject WeaponInstantiate(weapnID _item) {
        return Instantiate(weapons[(int)_item]);
    }
    public int ItemUse(itemID _item,objectBase script) {
        //  0:使用後アイテムが残らない 1:使用後アイテムが残る 
        switch(_item) {
            case itemID.healBottle:
                script.Heal(50);
                return 0;
            case itemID.stone:
                return 2;
            default:
                return 1;
        }
    }
}
