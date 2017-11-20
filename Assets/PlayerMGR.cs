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
        switch(IA) {
            case ItemEquipment.ItemAction.heal:
                for(int i = 0; i < 2; i++) {
                    if(Player[i] != null) {
                        float a = Vector3.Distance(Player[i].transform.position, Player[num].transform.position);
                        if(a < 5) {
                            Player[i].GetComponent<PlayerScript>().HP += 10;
                        }
                    }
                }
                break;
        }
    }
    public void setHP(int number,int hp) {
        HP[number] = hp;
    }
}
