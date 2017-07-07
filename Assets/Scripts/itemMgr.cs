using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemMgr : MonoBehaviour {
    public enum itemID{
        None=-1,
        healBottle,
        stone,
        count
    }
    public GameObject[] items;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public GameObject ItemInstantiate(itemID _item) {
        return Instantiate(items[(int)_item]);
    }
}
