using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMGR:MonoBehaviour {
    public int[] HP;
    public GameObject[] Player;
    // Use this for initialization
    void Start() {
        HP = new int[4];
    }

    // Update is called once per frame
    void Update() {

    }
    public void ItemApply(ItemEquipment.ItemAction IA, int num) {
        
    }
    public void setHP(int number,int hp) {
        HP[number] = hp;
    }
}
