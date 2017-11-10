using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject :Objects {
    public GameObject Item;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public GameObject Pick(Transform _parent) {
        GameObject re;
        re=Instantiate(Item,_parent);
        Destroy(this.gameObject);
        return re;
    }
}
