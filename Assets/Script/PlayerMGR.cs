using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMGR:MonoBehaviour {
    public int[] HP;
    public GameObject[] Player;
    GameObject Center;
    public GoalScript gs;
    bool GO;
    // Use this for initialization
    void Awake() {
        HP = new int[4];
        for(int i = 0; i < 4; i++) {
            HP[i] = 100;
        }
    }
    void Start() {
        GO = false;
        Center = GameObject.Find("Center");
    }

    // Update is called once per frame
    void Update() {
        if(HP[0]<=0&&HP[1]<=0 && HP[2] <= 0 && HP[3] <= 0&&!GO) {
            StartCoroutine(gs.GameOver());
            for(int i = 0; i < 4; i++) {
                HP[i] = -1;
            }
            GO = true;
        }
    }
    public void ItemApply(ItemEquipment.ItemAction IA, int num) {
        
    }
    public void setHP(int number,int hp) {
        HP[number] = hp;
    }
    public GameObject ReTarget() {
        if(HP[0] <= 0 && HP[1] <= 0 && HP[2] <= 0 && HP[3] <= 0) {
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
    public GameObject SelectTarget(int num) {
        return Player[num];
    }
    public void ReBirth() {
        for(int i = 0; i < 4; i++) {
            if(HP[i] == 0) {
                Player[i].GetComponent<PlayerScript>().Heal(50);
                Player[i].transform.position = Center.transform.position;
            }
        }
    }
}
