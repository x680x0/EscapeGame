using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroy : MonoBehaviour {
    public float Timer;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        Timer -= 0.1f;
        if(Timer < 0) {
            Destroy(this.gameObject);
        }
	}
}
