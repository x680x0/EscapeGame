using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class mgrScript : MonoBehaviour {
    public float once;
    public int[] input;
    public int[] prevInput;
    public bool[] b_input;
    public int numberOfPlayer;
    public objectBase[] player;
    public itemMgr itemmgr;
    public enemyMgr enemymgr;
    public Image image;
    public bool END;

    public enum keyIn{
        up=KeyCode.UpArrow,
        right=KeyCode.RightArrow,
        down=KeyCode.DownArrow,
        left=KeyCode.LeftArrow,
        pick=KeyCode.Z,
        weapon=KeyCode.X,
        item=KeyCode.C,
        vectorlock=KeyCode.LeftShift
    }
    public enum keyUse {
        up = 0,
        right,
        down ,
        left,
        pick,
        weapon,
        item,
        vectorlock
    }
    // Use this for initialization
    void Awake() {
        player = new objectBase[4];
        player[0] = GameObject.Find("Player1").GetComponent<objectBase>();
    }
    void Start () {
        END = false;
        b_input = new bool[8];
        input = new int[8];
        prevInput = new int[8];
        input = new int[8];
        for(int i = 0; i < b_input.Length; i++) {
            input[i] = 0;
            prevInput[i] = 0;
            b_input[i] = false;
        }
        numberOfPlayer = 1;
        itemmgr = GetComponent<itemMgr>();
        enemymgr = GetComponent<enemyMgr>();
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
        if(Input.GetKey((KeyCode)keyIn.pick) ){
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
        if(END) {
            image.enabled = true;
        }
    }
    void FixedUpdate() {
        for(int i = 0; i < b_input.Length; i++) {
            prevInput[i] = input[i];
            if(b_input[i]) {
                input[i] += 1;
            }else {
                input[i] = 0;
            }
        }
    }

    public GameObject ItemInstantiate(itemMgr.itemID _item,itemMgr.weapnID _weapon) {
        if(_item != itemMgr.itemID.weapon) {
            return itemmgr.ItemInstantiate(_item);
        }else {
            return itemmgr.WeaponInstantiate(_weapon);
        }
    }
    public int ItemUse(itemMgr.itemID _item,objectBase script) {
        return itemmgr.ItemUse(_item, script);
    }
    public GameObject EnemyInstantiate(enemyMgr.enemyID _enemy) {
        return enemymgr.EnemyInstantiate(_enemy);
    }
}
