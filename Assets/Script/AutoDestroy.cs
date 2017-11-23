using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroy : MonoBehaviour {
    public float Timer;
	// Use this for initialization
	void Start () {
        Timer = 0;
	}
	
	// Update is called once per frame
	void Update () {
        Timer += 0.1f;
        if(Timer > 20) {
            Destroy(this.gameObject);
        }
	}
}
