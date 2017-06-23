using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mgrScript : MonoBehaviour {
    public float once;
    public int[] input;
    public bool[] b_input;
    public enum keyIn{
        up=KeyCode.UpArrow,
        right=KeyCode.RightArrow,
        down=KeyCode.DownArrow,
        left=KeyCode.LeftArrow,
        weapon=KeyCode.Z,
        item1=KeyCode.X,
        item2=KeyCode.C,
        vectorlock=KeyCode.LeftShift
    }
    public enum keyUse {
        up = 0,
        right,
        down ,
        left,
        weapon,
        item1,
        item2,
        vectorlock
    }
    // Use this for initialization
    void Start () {
        b_input = new bool[8];
        input = new int[8];
        for(int i = 0; i < b_input.Length; i++) {
            input[i] = 0;
            b_input[i] = false;
        }
	}
	
	// Update is called once per frame
	void Update () {
        if(Input.GetKey((KeyCode)keyIn.up)) {
            b_input[(int)keyUse.up] = true;
        } else {
            b_input[0] = false;
        }
        if(Input.GetKey((KeyCode)keyIn.right)) {
            b_input[1] = true;
        } else {
            b_input[1] = false;
        }
        if(Input.GetKey((KeyCode)keyIn.down)) {
            b_input[2] = true;
        } else {
            b_input[2] = false;
        }
        if(Input.GetKey((KeyCode)keyIn.left)) {
            b_input[3] = true;
        } else {
            b_input[3] = false;
        }
        if(Input.GetKey((KeyCode)keyIn.weapon) ){
            b_input[4] = true;
        } else {
            b_input[4] = false;
        }
        if(Input.GetKey((KeyCode)keyIn.item1)) {
            b_input[5] = true;
        } else {
            b_input[5] = false;
        }
        if(Input.GetKey((KeyCode)keyIn.item2)) {
            b_input[6] = true;
        } else {
            b_input[6] = false;
        }
        if(Input.GetKey((KeyCode)keyIn.vectorlock)) {
            b_input[7] = true;
        } else {
            b_input[7] = false;
        }
    }
    void FixedUpdate() {
        for(int i = 0; i < b_input.Length; i++) {
            if(b_input[i]) {
                input[i] += 1;
            }else {
                input[i] = 0;
            }
        }
    }
}
