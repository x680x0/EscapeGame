using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMGR:MonoBehaviour {
    public int[] HP;
    public GameObject[] Player;
    // Use this for initialization
    void Awake() {
        HP = new int[4];
    }
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {

    }
    public void ItemApply(ItemEquipment.ItemAction IA, int num) {
        
    }
    public void setHP(int number,int hp) {
        HP[number] = hp;
    }
    public GameObject ReTarget() {
        if((HP[0] + HP[1] + HP[2] + HP[3]) == 0) {
            return null;
        } else {
            while(true) {
                int r = Random.Range(0, 4);
                if(HP[r] > 0) {
                    return Player[r];
                }
            }
        }
    }
}
