using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class weapongui:MonoBehaviour {

    public Image gage;
    public GameObject player;
    playerScript objec;
    public Sprite[] w;
    public Sprite kyomu;
    // Use this for initialization
    void Start() {

        objec = player.GetComponent<playerScript>();
    }

    // Update is called once per frame
    void Update() {
        int tmp = (int)objec.eWeapon;
        if(tmp != -1) {
            gage.sprite = w[tmp];
        }else {
            gage.sprite =w[(int)itemMgr.weapnID.count];
        }
    }
}