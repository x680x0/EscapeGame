using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class itemgui : MonoBehaviour {
    public Image gage;
    public GameObject player;
    playerScript objec;
    public Sprite[] i;
    public Sprite kyomu;
    // Use this for initialization
    void Start() {

        objec = player.GetComponent<playerScript>();
    }

    // Update is called once per frame
    void Update() {
        int tmp = (int)objec.eItem;
        if(tmp != -1) {
            gage.sprite = i[tmp];
        } else {
            gage.sprite = i[(int)itemMgr.itemID.weapon];
        }
    }
}
