using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GamepadInput;

public class MGR:MonoBehaviour {
    public float once;
    public int[] input;
    public int[] prevInput;
    public int[][] Inpad;
    public int[][] preInpad; 
    public bool[] b_input;
    public int numberOfPlayer;
    GamepadState state;

    public enum keyIn {
        up = KeyCode.UpArrow,
        right = KeyCode.RightArrow,
        down = KeyCode.DownArrow,
        left = KeyCode.LeftArrow,
        pick = KeyCode.Z,
        weapon = KeyCode.X,
        item = KeyCode.C,
        vectorlock = KeyCode.LeftShift
    }
    public enum keyUse {
        up = 0,
        right,
        down,
        left,
        pick,
        weapon,
        item,
        vectorlock
    }
    public enum padUse {
        up = 0,
        right,
        down,
        left,
        pick,
        weapon,
        item,
        vectorlock,
        count
    }
    // Use this for initialization
    void Start() {
        Inpad = new int[4][];
        for(int i = 0; i < Inpad.Length; i++) {
            Inpad[i] = new int[(int)padUse.count];
        }
        b_input = new bool[8];
        input = new int[8];
        prevInput = new int[8];
        input = new int[8];
        for(int i = 0; i < b_input.Length; i++) {
            input[i] = 0;
            prevInput[i] = 0;
            b_input[i] = false;
        }
    }

    // Update is called once per frame
    void Update() {

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
        if(Input.GetKey((KeyCode)keyIn.pick)) {
            b_input[4] = true;
        } else {
            b_input[4] = false;
        }
        if(Input.GetKey((KeyCode)keyIn.weapon)) {
            b_input[5] = true;
        } else {
            b_input[5] = false;
        }
        if(Input.GetKey((KeyCode)keyIn.item)) {
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
            prevInput[i] = input[i];
            if(b_input[i]) {
                input[i] += 1;
            } else {
                input[i] = 0;
            }
        }
        for(int i = 0; i < 4; i++) {
            state = GamePad.GetState((GamePad.Index)(i+1));
            if(state.LeftStickAxis.y==1.0f) {
                Inpad[i][(int)padUse.up] += 1;
            } else {
                Inpad[i][(int)padUse.up] = 0;
            }
            if(state.LeftStickAxis.x == 1.0f) {
                Inpad[i][(int)padUse.right] += 1;
            } else {
                Inpad[i][(int)padUse.right] = 0;
            }
            if(state.LeftStickAxis.y == -1.0f) {
                Inpad[i][(int)padUse.down] += 1;
            } else {
                Inpad[i][(int)padUse.down] = 0;
            }
            if(state.LeftStickAxis.x == -1.0f) {
                Inpad[i][(int)padUse.left] += 1;
            } else {
                Inpad[i][(int)padUse.left] = 0;
            }
            if(state.B) {
                Inpad[i][(int)padUse.pick] += 1;
            } else {
                Inpad[i][(int)padUse.pick] = 0;
            }
            if(state.A) {
                Inpad[i][(int)padUse.item] += 1;
            } else {
                Inpad[i][(int)padUse.item] = 0;
            }
            if(state.Y) {
                Inpad[i][(int)padUse.weapon] += 1;
            } else {
                Inpad[i][(int)padUse.weapon] = 0;
            }
            if(state.X) {
                Inpad[i][(int)padUse.vectorlock] += 1;
            } else {
                Inpad[i][(int)padUse.vectorlock] = 0;
            }
        }
    }
    int GetPad(padUse pu,int num) {

        return 0;
    }
}
