using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mgrScript : MonoBehaviour {
    public float once;
    public int[] input;
    public bool[] b_input;
    public class min {

    }
    // Use this for initialization
    void Start () {
        b_input = new bool[4];
        input = new int[4];
        for(int i = 0; i < b_input.Length; i++) {
            input[i] = 0;
            b_input[i] = false;
        }
	}
	
	// Update is called once per frame
	void Update () {
        if(Input.GetKey("up")) {
            b_input[0] = true;
        }
        if(Input.GetKey("right")) {
            b_input[1] = true;
        }
        if(Input.GetKey("down")) {
            b_input[2] = true;
        }
        if(Input.GetKey("left")) {
            b_input[3] = true;
        }

    }
    void FixedUpdate() {
        for(int i = 0; i < b_input.Length; i++) {
            if(b_input[i]) {
                input[i] += 1;
            }else {
                input[i] = 0;
            }
            b_input[i] = false;
        }
    }
}
