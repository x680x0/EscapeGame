using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemEquipment : MonoBehaviour {

    public enum ItemAction {
        heal=0,
    };

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public ItemAction ItemUse() {
        return ItemAction.heal;
    }
}
